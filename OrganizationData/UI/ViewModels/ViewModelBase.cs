using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OrganizationData.UI
{
    public class ViewModelBase<TModel> : ITypedList, IListSource, ICurrencyManagerProvider
        where TModel : class, IModelWrapper
    {
        private void CurrencyManager_PositionChanged(object sender, EventArgs e)
        {
            OnPositionChanged();
        }
        protected virtual void OnPositionChanged()
        {
            
        }
        public bool ContainsListCollection => true;
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(TModel));
        }
        public IList GetList()
        {
            return DataSource;
        }
        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return typeof(TModel).Name;
        }
        public virtual CurrencyManager GetRelatedCurrencyManager(string dataMember = default)
        {
            return DataSource?.CurrencyManager;
        }
        protected BindingSource DataSource { get; set; }
        public CurrencyManager CurrencyManager => GetRelatedCurrencyManager();
    }
}
