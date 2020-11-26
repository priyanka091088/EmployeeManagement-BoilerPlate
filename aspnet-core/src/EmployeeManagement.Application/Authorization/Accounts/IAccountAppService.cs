using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeManagement.Authorization.Accounts.Dto;

namespace EmployeeManagement.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
