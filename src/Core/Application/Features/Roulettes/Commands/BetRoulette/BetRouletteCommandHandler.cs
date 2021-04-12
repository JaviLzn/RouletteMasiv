using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
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
            var context = new ValidationContext<BetRouletteCommand>(instanceToValidate: request);
            List<ValidationFailure> failures = validators.Select(selector: x => x.Validate(context: context)).SelectMany(selector: x => x.Errors).Where(predicate: x => x != null).ToList();
            if (failures.Count > 0)
            {
                return new BetRouletteResponse() { BetStatus = "Failed", ValidationFailures = failures.Select(x => x.ErrorMessage).ToList() };
            }
            var roulette = await rouletteRepository.GetByIdAsync(rouletteId: request.RouletteId);
            if (roulette.Bets == null)
            {
                roulette.Bets = new List<Bet>();
            }
            roulette.Bets.Add(item: new Bet() { Amount = request.Amount, Color = request.Color, Number = request.Number, UserId = request.UserId });
            await rouletteRepository.AddOrUpdateAsync(roulette: roulette);

            return new BetRouletteResponse() { BetStatus = "Successful" };
        }
    }
}
