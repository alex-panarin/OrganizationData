using OrganizationData.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OrganizationData.UI.Validation
{
    public class ValidationWrapper<TModel> : ModelWrapper<TModel>, INotifyDataErrorInfo
        where TModel : class, IModel
    {
        public ValidationWrapper(
            TModel model, 
            IModeDataValidation<TModel> validation = default)
            : base(model, validation)
        {

        }

        private readonly Dictionary<string, List<string>> _propertyErrors
         = new Dictionary<string, List<string>>();

        [Browsable(false)]
        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return
                _propertyErrors.ContainsKey(propertyName)
               ? _propertyErrors[propertyName]
               : null;
        }

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

            OnPropertyChanged(nameof(HasErrors));
        }

        protected void ClearErrors(string propertyName)
        {
            if (_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Remove(propertyName);

                OnErrorsChanged(propertyName);
            }
        }
        /// <summary>
        /// Should be override in derived class to validate given property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        protected virtual IEnumerable<string> ValidateProperty(string propertyName, object currentValue)
        {
            return null;
        }

        protected override void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = default)
        {
            ValidatePropertyInternal(propertyName, value);

            if (HasErrors) return;

            base.SetValue(value, propertyName);
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors[propertyName] = new List<string>();
            }

            if (!_propertyErrors[propertyName].Contains(error))
            {
                _propertyErrors[propertyName].Add(error);

                OnErrorsChanged(propertyName);
            }
        }
        protected void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);

            try
            {
                ValidateCustomErrors(propertyName, currentValue);
            }
            catch (FormatException x)
            {
                AddError(propertyName, x.Message);
            }
        }

        private void ValidateCustomErrors(string propertyName, object currentValue)
        {
            var errors = ValidateProperty(propertyName, currentValue);

            if (errors == null) return;

            foreach (var error in errors)
            {
                AddError(propertyName, error);
            }
        }
    }
    
}
