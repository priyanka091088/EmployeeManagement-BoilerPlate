using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using EmployeeManagement.Authorization.Roles;
using EmployeeManagement.Authorization.Users;
using EmployeeManagement.MultiTenancy;
using EmployeeManagement.Domain;

namespace EmployeeManagement.EntityFrameworkCore
{
    public class EmployeeManagementDbContext : AbpZeroDbContext<Tenant, Role, User, EmployeeManagementDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        
        public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options)
            : base(options)
        {
        }
    }
}
