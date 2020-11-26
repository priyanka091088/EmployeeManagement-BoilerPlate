
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EmployeeManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Services.DTO
{
    [AutoMap(typeof(Department))]
    public class DepartmentDTO:EntityDto
    {
       
        [Required]
        public string DepartName { get; set; }
    }
}
