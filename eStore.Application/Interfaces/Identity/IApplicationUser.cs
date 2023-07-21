namespace eStore.Application.Interfaces.Identity;

public interface IApplicationUser
{
    string Id { get; set; }
    int CustomerId { get; set; }
    string Email { get; set; }
}