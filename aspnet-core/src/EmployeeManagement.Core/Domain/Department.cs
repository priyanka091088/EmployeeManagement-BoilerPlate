using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Domain
{
    public class Department:Entity
    {
        [Required]
        public string DepartName { get; set; }
    }
}
