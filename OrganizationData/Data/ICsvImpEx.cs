using System.Collections.Generic;

namespace OrganizationData.Data
{
    interface ICsvImpEx<TModel>
    {
        IEnumerable<TModel> Import(string fileName);
        void Export(IEnumerable<TModel> model, string fileName);
    }
}
