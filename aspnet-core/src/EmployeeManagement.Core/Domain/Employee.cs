using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeManagement.Domain
{
    public class Employee:Entity
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
        [ForeignKey("DepartId ")]
        public Department Department { get; set; }
    }
}
