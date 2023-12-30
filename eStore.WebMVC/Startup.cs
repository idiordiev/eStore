using eStore.Application.Extensions;
using eStore.Infrastructure.Extensions;
using eStore.Infrastructure.Identity;
using eStore.WebMVC.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eStore.WebMVC;

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
        services.AddApplication();
        services.AddInfrastructure(Configuration);
        
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("IdentityDbConnection"));
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
        
        services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        services.AddAutoMapper(typeof(AutomapperProfile));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
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
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        });
    }
}