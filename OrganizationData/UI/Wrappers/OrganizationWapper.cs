using OrganizationData.Models;
using System.ComponentModel;

namespace OrganizationData.UI
{
    public class OrganizationWapper : ModelWrapper<Organization>
    {
        public OrganizationWapper(
            Organization model,  
            IModeDataValidation<Organization> validation = default)
            : base(model, validation)
        {
             
        }

        [DisplayName("Наименование")]
        public string Name { get => GetValue<string>(); set => SetValue(value); }

        [DisplayName("ИНН")]
        public long VAT { get => GetValue<long>(); set => SetValue(value); }

        [DisplayName("Юридический адрес")]
        public string LegalAddress { get => GetValue<string>(); set => SetValue(value); }

        [DisplayName("Физический адрес")]
        public string PostalAddress { get => GetValue<string>(); set => SetValue(value); }

        [DisplayName("Примечание")]
        public string Notes { get => GetValue<string>(); set => SetValue(value); }
    }
}
