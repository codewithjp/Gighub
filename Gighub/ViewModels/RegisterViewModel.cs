using System;
using System.ComponentModel.DataAnnotations;

namespace Gighub.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be atleast {2} characters long", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confrim password must match with password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confrim Password")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Role Name")]
        [Required]
        public string RoleName { get; set; }
    }
}
