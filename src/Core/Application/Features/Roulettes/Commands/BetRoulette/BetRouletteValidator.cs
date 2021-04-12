using Application.Enum;
using Application.Helpers;
using Application.Interfaces;
using FluentValidation;

namespace Application.Features.Roulettes.Commands.BetRoulette
{
    public class BetRouletteValidator : AbstractValidator<BetRouletteCommand>
    {
        private readonly IRouletteRepository rouletteRepository;
        public BetRouletteValidator(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
            Rules();
        }

        private void Rules()
        {
            RuleFor(expression: p => p.RouletteId).NotEmpty().WithMessage(errorMessage: "Roulette not specified");
            RuleFor(expression: p => p.RouletteId).MustAsync(async (RouletteId, cancellationToken) => await ValidatorHelper.RouletteExistAsync(rouletteId: RouletteId, rouletteRepository: rouletteRepository)).WithMessage(errorMessage: "Roulette not found.");
            RuleFor(expression: p => p.RouletteId).MustAsync(async (RouletteId, cancellationToken) => await ValidatorHelper.RouletteIsOpen(rouletteId: RouletteId, rouletteRepository: rouletteRepository)).WithMessage(errorMessage: "Roulette must to be open");
            RuleFor(expression: p => p.Color).Empty().When(p => p.Number != null).WithMessage(errorMessage: "Color should not be sent when it is sent Number.");
            RuleFor(expression: p => p.Color).IsEnumName(enumType: typeof(RouletteColors)).When(predicate: p => p.Color != null).WithMessage(errorMessage: "Color not valid");
            RuleFor(expression: p => p.Number).Empty().When(p => !string.IsNullOrEmpty(p.Color)).WithMessage(errorMessage: "Number should not be sent when it is sent Color."); ;
            RuleFor(expression: p => p.Number).InclusiveBetween(from: 0, to: 36).When(predicate: p => p.Number != null).WithMessage(errorMessage: "Number not valid");
            RuleFor(expression: p => p.Amount).NotEmpty().WithMessage(errorMessage: "Amount not valid");
            RuleFor(expression: p => p.Amount).GreaterThan(valueToCompare: 0).WithMessage(errorMessage: "Amount must to be greater than zero");
            RuleFor(expression: p => p.Amount).LessThanOrEqualTo(valueToCompare: 10000).WithMessage(errorMessage: "Amount must to be less than or equal to 10.000 USD");
            RuleFor(expression: p => p.UserId).NotEmpty().WithMessage(errorMessage: "UserId not specified");
            RuleFor(expression: p => p).Must(predicate: p => !(p.Number == null && p.Color == null)).WithMessage("You must bet on Color or Number");
        }
    }
}
