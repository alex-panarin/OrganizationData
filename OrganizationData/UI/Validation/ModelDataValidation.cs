using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrganizationData.UI.Validation
{
    public class ModelDataValidation<TModel> : IModeDataValidation<TModel>
    {
        private readonly PropertyDescriptorCollection _properties;
        public ModelDataValidation()
        {
            _properties = TypeDescriptor.GetProperties(typeof(TModel));
        }
        public void ValidateDataAnnotations(TModel model, object value, string property)
        {
            var context = new ValidationContext(model) { MemberName = property };

            var propType = Nullable.GetUnderlyingType(_properties[property].PropertyType);

            var newValue = Convert.ChangeType(value, propType);

            var results = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(newValue, context, results))
            {
                StringBuilder errors = new StringBuilder();

                results.ForEach(e => errors.Append($"{e.ErrorMessage}\r\n"));

                throw new ValidationException(errors.ToString());
            }
        }

        public void ValidateDataAnnotations(TModel model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            if(!Validator.TryValidateObject(model, context, results, false))
            {
                StringBuilder errors = new StringBuilder();

                results.ForEach(e => errors.Append($"{e.ErrorMessage}\r\n"));

                throw new ValidationException(errors.ToString());
            }
        }
    }
}
