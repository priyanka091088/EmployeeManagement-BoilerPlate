using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using EmployeeManagement.Authorization;
using EmployeeManagement.Authorization.Users;
using EmployeeManagement.Domain;
using EmployeeManagement.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    [AbpAuthorize(PermissionNames.Pages_Departments)]
    public class DepartmentAppService:AsyncCrudAppService<Department,DepartmentDTO,int,GetAllDepartmentDTO,CreateDepartmentDTO,DepartmentDTO>, IDepartmentAppService
    {
        private readonly IRepository<Department> _department;
        private readonly IRepository<Employee> _employee;
        private readonly UserManager _userManager;
        public DepartmentAppService(IRepository<Department> repository,IRepository<Department> department,
            IRepository<Employee> employee, UserManager userManager) :base(repository)
        {
            _department = department;
            _employee = employee;
            _userManager = userManager;
        }
        [AbpAuthorize(PermissionNames.Pages_Users)]
        public override async Task<DepartmentDTO> CreateAsync(CreateDepartmentDTO input)
        {
            var depart = ObjectMapper.Map<Department>(input);
            await _department.InsertAsync(depart);
            CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(depart);
        }
        [AbpAuthorize(PermissionNames.Pages_Users)]
        public override async Task<DepartmentDTO> UpdateAsync(DepartmentDTO input)
        {
            var depart = ObjectMapper.Map<Department>(input);
            await _department.UpdateAsync(depart);

            CurrentUnitOfWork.SaveChanges();

            return await GetAsync(input);
        }
        [AbpAuthorize(PermissionNames.Pages_Users)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var depart = _department.FirstOrDefault(e => e.Id == input.Id);

            var emps = _employee.GetAll().
                        Where(e => e.DepartId == input.Id).ToList();
            IEnumerable<Employee> employeeList = emps;
            foreach (var item in employeeList)
            {
                var user = await _userManager.FindByEmailAsync(item.Email);
                await _userManager.DeleteAsync(user);

                await _employee.DeleteAsync(item.Id);
            }
                await _department.DeleteAsync(depart);

            CurrentUnitOfWork.SaveChanges();
        }
        protected override DepartmentDTO MapToEntityDto(Department depart)
        {
            var departmentDto = base.MapToEntityDto(depart);
            return departmentDto;
        }
    }
}
