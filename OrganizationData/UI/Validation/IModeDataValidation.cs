namespace OrganizationData.UI
{
    public interface IModeDataValidation<TModel>
    {
        void ValidateDataAnnotations(TModel model, object value, string property);
        void ValidateDataAnnotations(TModel model);
    }
}
