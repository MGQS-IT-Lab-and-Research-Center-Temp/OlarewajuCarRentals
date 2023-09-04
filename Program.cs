using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Carentals.MapperConfig;
using CarRentals.Context;
using CarRentals.Repository.Implementation;
using CarRentals.Repository.Implementations;
using CarRentals.Repository.Interface;
using CarRentals.Repository.Interfaces;
using CarRentals.Service.Interface;
using CarRentals.Services.Implementation;
using CarRentals.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace CarRentals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(MapConfig));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopRight;
            });


            builder.Services.AddDbContext<CarRentalsContext>(option =>
           option.UseMySQL(builder.Configuration.GetConnectionString("CarRentalsContext")));


            builder.Services.AddScoped<DbInitializer>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                           .AddCookie(config =>
                           {
                               config.LoginPath = "/home/login";
                               config.Cookie.Name = "CarRentals";
                               config.ExpireTimeSpan = TimeSpan.FromDays(1);
                               config.AccessDeniedPath = "/home/privacy";
                           });
            builder.Services.AddHttpContextAccessor();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }




            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.SeedToDatabase();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNotyf();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}