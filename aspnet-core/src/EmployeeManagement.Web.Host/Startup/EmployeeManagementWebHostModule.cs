using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using EmployeeManagement.Configuration;

namespace EmployeeManagement.Web.Host.Startup
{
    [DependsOn(
       typeof(EmployeeManagementWebCoreModule))]
    public class EmployeeManagementWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EmployeeManagementWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EmployeeManagementWebHostModule).GetAssembly());
        }
    }
}
