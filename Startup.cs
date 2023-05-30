using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Repositories;
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

            services.AddControllersWithViews();
            
            services.AddDbContext<CompanyManagerContext>(options => options
                .UseMySql(Configuration.GetConnectionString(connectionStringKey), 
                    new MySqlServerVersion(new Version(8, 0, 26)))
            );
            services.AddScoped<IQualificationRepository, QualificationRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>(); 

            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000") // React app's url
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });  
                     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CompanyManagerContext dbContext)
        {
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

                // Apply pending migrations and create the database
                // dbContext.Database.Migrate();
                dbContext.Database.EnsureCreated();

                app.UseStaticFiles(); // For the wwwroot folder

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "ClientApp/build")),
                    RequestPath = "" // empty RequestPath means serve from the root
                });

                app.UseCors("AllowReactApp");

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
