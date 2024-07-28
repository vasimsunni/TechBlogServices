using Blog.Application.Data;
using Blog.Application.Helpers;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Data.Extensions;
using Blog.Infrastructure.Data.Interceptors;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Blog.IntegrationTests
{
    public class AppBuilder<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's StoreContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json")
                 .AddEnvironmentVariables()
                 .Build();

                var connectionString = configuration.GetConnectionString("Database");

                services.AddDbContext<ApplicationDbContext>((sp, options) =>
                {
                    var saveInterceptorServices = sp.GetServices<ISaveChangesInterceptor>();
                    options.AddInterceptors(saveInterceptorServices);
                    options.UseSqlServer(connectionString);
                });

                services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            });
        }
    }
}
