namespace OrganizationData.Models
{
    internal class OrganizationFactory : ModelFactory<Organization>
    {
        public override Organization CreateModel(object[] values)
        {
            return new Organization
            {
                Id = ConvertValue<int?>(values[0]),
                Name = ConvertValue<string>(values[1]),
                VAT = ConvertValue<long>(values[2]),
                LegalAddress = ConvertValue<string>(values[3]),
                PostalAddress = ConvertValue<string>(values[4]),
                Notes = ConvertValue<string>(values[5]),
            };

        }

        
    }
}
