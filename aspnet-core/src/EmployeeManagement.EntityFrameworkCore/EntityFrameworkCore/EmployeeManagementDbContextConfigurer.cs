using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.EntityFrameworkCore
{
    public static class EmployeeManagementDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<EmployeeManagementDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<EmployeeManagementDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
