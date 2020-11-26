using Abp.Application.Services;
using EmployeeManagement.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Services
{
    public interface IDepartmentAppService: IAsyncCrudAppService<DepartmentDTO>
    {
        //void CreateDepartment(DepartmentDTO input);
    }
}
