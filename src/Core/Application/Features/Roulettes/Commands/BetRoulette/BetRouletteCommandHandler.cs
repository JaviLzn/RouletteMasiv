using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Roulettes.Commands.BetRoulette
{
    public class BetRouletteCommandHandler : IRequestHandler<BetRouletteCommand, BetRouletteResponse>
    {
        private readonly IEnumerable<IValidator<BetRouletteCommand>> validators;
        private readonly IRouletteRepository rouletteRepository;

        public BetRouletteCommandHandler(IEnumerable<IValidator<BetRouletteCommand>> validators,
                                         IRouletteRepository rouletteRepository)
        {
            this.validators = validators;
            this.rouletteRepository = rouletteRepository;
        }

        public async Task<BetRouletteResponse> Handle(BetRouletteCommand request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<BetRouletteCommand>(request);
            List<ValidationFailure> failures = validators.Select(x => x.Validate(context)).SelectMany(x => x.Errors).Where(x => x != null).ToList();
            if (failures.Count > 0)
            {
                return new BetRouletteResponse() { BetStatus = "Failed", ValidationFailures = failures.Select(x => x.ErrorMessage).ToList() };
            }
            Guid.TryParse(request.RouletteId, out Guid roulleteId);
            var roulette = await rouletteRepository.GetByIdAsync(rouletteId: roulleteId);
            roulette.Bets.Add(new Bet() { Amount = request.Amount, Color = request.Color, Number = request.Number, UserId = request.UserId });
            await rouletteRepository.AddOrUpdateAsync(roulette);

            return new BetRouletteResponse() { BetStatus = "Successful" };
        }
    }
}
