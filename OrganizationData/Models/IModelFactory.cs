namespace OrganizationData.Models
{
    internal interface IModelFactory<TModel>
    {
        TModel CreateModel(object[] values);
    }
}
