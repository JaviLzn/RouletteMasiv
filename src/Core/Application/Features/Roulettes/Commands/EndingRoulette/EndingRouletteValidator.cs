using Application.Helpers;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roulettes.Commands.EndingRoulette
{
    public class EndingRouletteValidator : AbstractValidator<EndingRouletteCommand>
    {
        private readonly IRouletteRepository rouletteRepository;
        public EndingRouletteValidator(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
            Rules();
        }

        private void Rules()
        {
            RuleFor(expression: p => p.RouletteId).NotEmpty().WithMessage(errorMessage: "Roulette not specified");
            RuleFor(expression: p => p.RouletteId).MustAsync(predicate: async (RouletteId, cancellationToken) => await ValidatorHelper.RouletteExistAsync(rouletteId: RouletteId, rouletteRepository: rouletteRepository)).WithMessage(errorMessage: "Roulette not found.");
            RuleFor(expression: p => p.RouletteId).MustAsync(predicate: async (RouletteId, cancellationToken) => await ValidatorHelper.RouletteIsOpen(rouletteId: RouletteId, rouletteRepository: rouletteRepository)).WithMessage(errorMessage: "Roulette must to be open");
        }
    }
}
