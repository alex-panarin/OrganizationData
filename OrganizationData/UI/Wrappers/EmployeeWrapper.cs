using OrganizationData.Models;
using OrganizationData.UI.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OrganizationData.UI
{
    public class EmployeeWrapper : ValidationWrapper<Employee>
    {
        public EmployeeWrapper(
            Employee model,
            IModeDataValidation<Employee> validation = default)
            :base(model, validation)
        {

        }

        [DisplayName("Фамилия")]
        public string LastName { get => GetValue<string>(); set => SetValue(value); }

        [DisplayName("Имя")]
        public string FirstName { get => GetValue<string>(); set => SetValue(value); }

        [DisplayName("Отчество")]
        public string FatherName { get => GetValue<string>(); set => SetValue(value); }

        [DisplayName("День рождения")]
        public DateTime? BirthDate { get => GetValue<DateTime?>(); set => SetValue(value); }

        [DisplayName("Паспорт серия")]
        public int PasportSerial { get => GetValue<int>(); set => SetValue(value); }

        [DisplayName("Паспорт номер")]
        public int PasportNumber { get => GetValue<int>(); set => SetValue(value); }

        [DisplayName("Примечание")]
        public string Notes { get => GetValue<string>(); set => SetValue(value); }

        protected override IEnumerable<string> ValidateProperty(string propertyName, object newValue)
        {
            //Let's validate BirthDate property
            switch(propertyName)
            {
                case nameof(BirthDate):
                    
                    if(newValue == null)
                    {
                        yield return "День рождения не может быть NULL.";
                    }
                                       
                    if (newValue is string)
                    {
                        try
                        {
                            DateTime.Parse(newValue.ToString());
                        }
                        catch (FormatException x) 
                        {
                            throw x; // Catched in base class
                        }
                    }
                    
                    yield break;
            }

        }

    }
}