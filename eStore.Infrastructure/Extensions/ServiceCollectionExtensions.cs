using System;
using System.Net;
using System.Net.Mail;
using eStore.Application.Interfaces;
using eStore.Application.Interfaces.Data;
using eStore.Application.Interfaces.Services;
using eStore.Application.Services;
using eStore.Infrastructure.Persistence;
using eStore.Infrastructure.Services.Email;
using eStore.Infrastructure.Services.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace eStore.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDbContext(configuration);

        services.AddUnitOfWork();
        services.AddSmtpClient();
        services.AddHtmlEmailSender();
        services.AddEmailService();
        services.AddAttachmentService();
    }

    private static void AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("Application"));
        });
    }

    private static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddSmtpClient(this IServiceCollection services)
    {
        services.AddOptions<SmtpClientOptions>()
            .BindConfiguration(SmtpClientOptions.SmtpClient);
        
        services.AddScoped(sp =>
        {
            var options = sp.GetRequiredService<IOptions<SmtpClientOptions>>().Value;
            var password = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var smtpClient = new SmtpClient(options.Address, options.Port);
            smtpClient.Credentials = new NetworkCredential(options.UserName, password);
            
            return smtpClient;
        });
    }

    private static void AddHtmlEmailSender(this IServiceCollection services)
    {
        services.AddScoped<IHtmlEmailSender, HtmlEmailSender>();
    }

    private static void AddEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
    }

    private static void AddAttachmentService(this IServiceCollection services)
    {
        services.AddScoped<IAttachmentService, AttachmentService>();
    }
}