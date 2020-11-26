using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using EmployeeManagement.Configuration.Dto;

namespace EmployeeManagement.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : EmployeeManagementAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
