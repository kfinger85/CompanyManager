using Microsoft.AspNetCore.Server.Kestrel.Core;


namespace CompanyManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.ListenLocalhost(7262, o => o.Protocols = HttpProtocols.Http1AndHttp2);
                    })
                    .UseStartup<Startup>();
                });
    }

    
}
