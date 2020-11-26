using System.Threading.Tasks;
using Abp.Application.Services;
using EmployeeManagement.Sessions.Dto;

namespace EmployeeManagement.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
