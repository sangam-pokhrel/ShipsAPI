using FluentValidation;
using Ships.DTO;

namespace Ships.API.Validations
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ShipRequest>, ShipRequestValidator>();
            return services;
        }
    }
}
