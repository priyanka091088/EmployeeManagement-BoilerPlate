﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using EmployeeManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeManagement.Services.DTO
{
    [AutoMap(typeof(Employee))]
    public class EmployeeDTO:EntityDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Maximum number of characters that can be entered is 20")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Maximum number of characters that can be entered is 20")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Maximum number of characters that can be entered is 20")]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string ContactNo { get; set; }
        public int DepartId { get; set; }
        public virtual Department Department { get; set; }
    }
}
