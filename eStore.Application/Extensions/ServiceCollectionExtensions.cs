using eStore.Application.Interfaces.Services;
using eStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eStore.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGoodsService, GoodsService>();
        services.AddScoped<IGamepadService, GamepadService>();
        services.AddScoped<IKeyboardService, KeyboardService>();
        services.AddScoped<IMouseService, MouseService>();
        services.AddScoped<IMousepadService, MousepadService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
    }
}