using Hospital_Management.CommonMethod_Class;
using Hospital_Management.Interfaces;

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

            builder.Services.AddSingleton<IDepartmentService,DepartmentServices>();
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
                pattern: "{controller=Try}/{action=Index}/{id?}"
            );

            app.Run();

        }
    }
}