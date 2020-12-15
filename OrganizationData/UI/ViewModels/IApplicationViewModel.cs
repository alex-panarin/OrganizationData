namespace OrganizationData.UI.ViewModels
{
    public interface IApplicationViewModel
    {
        EmployeeViewModel Employees { get; }
        OrganizationViewModel Organizations { get; }

        void ExportToCSV(string fileName);
        void ImportFromCSV(string fileName);
        void LoadFromDatabase(string connectionString);
    }
}