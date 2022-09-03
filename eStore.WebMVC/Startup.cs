using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using eStore.ApplicationCore.Interfaces;
using eStore.ApplicationCore.Interfaces.Data;
using eStore.ApplicationCore.Services;
using eStore.Email;
using eStore.Email.Interfaces;
using eStore.Infrastructure.Data;
using eStore.Infrastructure.Data.UnitOfWork;
using eStore.Infrastructure.Identity;
using eStore.Invoice;
using eStore.WebMVC.DI;
using eStore.WebMVC.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace eStore.WebMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Application"));
            });
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Identity"));
            });
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

            services.AddEntityServices();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<SmtpClient>(sp =>
            {
                SmtpClientOptions options = sp.GetRequiredService<IOptions<SmtpClientOptions>>().Value;
                string password = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                SmtpClient smtpClient = new SmtpClient(options.Address, options.Port);
                smtpClient.Credentials = new NetworkCredential(options.UserName, password);
                return smtpClient;
            });
            services.Configure<SmtpClientOptions>(Configuration.GetSection(SmtpClientOptions.SmtpClient));
            services.AddScoped<IHtmlEmailSender, HtmlEmailSender>();
            services.AddScoped<IEmailService, EmailService>();
            
            services.AddScoped<IAttachmentService, AttachmentService>();
            
            services.AddAutoMapper(typeof(AutomapperProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}