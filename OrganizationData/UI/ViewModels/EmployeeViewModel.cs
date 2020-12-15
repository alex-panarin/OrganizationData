using OrganizationData.Data;
using OrganizationData.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OrganizationData.UI
{
    public class EmployeeViewModel : ViewModelBase<EmployeeWrapper>, IChildModel
    {
        private readonly IEmployeeDataProvider _provider;
        private readonly IModeDataValidation<Employee> _validation;

        public EmployeeViewModel(
            IEmployeeDataProvider provider,
            IModeDataValidation<Employee> validation = default)
        {
            _provider = provider;
            _validation = validation;
        }

        public override CurrencyManager GetRelatedCurrencyManager(string dataMember)
        {
            if(DataSource == null)
            {
                DataSource = new BindingSource();
            }

            return base.GetRelatedCurrencyManager(dataMember);
        }

        public async void SetParent(int? parentId)
        {
            var children = await _provider.GetChildrenAsync(parentId);
            
            DataSource.DataSource = children
                .Select(m => new EmployeeWrapper(m, _validation))
                .ToList();
        }

        internal void ImportEmployees(IEnumerable<Employee> employees, int organizationId)
        {
            employees
                .ToList()
                .ForEach(e => _validation?.ValidateDataAnnotations(e));

            _provider.UpdateChildren(employees, organizationId);

            SetParent(organizationId);
        }
    }
}