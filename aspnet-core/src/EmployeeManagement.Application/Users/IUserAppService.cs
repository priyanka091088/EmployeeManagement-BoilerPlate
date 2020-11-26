using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EmployeeManagement.Roles.Dto;
using EmployeeManagement.Users.Dto;

namespace EmployeeManagement.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);
    }
}
