using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCore
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

            var connString = Configuration["ConnectionStrings:Default"];
            services.AddDbContext<GroceryDbContext>(option => option.UseSqlServer(connString));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<GroceryDbContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

            });

            services.ConfigureApplicationCookie(options =>
            {

                options.LoginPath = "/Identity/Signin";
                options.AccessDeniedPath = "/Identity/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(5);

            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserPolicy", p =>
                {
                    p.RequireClaim("Country", "india").RequireRole("User");
                });

                options.AddPolicy("AdminPolicy", p =>
                {
                    p.RequireClaim("Country", "All").RequireRole("Admin");
                });
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GroceryDbContext context)
        {
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                    pattern: "{controller=Identity}/{action=Signin}/{id?}");
            });
        }
    }
}
