using LibApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Interfaces;
using LibApp.Repositories;

namespace LibApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenresRepository, GenresRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMembershipRepository, MembershipTypeRepository>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        CreateRolesAndUsers(serviceProvider).Wait();
        }
        private async Task CreateRolesAndUsers(IServiceProvider serviceProvider)
        {
            //Init Custom Roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "User", "StoreManager", "Owner" };
            IdentityResult roleResult;
        
            //Creating Roles
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Creating Users with created Roles
            var user = new IdentityUser()
            {
                UserName = Configuration["UserConfig:User:Email"],
                Email = Configuration["UserConfig:User:Email"]
            };
            var owner = new IdentityUser()
            {
                UserName = Configuration["UserConfig:Owner:Email"],
                Email = Configuration["UserConfig:Owner:Email"]
            };
            var storeManager = new IdentityUser()
            {
                UserName = Configuration["UserConfig:StoreManager:Email"],
                Email = Configuration["UserConfig:StoreManager:Email"]
            };
            user.EmailConfirmed = true;
            owner.EmailConfirmed = true;
            storeManager.EmailConfirmed = true;

            string pwd = Configuration["UserConfig:UserPWD"];

            var _owner = await UserManager.FindByEmailAsync(Configuration["UserConfig:Owner:Email"]);
            var _user = await UserManager.FindByEmailAsync(Configuration["UserConfig:User:Email"]);
            var _storeManager = await UserManager.FindByEmailAsync(Configuration["UserConfig:StoreManager:Email"]);
            
            if(_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(user, pwd);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "User");
                }
            }
            if(_owner == null)
            {
                var createPowerUser = await UserManager.CreateAsync(owner, pwd);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(owner, "Owner");
                }
            }if(_storeManager == null)
            {
                var createPowerUser = await UserManager.CreateAsync(storeManager, pwd);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(storeManager, "StoreManager");
                }
            }
        }
    }
}
