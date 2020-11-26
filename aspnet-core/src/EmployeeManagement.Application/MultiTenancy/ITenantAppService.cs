using Abp.Application.Services;
using EmployeeManagement.MultiTenancy.Dto;

namespace EmployeeManagement.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

