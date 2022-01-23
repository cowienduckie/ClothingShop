using System;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using ClothingShop.BusinessLogic.Repositories;
using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.BusinessLogic.Services;
using ClothingShop.BusinessLogic.Services.Interfaces;
using ClothingShop.Entity.Data;
using ClothingShop.Entity.Entities;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClothingShop
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
            services.AddDbContext<ShopContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("ClothingShopConnectionString")));

            services.AddScoped<IShopRepository, ShopRepository>();

            services.AddControllersWithViews();

            services.AddMvc();

            //Identity Register
            services.AddIdentity<Users, Roles>()
                .AddEntityFrameworkStores<ShopContext>()
                .AddDefaultTokenProviders();

            //Identity Options
            services.Configure<IdentityOptions>(options =>
            {
                //Config Password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                //Config Lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //Config User
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                //Config Login
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            //Config Cookie
            services.ConfigureApplicationCookie(options =>
            {
                // options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login/";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(5);
            });

            //Add authorization policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => { policy.RequireRole("Admin"); });
                options.AddPolicy("User", policy => { policy.RequireRole("User"); });
            });

            //Send email service register
            services.AddOptions();
            var mailSettings = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailSettings);
            services.AddTransient<IEmailService, EmailService>();

            //Notyf
            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 5;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopRight;
            });
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
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    "MemberShip",
                    "{controller=Membership}/{action=Index}");
                endpoints.MapControllerRoute(
                    "VoucherList",
                    "{controller=Membership}/{action=VoucherList}");
                endpoints.MapControllerRoute(
                    "Report",
                    "{controller=Report}/{action=Billing}");
            });

            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<ShopContext>();
            context.Database.EnsureCreated();

            app.UseNotyf();
        }
    }
}