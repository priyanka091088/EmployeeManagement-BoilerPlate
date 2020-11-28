using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using EmployeeManagement.Authorization;
using EmployeeManagement.Domain;
using EmployeeManagement.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    [AbpAuthorize(PermissionNames.Pages_Departments)]
    public class DepartmentAppService:AsyncCrudAppService<Department,DepartmentDTO,int,GetAllDepartmentDTO,CreateDepartmentDTO,DepartmentDTO>, IDepartmentAppService
    {
        private readonly IRepository<Department> _department;
        public DepartmentAppService(IRepository<Department> repository,IRepository<Department> department):base(repository)
        {
            _department = department;
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
