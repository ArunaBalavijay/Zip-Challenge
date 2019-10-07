using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Dto;
using Zip.Challenge.Common.Exceptions;
using Zip.Challenge.Common.Mongo;
using Zip.Challenge.Common.Queries;
using Zip.Challenge.Common.Services;
using Zip.Challenge.Services.Identity.Domain.Repositories;
using Zip.Challenge.Services.Identity.Handlers;
using Zip.Challenge.Services.Identity.Services;

namespace Zip.Challenge.Services.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
            IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMongoDB(Configuration);
            services.AddRabbitMq(CurrentEnvironment);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddTransient<ICommandHandler<CreateAccount>, CreateAccountHandler>();
            services.AddTransient<IQueryHandler<ListUsers, List<User>>, ListUsersHandler>();
            services.AddTransient<IQueryHandler<GetUser, User>, GetUserHandler>();
            services.AddTransient<IQueryHandler<ListAccounts, List<Account>>, ListAccountsHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.ConfigureExceptionHandler();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
