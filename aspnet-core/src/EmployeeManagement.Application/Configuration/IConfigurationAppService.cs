using System.Threading.Tasks;
using EmployeeManagement.Configuration.Dto;

namespace EmployeeManagement.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
