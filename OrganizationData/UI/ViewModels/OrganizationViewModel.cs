using OrganizationData.Data;
using OrganizationData.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OrganizationData.UI
{
    public class OrganizationViewModel : ViewModelBase<OrganizationWapper>
    {
        private readonly IDataProvider<Organization> _provider;
        private readonly IModeDataValidation<Organization> _validation;
        public OrganizationViewModel(
            IDataProvider<Organization> provider, 
            IModeDataValidation<Organization> validation = default) 
        {
            _provider = provider;
            _validation = validation;
        }

        public override CurrencyManager GetRelatedCurrencyManager(string dataMember = default)
        {
            if (DataSource == null)
            {
                DataSource = new BindingSource();
                
                DataSource.PositionChanged += CurrencyManager_PositionChanged;

                DataSource.DataSource = _provider.GetAll()
                    .Select(o => new OrganizationWapper(o, _validation));
                
            }
            
            return base.GetRelatedCurrencyManager(dataMember);
        }
        private void CurrencyManager_PositionChanged(object sender, EventArgs e)
        {
            OnPositionChanged();
        }

        public IChildModel Children { get; set; }

        protected override void OnPositionChanged()
        {
            Children?.SetParent(((IModelWrapper)CurrencyManager.Current).Model.Id);
        }
    }
}
