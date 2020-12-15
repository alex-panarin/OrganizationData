using LINQtoCSV;
using OrganizationData.Models;
using System.Collections.Generic;

namespace OrganizationData.Data
{
    public class EmployeeCsvImpEx : ICsvImpEx<Employee>
    {
        public void Export(IEnumerable<Employee> models, string fileName)
        {
            CsvContext cc = new CsvContext();
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                MaximumNbrExceptions = 50,
                IgnoreUnknownColumns = true,
                EnforceCsvColumnAttribute = true,
                UseFieldIndexForReadingData = true

            };

            cc.Write(models, fileName, inputFileDescription);
        }

        public IEnumerable<Employee> Import(string fileName)
        {
            CsvContext cc = new CsvContext();
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                MaximumNbrExceptions = 50,
                IgnoreUnknownColumns = true,
                UseFieldIndexForReadingData = true
            };

            return cc.Read<Employee>(fileName, inputFileDescription);
        }
    }
}
