using System;
using System.Net;
using System.Net.Mail;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Interfaces.DomainServices;
using eStore.ApplicationCore.Services;
using eStore.Infrastructure.Data;
using eStore.Infrastructure.Data.UnitOfWork;
using eStore.Infrastructure.Identity;
using eStore.Infrastructure.Services.Email;
using eStore.Infrastructure.Services.Invoices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace eStore.WebMVC.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddEntityServices(this IServiceCollection services)
        {
            services.AddScoped<IGoodsService, GoodsService>();
            services.AddScoped<IGamepadService, GamepadService>();
            services.AddScoped<IKeyboardService, KeyboardService>();
            services.AddScoped<IMouseService, MouseService>();
            services.AddScoped<IMousepadService, MousepadService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
        }

        public static void ConfigureApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Application"));
            });
        }

        public static void ConfigureIdentityDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Identity"));
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
                {
                    opt.Password.RequireDigit = true;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredUniqueChars = 2;
                    opt.Password.RequireNonAlphanumeric = false;

                    opt.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddSmtpClient(this IServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                var options = sp.GetRequiredService<IOptions<SmtpClientOptions>>().Value;
                var password = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var smtpClient = new SmtpClient(options.Address, options.Port);
                smtpClient.Credentials = new NetworkCredential(options.UserName, password);
                return smtpClient;
            });
        }

        public static void ConfigureSmtpClientOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SmtpClientOptions>(config.GetSection(SmtpClientOptions.SmtpClient));
        }

        public static void AddHtmlEmailSender(this IServiceCollection services)
        {
            services.AddScoped<IHtmlEmailSender, HtmlEmailSender>();
        }

        public static void AddEmailService(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
        }

        public static void AddAttachmentService(this IServiceCollection services)
        {
            services.AddScoped<IAttachmentService, AttachmentService>();
        }
    }
}