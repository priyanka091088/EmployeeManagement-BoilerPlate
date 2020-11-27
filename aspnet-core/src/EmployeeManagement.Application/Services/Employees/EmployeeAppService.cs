using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using EmployeeManagement.Authorization;
using EmployeeManagement.Authorization.Roles;
using EmployeeManagement.Authorization.Users;
using EmployeeManagement.Domain;
using EmployeeManagement.Services.DTO;
using EmployeeManagement.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Employees
{
    [AbpAuthorize(PermissionNames.Pages_Employee)]
    public class EmployeeAppService : AsyncCrudAppService<Employee, EmployeeDTO,int,GetAllEmployeesDTO,CreateEmployeeDTO>, IEmployeeAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<Employee> _employee;
        private readonly RoleManager _roleManager;
        public EmployeeAppService(IRepository<Employee> repository, UserManager userManager, IRepository<Employee> employee
            , RoleManager roleManager) : base(repository)
        {
            _userManager = userManager;
            _employee = employee;
            _roleManager = roleManager;
        }

        public override async Task<EmployeeDTO> CreateAsync(CreateEmployeeDTO input)
        {
            CheckCreatePermission();

            var user = new User();
            var employee = ObjectMapper.Map<Employee>(input);

            user.UserName = input.Name;
            user.EmailAddress = input.Email;
            user.Name = input.Name;
            user.Surname = input.Surname;
            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;
            var password = input.Name.ToUpper() + input.Surname + "@123";

            var role = await _roleManager.RoleExistsAsync("Employee");
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var result = await _userManager.FindByEmailAsync(input.Email);
                if (result == null)
            {
                CheckErrors(await _userManager.CreateAsync(user, password));
                if (role == true)
                {
                    CheckErrors(await _userManager.AddToRoleAsync(user, "Employee"));
                    _employee.Insert(employee);
                }
                CurrentUnitOfWork.SaveChanges();
            }
            
            return MapToEntityDto(employee);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected override EmployeeDTO MapToEntityDto(Employee emp)
        {
            var employeeDto = base.MapToEntityDto(emp);
            return employeeDto;
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            
            var emp = _employee.FirstOrDefault(e => e.Id == input.Id);

            var user = await _userManager.FindByEmailAsync(emp.Email);
            await _userManager.DeleteAsync(user);

            await  _employee.DeleteAsync(emp);

            CurrentUnitOfWork.SaveChanges();
        }
        [HttpGet]
        [Route("getAllEmployees")]
        public Task<PagedResultDto<EmployeeDTO>> GetAllAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }
    }
}
