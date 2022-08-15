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
        
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        
        public string City { get; set; }
        public string Address { get; set; }
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
    }
}