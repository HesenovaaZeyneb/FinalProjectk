using System.Configuration;
using Evergreen_Application.Abstractions;

using Evergreen_Application.Abstractions.ShopServices;
using Evergreen_Domain;
using Evergreen_Domain.Helper;
using Evergreen_Persistence.Concretes;
using Evergreen_Persistence.Concretes.Setting;
using Evergreen_Persistence.Concretes.ShopService;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace EverGreenHospital
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                //opt.Password.RequiredLength = 5;
                //opt.User.AllowedUserNameCharacters= "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
                //opt.User.RequireUniqueEmail=true;
                //opt.Lockout.AllowedForNewUsers=false;
                //opt.Lockout.MaxFailedAccessAttempts=3;
                //opt.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromSeconds(1);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
			builder.Services.AddScoped<IDoctorsService, DoctorsService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddScoped<IProductService, Evergreen_Persistence.Concretes.ShopService.ProductService>();
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddScoped<LayoutServicee>();
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            var app = builder.Build();
            

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
            app.Services.AddRoleServices();
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:Secretkey"];

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
