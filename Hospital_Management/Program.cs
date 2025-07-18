using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Services;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<HospitalDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSingleton<IDepartmentService, DepartmentServices>();
            builder.Services.AddScoped<IDoctorServices,DoctorService>();

            var app = builder.Build();

            app.UseStatusCodePagesWithReExecute("/Error/InvalidURL");
            app.UseExceptionHandler("/Error/General");

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}"
            );

            app.Run();

        }
    }
}