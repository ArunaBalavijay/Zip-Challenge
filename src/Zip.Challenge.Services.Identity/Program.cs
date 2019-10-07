using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Dto;
using Zip.Challenge.Common.Queries;
using Zip.Challenge.Common.Services;

namespace Zip.Challenge.Services.Identity
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
                .UseRabbitMq()
                .SubscribeToCommand<CreateUser>()
                .SubscribeToCommand<CreateAccount>()
                .SubscribeToQuery<ListUsers, List<User>>()
                .SubscribeToQuery<GetUser, User>()
                .SubscribeToQuery<ListAccounts, List<Account>>()
                .Build()
                .Run();
        }
    }
}
