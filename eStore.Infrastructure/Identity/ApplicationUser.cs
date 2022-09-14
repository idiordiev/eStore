using eStore.ApplicationCore.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;

namespace eStore.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public int CustomerId { get; set; }
    }
}