using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
//using Workshoppservices.data;

namespace Workshoppservices {

    [Authorize]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        // Add services to the container.
        builder.Services.AddControllersWithViews();
        // builder.Services.AddDbContext<dbContext>(p => p.UseSqlServer(_conn));
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);
            options.Cookie.Name = "testApp";
            options.Cookie.Path = "/";
            options.Cookie.HttpOnly = true; //more secure, only the server can read the cookie
            options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            options.Cookie.IsEssential = true;

        });
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
            options.LoginPath = "/accounts/login";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

        });



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

        app.UseRouting();
        app.UseAuthentication();
        app.UseSession();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Accountss}/{action=Index}/{id?}");

        app.Run();
    }
} }