using OrganizationData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationData.Data
{
    internal abstract class ModelDataProvider<TModel> : IDataProvider<TModel>
        where TModel : IModel
    {
        protected IDbConnection connection;
        protected IModelFactory<TModel> factory;

        public ModelDataProvider(
            IDbConnection connection, 
            IModelFactory<TModel> factory)
        {
            this.connection = connection;
            this.factory = factory;
        }
        public virtual IList<TModel> GetAll()
        {
            try
            {
                connection.Open();

                using (var command = CreateSelectCommand(connection))
                {
                    command.Prepare();

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        var models =  new List<TModel>();

                        while (reader.Read())
                        {
                            object[] vals = new object[reader.FieldCount];

                            reader.GetValues(vals);

                            models.Add(factory.CreateModel(vals));
                        }

                        return models;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }
        public TModel GetData(int? id)
        {
            if (!id.HasValue) return default;

            try
            {
                connection.Open();

                using (var command = CreateSelectCommand(connection, id))
                {
                    command.Prepare();

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        reader.Read();

                        object[] vals = new object[reader.FieldCount];

                        reader.GetValues(vals);

                        return factory.CreateModel(vals);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }
        public virtual void SetData(TModel model)
        {
            try
            {
                connection.Open();

                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = CreateUpdateCommand(connection, model))
                        {
                            command.Prepare();
                            command.Transaction = trans;

                            var id = (int?)command.ExecuteScalar();

                            if (!model.Id.HasValue)
                                model.Id = id;
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }
        protected virtual IDbCommand CreateSelectCommand(IDbConnection connection, int? id = default)
        {
            var command = connection.CreateCommand();

            var table = typeof(TModel)
                .GetCustomAttributes(typeof(TableAttribute), false)
                .Select(ta => ((TableAttribute)ta).Name)
                .First();

            command.CommandText = $"SELECT * FROM {table}";

            if (id.HasValue)
                command.CommandText += $" WHERE Id = {id.Value}";

            return command;
        }
        protected virtual IDbCommand CreateUpdateCommand(IDbConnection connection, TModel model)
        {
            var command = connection.CreateCommand();

            var table = typeof(TModel)
              .GetCustomAttributes(typeof(TableAttribute), false)
              .Select(ta => ((TableAttribute)ta).Name)
              .First();

            var properties = typeof(TModel).GetProperties();

            IEnumerable<KeyValuePair<string, object>> valueProps = properties
                .Where(p => p.Name != nameof(model.Id))
                .Select(p => new KeyValuePair<string, object>(p.Name, p.GetValue(model)));

            if (model.Id.HasValue)
            {
                var colValues = new StringBuilder();
                valueProps.Select(k => k.Value is string || k.Value is DateTime ? $"{k.Key} = '{k.Value}'," : $"{k.Key} = {k.Value},")
                    .ToList()
                    .ForEach(s => colValues.Append(s));

                command.CommandText = $"UPDATE {table} SET {colValues.ToString().TrimEnd(',')} WHERE Id = {model.Id.Value}";
            }
            else
            {
                var columns = new StringBuilder();

                valueProps.Select(k => $"{k.Key},")
                    .ToList()
                    .ForEach(s => columns.Append(s));

                var values = new StringBuilder();

                valueProps.Select(k => k.Value is string || k.Value is DateTime ? $"'{k.Value}'," : $"{k.Value},")
                    .ToList()
                    .ForEach(v => values.Append(v));

                command.CommandText = $"INSERT INTO {table}" +
                    $" ({columns.ToString().TrimEnd(',')})" +
                    $" VALUES ({values.ToString().TrimEnd(',')}); SELECT @@IDENTITY;";
            }

            return command;
        }
        public Task<IList<TModel>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }
        public Task<TModel> GetDataAsync(int? id)
        {
            return Task.Run(() => GetData(id));
        }
        public Task SetDataAsync(TModel model)
        {
            return Task.Run(() => SetData(model));
        }
    }
}
