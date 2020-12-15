using OrganizationData.UI.Errors;
using OrganizationData.UI.ViewModels;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace OrganizationData
{
    public partial class OrganizationForm : Form
    {
        private IApplicationViewModel _applicationViewModel;
        private DisplayErrorInfo _displayError;
        public OrganizationForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _displayError = new DisplayErrorInfo();
            _applicationViewModel = new ApplicationViewModel(_displayError);

        }

        private void button_load_Click(object sender, EventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["OrganizationData"].ConnectionString;

            _applicationViewModel.LoadFromDatabase(connectionString);

            organizationDataGridView.DataSource = _applicationViewModel.Organizations;
            employeeDataGridView.DataSource = _applicationViewModel.Employees;
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            if (organizationDataGridView.SelectedRows.Count == 0)
            {
                _displayError.ShowInfo("Не выбрана организация для экспорта данных");
                return;
            }

            using (SaveFileDialog fileDialog = new SaveFileDialog())
            {
                fileDialog.Title = "Выгрузка списка сотрудников в CSV файл";
                fileDialog.InitialDirectory = Environment.CurrentDirectory;
                fileDialog.DefaultExt = "*.csv";
                fileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    _applicationViewModel.ExportToCSV(fileDialog.FileName);
                }
            }
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            if (organizationDataGridView.SelectedRows.Count == 0)
            {
                _displayError.ShowInfo("Не выбрана организация для импорта данных");
                return;
            }
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = "Загрузка списка сотрудников в CSV файл";
                fileDialog.InitialDirectory = Environment.CurrentDirectory;
                fileDialog.DefaultExt = "*.csv";
                fileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    _applicationViewModel.ImportFromCSV(fileDialog.FileName);
                }
            }
        }
    }
}
