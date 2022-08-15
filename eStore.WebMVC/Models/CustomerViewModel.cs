using System.Collections.Generic;
using eStore.WebMVC.Models.Goods;

namespace eStore.WebMVC.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public ICollection<GoodsViewModel> GoodsInCart { get; set; }

        public CustomerViewModel()
        {
            GoodsInCart = new List<GoodsViewModel>();
        }
    }
}