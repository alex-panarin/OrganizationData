using OrganizationData.Models;
using System.Data;

namespace OrganizationData.Data
{
    internal class OrganizationDataProvider : ModelDataProvider<Organization>
    {
        public OrganizationDataProvider(IDbConnection connection)
            : base(connection, new OrganizationFactory())
        {

        }
    }
}
