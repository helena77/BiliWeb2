using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using BiliWeb2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BiliWeb2.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using BiliWeb2.Backend.Email;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BiliWeb2
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
            #region Session

            // IDistributedCache memory caches. 
            // The IDistributedCache implementation is used as a backing store for session.
            services.AddDistributedMemoryCache();

            //A call to AddSession
            // Session uses a cookie to track and identify requests from a single browser. 
            // By default, this cookie is named .AspNetCore.Session, and it uses a path of /. 
            // Because the cookie default doesn't specify a domain, 
            // it isn't made available to the client-side script on the page (because HttpOnly defaults to true).
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                // The app uses the IdleTimeout property to determine how long a session can be idle before its contents
                // in the server's cache are abandoned. 
                // This property is independent of the cookie expiration.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });

            #endregion Session

            #region DbContext

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            #endregion DbContext

            #region Identity

            services.AddIdentity<TechnicianModel, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                // By default, Identity requires that passwords contain an uppercase character,
                // lowercase character, a digit, and a non-alphanumeric character.
                // Passwords must be at least six characters long.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            #endregion Identity

            #region Cookie

            // Configure the app's cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "YourAppCookieName";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            #endregion Cookie

            services.AddSingleton<IEmailSender, EmailSender>();

            #region Views and Controllers

            services.AddControllersWithViews();
            services.AddRazorPages();

            #endregion Views and Controllers

            #region Password Hashing

            // Password Hashing
            services.Configure<PasswordHasherOptions>(option =>
            {
                option.IterationCount = 12000;
            });

            #endregion Password Hashing
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            // A call to UseSession
            // Call UserSession after UseRouting and before UseEndpoints
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
