using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RawRabbit.vNext;
using Serilog;
using System.IO;
using Zip.Challenge.Common.RabbitMq;

namespace Zip.Challenge.Common.Services
{
    public static class HostBuilderExtensions
    {
        public static HostBuilder ConfigureHostBuilder(this IHostBuilder builder)
        {
            builder
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    configBuilder
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .UseSerilog();

            return new HostBuilder(builder.Build());
        }

        public static void AddRabbitMq(this IServiceCollection services, IHostEnvironment currentEnvironment)
        {
            services.AddRawRabbit(
                config => config
                    .AddJsonFile("appsettings.rabbitmq.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.rabbitmq.{currentEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            );

            services.AddTransient<IBusRequestClient, BusRequestClient>();
        }
    }
}
