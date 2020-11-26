using Abp.Application.Services.Dto;

namespace EmployeeManagement.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

