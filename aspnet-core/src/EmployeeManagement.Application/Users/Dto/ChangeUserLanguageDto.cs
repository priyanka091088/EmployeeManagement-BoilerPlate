using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}