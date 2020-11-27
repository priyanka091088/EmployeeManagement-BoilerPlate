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
    public class EmployeeAppService : AsyncCrudAppService<Employee, EmployeeDTO,int,GetAllEmployeesDTO,CreateEmployeeDTO>, IEmployeeAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<Employee> _employee;
        public EmployeeAppService(IRepository<Employee> repository, UserManager userManager, IRepository<Employee> employee) : base(repository)
        {
            _userManager = userManager;
            _employee = employee;
        }

        public override async Task<EmployeeDTO> CreateAsync(CreateEmployeeDTO input)
        {
            CheckCreatePermission();
            var user = new User();
            var employees = new Employee();
            user.UserName = input.Email;
            user.EmailAddress = input.Email;
            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            var password = input.Name.ToUpper() + input.Surname + "@123";

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                employees.Name = input.Name;
                employees.Surname = input.Surname;
                employees.Address = input.Address;
                employees.Email = input.Email;
                employees.Qualification = input.Qualification;
                employees.ContactNo = input.ContactNo;
                employees.DepartId = input.DepartId;
                _employee.Insert(employees);
            }
            CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(employees);
            //throw new NotImplementedException();
        }
        protected override EmployeeDTO MapToEntityDto(Employee emp)
        {
            var employeeDto = base.MapToEntityDto(emp);
            return employeeDto;
        }
        public Task<PagedResultDto<EmployeeDTO>> GetAllAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }
    }
}
