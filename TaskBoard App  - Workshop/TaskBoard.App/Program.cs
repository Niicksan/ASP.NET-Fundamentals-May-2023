namespace TaskBoard.App
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using TaskBoard.Data;
    using TaskBoard.Services;
    using TaskBoard.Services.Interfaces;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add connection string
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Adding DbContext allows us to take instance of DbContext in the entire application
            builder.Services.AddDbContext<TaskBoardDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Add services to the container.
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Authorize Task Controller
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/login";
            });

            builder.Services
                 .AddDefaultIdentity<IdentityUser>(options =>
                 {
                     options.SignIn.RequireConfirmedAccount = builder.Configuration
                        .GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                     options.Password.RequireDigit = builder.Configuration
                        .GetValue<bool>("Identity:Password:RequireDigit");
                     options.Password.RequireNonAlphanumeric = builder.Configuration
                        .GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                     options.Password.RequireUppercase = builder.Configuration
                        .GetValue<bool>("Identity:Password:RequireUppercase");
                     options.Password.RequireLowercase = builder.Configuration
                        .GetValue<bool>("Identity:Password:RequireLowercase");
                     options.Password.RequiredLength = builder.Configuration
                        .GetValue<int>("Identity:Password:RequiredLength");
                 })
                 .AddEntityFrameworkStores<TaskBoardDbContext>();

            builder.Services.AddScoped<IBoardService, BoardService>();
            builder.Services.AddScoped<ITaskService, TaskService>();

            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
