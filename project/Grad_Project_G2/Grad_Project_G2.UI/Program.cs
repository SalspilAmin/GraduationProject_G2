using Grad_Project_G2.BLL.Services;
using Grad_Project_G2.BLL.Services.Interfaces;
using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Repositories.Interface;
using Grad_Project_G2.DAL.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
namespace Grad_Project_G2.UI

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //Add Connection 
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IGradeService, GradeService>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
