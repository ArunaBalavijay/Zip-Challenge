using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Zip.Challenge.Common.Services;

namespace Zip.Challenge.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ZipWebHost.CreateHostBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureHostBuilder()
                //.UseRabbitMq()
                .Build()
                .Run();
        }
    }
}
