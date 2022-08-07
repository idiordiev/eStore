using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eStore.WebMVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not equal.")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}