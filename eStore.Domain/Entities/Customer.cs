using System.Collections.Generic;

namespace eStore.Domain.Entities;

public class Customer : Entity
{
    public string IdentityId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public int ShoppingCartId { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
}