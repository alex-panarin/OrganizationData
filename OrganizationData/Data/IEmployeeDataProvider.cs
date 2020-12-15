using OrganizationData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrganizationData.Data
{
    public interface IEmployeeDataProvider : IDataProvider<Employee>
    {
        IEnumerable<Employee> GetChildren(int? parentId);
        Task<IEnumerable<Employee>> GetChildrenAsync(int? id);
        void UpdateChildren(IEnumerable<Employee> employees, int organizationId);
        
    }
}