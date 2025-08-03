using Hospital_Management.CommonMethod_Class;
using Hospital_Management.CustomMiddleware;
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
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<HospitalDbContext>(options =>          
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IDepartmentService, DepartmentServices>();
            builder.Services.AddScoped<IAdminService, AdminServices>();
            builder.Services.AddScoped<IDoctorServices, DoctorService>();
            builder.Services.AddScoped<IEmailservices, EmailServices>();
            builder.Services.AddScoped<IDoctorDepartment, DoctorDepartmentServices>();
            
            var app = builder.Build();
            
            SessionUtility.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error/General");
            }
            else
            {
                app.UseExceptionHandler("/Error/General");
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseSession();

            app.UseMiddleware<AuthenticationMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}"
            );

            app.Run();

        }
    }
}