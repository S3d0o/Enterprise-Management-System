using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Data.Repositories.Classes;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Data.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentaion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region DI Container

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //Life Times [object] => AddTransient, AddScoped, AddSingleton
            //AddDbContext => Scoped for DbContext
            builder.Services.AddScoped<AppDbContext /*when any instace of AppDbContext will be created it will be on the CLR */ >();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseSqlServer("Connection string ");
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnectionString"]); // first way
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnectionString"]); // second way
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")); // third way, better way if u named the section "ConnectionString"

            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService,DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            #endregion

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
