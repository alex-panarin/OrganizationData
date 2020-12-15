using OrganizationData.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrganizationData.UI
{
    public class ModelWrapper<TModel> : 
        INotifyPropertyChanged, 
        IModelWrapper
        where TModel : class, IModel
    {
        private readonly IModeDataValidation<TModel> _validation;
        private PropertyDescriptorCollection _properties;
        public ModelWrapper(
            TModel model, 
            IModeDataValidation<TModel> validation)
        {
            Model = model;
            _validation = validation;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IModel Model { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string property = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string property = default)
        {
            try
            {
                _validation?.ValidateDataAnnotations((TModel)Model, value, property);
            
                var pi = GetProperty(property);

                var propType = Nullable.GetUnderlyingType(pi.PropertyType);

                pi?.SetValue(Model, Convert.ChangeType(value, propType));

                OnPropertyChanged(property);
            }
            catch(FormatException)
            {
                throw;
            }
            
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName] string property = default)
        {
            var pi = GetProperty(property);

            return (TValue)pi?.GetValue(Model);
        }

        protected PropertyDescriptor GetProperty(string property)
        {
            if (_properties == null)
            {
                _properties = TypeDescriptor.GetProperties(Model.GetType());
            }
            return _properties[property];
        }
    }
}
