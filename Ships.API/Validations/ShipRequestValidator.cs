using FluentValidation;
using Ships.DTO;

namespace Ships.API.Validations
{
    public class ShipRequestValidator : AbstractValidator<ShipRequest>
    {
        public ShipRequestValidator()
        {
            RuleFor(x=> x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.WidthInMeters).GreaterThan(0);
            RuleFor(x=> x.LengthInMeters).GreaterThan(0);
        }
    }
}
