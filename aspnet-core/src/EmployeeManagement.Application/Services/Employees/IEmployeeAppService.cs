using Abp.Application.Services;
using Abp.Application.Services.Dto;
using EmployeeManagement.Services.DTO;
using EmployeeManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Services.Employees
{
    public interface IEmployeeAppService: IAsyncCrudAppService<EmployeeDTO>
    {
    }
}
