using Blog.Application.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Blog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                var saveInterceptorServices = sp.GetServices<ISaveChangesInterceptor>();
                options.AddInterceptors(saveInterceptorServices);
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
