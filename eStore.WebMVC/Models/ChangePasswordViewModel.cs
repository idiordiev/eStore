using System.ComponentModel.DataAnnotations;

namespace eStore.WebMVC.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords are not equal.")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}