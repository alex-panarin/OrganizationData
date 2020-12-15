using OrganizationData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganizationData.Data
{
    public interface IDataProvider<TModel>
        where TModel : IModel
    {
        IList<TModel> GetAll();
        TModel GetData(int? id);
        void SetData(TModel model);
        Task<IList<TModel>> GetAllAsync();
        Task<TModel> GetDataAsync(int? id);
        Task SetDataAsync(TModel model);
    }

    
}