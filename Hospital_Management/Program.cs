using Hospital_Management.CustomMiddleware;

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

            // Inject All Dependency
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IEmailservice, EmailServices>();
            builder.Services.AddScoped<IDoctorDepartment, DoctorDepartmentService>();
            builder.Services.AddScoped<IPasswordService,PasswordServices>();
            builder.Services.AddScoped<IotpService,OTPServices>();
            builder.Services.AddScoped<IPatient,PatientServices>();
            builder.Services.AddScoped<IAppoiment,AppoimentServices>();
            
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