using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


using CompanyManager.Repositories;
using CompanyManager.Services;
using CompanyManager.Middleware;
using CompanyManager.Models; 

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Serilog;


using CompanyManager.Logging;

namespace CompanyManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            string connectionStringKey = isWindows ? "DefaultConnection" : "LinuxConnection";

            // services.AddControllersWithViews();
            services.AddControllers(); //  Add only the services for the controllers to the DI container, without the view related services
            services.AddDbContext<CompanyManagerContext>(options => options
                .UseMySql(Configuration.GetConnectionString(connectionStringKey),
                    new MySqlServerVersion(new Version(8, 0, 26)))
            );
            services.AddScoped<IQualificationRepository, QualificationRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>(); 

            services.AddScoped<WorkerService, WorkerService>();
            services.AddScoped<ILogger<ProjectService>, Logger<ProjectService>>();
            services.AddScoped<ProjectService, ProjectService>();
            services.AddScoped<CompanyService, CompanyService>();
            services.AddScoped<QualificationService, QualificationService>();

            services.AddScoped<CompanyInitializer, CompanyInitializer>();

             services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<CompanyManagerContext>()
        .AddDefaultTokenProviders();


            services.AddScoped<ProjectService>(serviceProvider =>
            {
                var context = serviceProvider.GetRequiredService<CompanyManagerContext>();
                var workerService = serviceProvider.GetRequiredService<WorkerService>();
                var logger = serviceProvider.GetRequiredService<ILogger<ProjectService>>();
                return new ProjectService(context, workerService, logger);
            });

  

            services.AddSingleton<ClientIpEnricher>();

            services.AddLogging();


            services.AddHttpContextAccessor();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000") // React app's url
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod()
                            .WithExposedHeaders("Access-Control-Allow-Origin");
                    });
            });
            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.MaxDepth = 64; // Increase the maximum allowed depth
                });
            services.AddControllers();
                services.AddLogging(loggingBuilder =>
                    loggingBuilder.AddSerilog(dispose: true));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CompanyManagerContext dbContext, IHostApplicationLifetime applicationLifetime,
            CompanyInitializer companyInitializer)
        {
            var clientIpEnricher = app.ApplicationServices.GetRequiredService<ClientIpEnricher>();


            Logger.Configure(clientIpEnricher);
            

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            app.UseCors("AllowReactApp");
            app.UseMiddleware<CustomMiddleware>();

            // app.UseHttpsRedirection();
            // app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();

            companyInitializer.Initialize(); // Assuming you have an Initialize method

                // Apply pending migrations and create the database
                // dbContext.Database.Migrate();

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                });

            app.UseEndpoints(endpoints =>
            {
                // Configure your desired endpoints here
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            applicationLifetime.ApplicationStarted.Register(() =>
            {
                Logger.LogInformation("Application started");
            });
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                Logger.LogInformation("Application is stopping");
            });

        }
    }
}
