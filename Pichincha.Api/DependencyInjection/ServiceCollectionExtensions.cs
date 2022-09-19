using Microsoft.EntityFrameworkCore;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Database;
using Pichincha.Infrastructure.Repositories;
using Pichincha.Services.Implementations;
using Pichincha.Services.Intefaces;

namespace Pichincha.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>()
                .AddScoped<ICuentaService, CuentaService>()
                .AddScoped<IMovimientoService, MovimientoService>();
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>()
                .AddScoped<ICuentaRepository, CuentaRepository>()
                .AddScoped<IMovimientoRepository, MovimientoRepository>();
        }

        public static void AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options
                  .EnableSensitiveDataLogging()
                  .UseNpgsql(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }
    }
}
