using CollegeApp.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Modal
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage ="Student name is required")]
        [StringLength(100)]
        public string StudentName { get; set; }
        [EmailAddress]
        public string StudentEmail { get; set; }
        [Required]
        public string Address { get; set; }
        public string DOB { get; set; }

    }
}
