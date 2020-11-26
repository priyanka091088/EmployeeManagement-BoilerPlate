using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using EmployeeManagement.Configuration;
using EmployeeManagement.Web;

namespace EmployeeManagement.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class EmployeeManagementDbContextFactory : IDesignTimeDbContextFactory<EmployeeManagementDbContext>
    {
        public EmployeeManagementDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EmployeeManagementDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            EmployeeManagementDbContextConfigurer.Configure(builder, configuration.GetConnectionString(EmployeeManagementConsts.ConnectionStringName));

            return new EmployeeManagementDbContext(builder.Options);
        }
    }
}
