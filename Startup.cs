using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


using CompanyManager.Repositories;
using CompanyManager.Services;
using CompanyManager.Middleware;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Serilog;


using CompanyManager.Logging;
using Microsoft.Extensions.Options;
using MusicProduction.Services;
using MusicProduction.Repositories;
using MusicProduction.Models;

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
        /// <seealso>
        ///In ASP.NET Core, the AddScoped method is an extension method provided by the Microsoft.Extensions.DependencyInjection namespace 
        ///and is used to register a service with a scoped lifetime in the dependency injection container.
        ///  Dependency Inversion Principle, one of the key principles of SOLID. This approach makes it easier to swap out the EmailService for another implementation in the future if required, 
        /// without changing the rest of your code that depends on the service.
        /// </seealso>
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

            services.AddScoped<ILogger<ProjectService>, Logger<ProjectService>>();

            /*
            The interfaces (IWorkerService, IProjectService, ICompanyService, IQualificationService) will be resolved automatically by the 
            dependency injection container based on their corresponding implementations 
            (WorkerService, ProjectService, CompanyService, QualificationService) registered above.
            */
            services.AddScoped<WorkerService>();
            services.AddScoped<ProjectService>();   
            services.AddScoped<CompanyService>();
            services.AddScoped<QualificationService>();

            /*
            By keeping only these registrations, the dependency injection container will correctly resolve the IProductService interface to the 
            ProductService class and the IProductRepository interface to the ProductRepository class when needed.
            */
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();



            services.AddScoped<CompanyInitializer, CompanyInitializer>();

        /*
        This method sets up the ASP.NET Identity system with the specified user and role types. 
        In this case, it configures the system to use IdentityUser as the user type and IdentityRole as the role type. 
        These types are provided by the ASP.NET Identity framework.
        */
            services.AddIdentity<IdentityUser, IdentityRole>()
            //This method configures the storage mechanism for user and role data using Entity Framework. It specifies 
            // that the CompanyManagerContext class should be used as the database context for storing and retrieving user and role information.
                    .AddEntityFrameworkStores<CompanyManagerContext>()
                    .AddDefaultTokenProviders();


            services.AddScoped<UserManager<IdentityUser>>();

            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
            services.AddScoped<IEmailService, EmailService>(); // Add the email service
            


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
