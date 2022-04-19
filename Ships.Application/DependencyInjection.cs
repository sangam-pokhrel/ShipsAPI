using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ships.Application.Interfaces;
using Ships.Application.Services;
using Ships.Domain;

namespace Ships.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddScoped<IShipsService, ShipsService>();
            services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();

            services.AddDbContext<ShipsDbContext>(options =>
                    options.UseInMemoryDatabase("InMemory"));

            return services;
        }
    }
}
