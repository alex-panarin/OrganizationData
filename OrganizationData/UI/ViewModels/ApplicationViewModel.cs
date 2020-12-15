using LINQtoCSV;
using OrganizationData.Data;
using OrganizationData.Models;
using OrganizationData.UI.Errors;
using OrganizationData.UI.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OrganizationData.UI.ViewModels
{
    public class ApplicationViewModel : IApplicationViewModel
    {
        private readonly IDisplayErrorInfo _errorInfo;
        private ICsvImpEx<Employee> _csvImpex;

        public ApplicationViewModel(IDisplayErrorInfo errorInfo)
        {
            _errorInfo = errorInfo;
        }

        public EmployeeViewModel Employees { get; private set; }
        public OrganizationViewModel Organizations { get; private set; }

        public void LoadFromDatabase(string connectionString)
        {
            try
            {
                var connection = new SqlConnection(connectionString);

                _csvImpex = new EmployeeCsvImpEx();

                Employees = new EmployeeViewModel(new EmployeeDataProvider(connection), new ModelDataValidation<Employee>());

                Organizations = new OrganizationViewModel(new OrganizationDataProvider(connection))
                {
                    Children = Employees
                };
            }
            catch(Exception x)
            {
                _errorInfo?.ShowError($"Ошибка загрузки данных: {x.Message}");
            }
        }

        public void ImportFromCSV(string fileName)
        {
            if (_csvImpex == null)
            {
                _errorInfo.ShowInfo("Данная операция доступна только после загрузки данных");
                return;
            }

            try
            {
                var employees = _csvImpex.Import(fileName)
                    .ToArray();
                
                Employees.ImportEmployees(employees, ((OrganizationWapper)Organizations.CurrencyManager.Current).Model.Id.Value);
                
            }
            catch(ValidationException ve)
            {
                _errorInfo.ShowError($"Ошибка валидации при импорте данных: {ve.Message}");
            }
            catch(AggregatedException ae)
            {
                StringBuilder exs = new StringBuilder();

                List<Exception> innerExceptionsList =
                    (List<Exception>)ae.Data["InnerExceptionsList"];

                innerExceptionsList.ForEach(x => exs.Append($"{x.Message}\r\n"));
                
                _errorInfo.ShowError($"Ошибка чтения данных при импорте: {exs}");
                
            }
            catch (Exception e)
            {
                _errorInfo.ShowError($"Ошибка импорта данных: {e.Message}");
            }
        }

        public void ExportToCSV(string fileName)
        {
            if (_csvImpex == null)
            {
                _errorInfo.ShowInfo("Данная операция доступна только после загрузки данных");
                return;
            }

            try
            {
                var employees = Employees.GetList()
                    .OfType<EmployeeWrapper>()
                    .Select(e => (Employee)e.Model);

                _csvImpex.Export(employees, fileName);
            }
            catch (Exception e)
            {
                _errorInfo.ShowError($"Ошибка экспорта данных: {e.Message}"); 
            }

        }

    }
}
