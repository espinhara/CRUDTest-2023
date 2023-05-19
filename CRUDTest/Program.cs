using CRUDTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Authentication.Cookies;
using CRUDTest.Helpers;
using CRUDTest.Interfaces;
using CRUDTest.Services;

namespace CRUDTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme
            ).AddCookie(option => {
                option.LoginPath = "/Access/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(20);

            });
            builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(
                builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value    
            ));
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Access}/{action=Login}/{id?}");

            app.Run();
        }
    }
}