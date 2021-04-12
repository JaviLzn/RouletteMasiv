using Application.Enum;
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

namespace Application.Features.Roulettes.Commands.EndingRoulette
{
    public class EndingRouletteCommandHandler : IRequestHandler<EndingRouletteCommand, EndingRouletteResponse>
    {
        private readonly IEnumerable<IValidator<EndingRouletteCommand>> validators;
        private readonly IRouletteRepository rouletteRepository;
        private readonly int winnerNumber;

        public EndingRouletteCommandHandler(IEnumerable<IValidator<EndingRouletteCommand>> validators,
                                            IRouletteRepository rouletteRepository)
        {
            this.validators = validators;
            this.rouletteRepository = rouletteRepository;
            Random rnd = new Random();
            winnerNumber = rnd.Next(37);
        }

        public async Task<EndingRouletteResponse> Handle(EndingRouletteCommand request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<EndingRouletteCommand>(request);
            List<ValidationFailure> failures = validators.Select(x => x.Validate(context: context)).SelectMany(selector: x => x.Errors).Where(predicate: x => x != null).ToList();
            if (failures.Count > 0)
            {
                return new EndingRouletteResponse() { OperationStatus = "Failed", ValidationFailures = failures.Select(x => x.ErrorMessage).ToList() };
            }
            Roulette roulette = await ProcessRoulette(request);
            return new EndingRouletteResponse() { OperationStatus = "Successful", RouletteId = roulette.Id, Bets = roulette.Bets, RouletteCurrentStatus = roulette.Status, WinnerNumber = winnerNumber };
        }

        private async Task<Roulette> ProcessRoulette(EndingRouletteCommand request)
        {
            Roulette roulette = await rouletteRepository.GetByIdAsync(request.RouletteId);
            roulette.WinnerNumber = winnerNumber;
            roulette.Status = RouletteStatus.Closed.ToString();
            roulette.Bets.ForEach(bet => SetEarnInBet(bet));
            return await rouletteRepository.AddOrUpdateAsync(roulette);
        }

        private void SetEarnInBet(Bet bet)
        {
            if (bet.Number != null)
            {
                bet.AmountEarned = bet.Number == winnerNumber ? bet.Amount * 5 : 0;
            }
            if (bet.Color != null)
            {
                var colorWinner = winnerNumber % 2 == 0 ? RouletteColors.Red.ToString() : RouletteColors.Black.ToString();
                bet.AmountEarned = bet.Color == colorWinner ? Math.Round(bet.Amount * 1.8m, 2) : 0;
            }
        }
    }
}
