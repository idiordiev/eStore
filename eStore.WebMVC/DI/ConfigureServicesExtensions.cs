using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.DomainServices;
using eStore.ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eStore.WebMVC.DI
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddEntityServices(this IServiceCollection services)
        {
            services.AddScoped<IGoodsService, GoodsService>();
            services.AddScoped<IGamepadService, GamepadService>();
            services.AddScoped<IKeyboardService, KeyboardService>();
            services.AddScoped<IMouseService, MouseService>();
            services.AddScoped<IMousepadService, MousepadService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            
            return services;
        }
    }
}