using System.ComponentModel.DataAnnotations;

namespace eStore.WebMVC.Models
{
    public class RegisterViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not equal.")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First name")]
        [MaxLength(120)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")] 
        [MaxLength(120)]
        public string LastName { get; set; }

        [Display(Name = "Phone number")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Postal code")] 
        [MaxLength(10)]
        public string PostalCode { get; set; }
    }
}