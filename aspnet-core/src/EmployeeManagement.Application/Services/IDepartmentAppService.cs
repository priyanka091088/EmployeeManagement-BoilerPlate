using Abp.Application.Services;
using EmployeeManagement.Services.DTO;
using EmployeeManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Services
{
    public interface IDepartmentAppService: IAsyncCrudAppService<DepartmentDTO,int, GetAllDepartmentDTO, CreateDepartmentDTO,DepartmentDTO>
    {
        //void CreateDepartment(DepartmentDTO input);
    }
}
