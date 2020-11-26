using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmployeeManagement.EntityFrameworkCore;
using EmployeeManagement.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace EmployeeManagement.Web.Tests
{
    [DependsOn(
        typeof(EmployeeManagementWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class EmployeeManagementWebTestModule : AbpModule
    {
        public EmployeeManagementWebTestModule(EmployeeManagementEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EmployeeManagementWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(EmployeeManagementWebMvcModule).Assembly);
        }
    }
}