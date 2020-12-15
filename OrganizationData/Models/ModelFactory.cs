using System;

namespace OrganizationData.Models
{
    internal abstract class ModelFactory<TModel> : IModelFactory<TModel>
    {
        public abstract TModel CreateModel(object[] values);

        protected TValue ConvertValue<TValue>(object value)
        {
            var propertyType = Nullable.GetUnderlyingType(typeof(TValue));

            return (TValue)Convert.ChangeType(value, propertyType ?? typeof(TValue));
        }
    }
}
