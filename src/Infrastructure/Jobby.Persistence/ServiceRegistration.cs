using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Interfaces.Storage;
using Jobby.Application.Repositories;
using Jobby.Persistence.Concrets;
using Jobby.Persistence.Context;
using Jobby.Persistence.Repositories;
using Jobby.Persistence.Services.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobby.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("cString"), options =>
                {
                    options.MigrationsHistoryTable("__efmigrationshistory", "production");
                    options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(3), new List<string>());
                }).UseSnakeCaseNamingConvention();
            }, ServiceLifetime.Scoped);

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IReadRepository<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IReadRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IWriteRepository<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IWriteRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(ReadRepository<>))
                .AddClasses(classes => classes.AssignableTo(typeof(ReadRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(WriteRepository<>))
                .AddClasses(classes => classes.AssignableTo(typeof(WriteRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped<IClaimManager, ClaimManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ILocalStorage, LocalStorage>();
        }
    }
}
