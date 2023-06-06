using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Repositories;
using CompanyManager.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

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
            services.AddScoped<CompanyService, CompanyService>();
            services.AddScoped<ProjectService, ProjectService>();
            services.AddScoped<QualificationService, QualificationService>();
            


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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CompanyManagerContext dbContext)
        {
            dbContext.Database.EnsureDeleted();

            app.UseCors("AllowReactApp");
            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

                // Apply pending migrations and create the database
                // dbContext.Database.Migrate();
            dbContext.Database.EnsureCreated();

                CompanyInitializer companyInitializer = new CompanyInitializer(dbContext);
                companyInitializer.Initialize();
                // app.UseStaticFiles(); // For the wwwroot folder


            app.UseEndpoints(endpoints =>
            {
                // Configure your desired endpoints here
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
