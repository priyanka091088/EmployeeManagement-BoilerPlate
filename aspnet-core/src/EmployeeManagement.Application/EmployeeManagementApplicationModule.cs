using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmployeeManagement.Authorization;

namespace EmployeeManagement
{
    [DependsOn(
        typeof(EmployeeManagementCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class EmployeeManagementApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<EmployeeManagementAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(EmployeeManagementApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
