using System;

namespace OrganizationData.Models
{
    internal class EmployeeFactory : ModelFactory<Employee>
    {
        public override Employee CreateModel(object[] vals)
        {
            return new Employee
            {
                Id = ConvertValue<int?>(vals[0]),
                LastName = ConvertValue<string>(vals[1]),
                FirstName = ConvertValue<string>(vals[2]),
                FatherName = ConvertValue<string>(vals[3]),
                BirthDate = ConvertValue<DateTime?>(vals[4]),
                PasportSerial = ConvertValue<int>(vals[5]),
                PasportNumber = ConvertValue<int>(vals[6]),
                Notes = ConvertValue<string>(vals[7]),
            };
        }

        
    }
}