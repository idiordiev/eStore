using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eStore.WebMVC.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            GoodsInCart = new List<GoodsViewModel>();
        }

        public int Id { get; set; }
        public string IdentityId { get; set; }

        [MaxLength(120)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [MaxLength(120)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        public ICollection<GoodsViewModel> GoodsInCart { get; set; }
    }
}