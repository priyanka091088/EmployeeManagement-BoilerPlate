using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using EmployeeManagement.Authorization;
using EmployeeManagement.Domain;
using EmployeeManagement.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Services
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class DepartmentAppService:AsyncCrudAppService<Department,DepartmentDTO>, IDepartmentAppService
    {
        public DepartmentAppService(IRepository<Department> repository):base(repository)
        {
        }
    }
}
