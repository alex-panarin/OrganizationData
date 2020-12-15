using OrganizationData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationData.Data
{
    internal class EmployeeDataProvider : ModelDataProvider<Employee>, IEmployeeDataProvider
    {
        public EmployeeDataProvider(IDbConnection connetion)
            : base(connetion, new EmployeeFactory())
        {

        }

        public IEnumerable<Employee> GetChildren(int? organizationId)
        {
            if (!organizationId.HasValue)
                throw new ArgumentNullException(nameof(organizationId));

            try
            {
                connection.Open();

                using (var command = CreateSelectCommand(connection))
                {
                    command.CommandText += $" INNER JOIN EmployeesOrganizations ON EmployeeId = Id WHERE OrganizationId = {organizationId.Value}";

                    command.Prepare();

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        var list = new List<Employee>();

                        while (reader.Read())
                        {
                            object[] vals = new object[reader.FieldCount];

                            reader.GetValues(vals);

                            list.Add(factory.CreateModel(vals));
                        }

                        return list;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }
        public Task<IEnumerable<Employee>> GetChildrenAsync(int? organizationId)
        {
            return Task.Run(() => GetChildren(organizationId));
        }

        public void UpdateChildren(IEnumerable<Employee> employees, int organizationId)
        {
            try
            {
                connection.Open();

                using(var trans = connection.BeginTransaction())
                {
                    try
                    {
                        var properties = typeof(Employee).GetProperties();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "sp_UpdateEmployeeAndRelations";

                            foreach (var employee in employees)
                            {
                                command.Parameters.Clear();

                                properties
                                    .Where(p => p.Name != nameof(employee.Id))
                                    .Select(p =>
                                    {
                                        var param = command.CreateParameter();
                                        param.ParameterName = $"@{p.Name}";
                                        param.Value = p.GetValue(employee);

                                        return param;
                                    })
                                    .ToList()
                                    .ForEach(p => command.Parameters.Add(p));

                                var orgParam = command.CreateParameter();
                                orgParam.ParameterName = "@OrganizationId";
                                orgParam.Value = organizationId;

                                command.Parameters.Add(orgParam);

                                command.Prepare();
                                command.Transaction = trans;

                                var id = (int?)command.ExecuteScalar();

                                if (!employee.Id.HasValue && id.HasValue)
                                {
                                    employee.Id = id;
                                }
                            }
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

    }
}
