using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using EmployeeManagement.Authorization.Users;
using EmployeeManagement.Domain;
using EmployeeManagement.Services.DTO;
using EmployeeManagement.Users.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Employees
{
    public class EmployeeAppService:AsyncCrudAppService<Employee,EmployeeDTO>,IEmployeeAppService
    {
        public EmployeeAppService(IRepository<Employee> repository) :base(repository)
        {
        }
    }
}
