using Application.Enum;
using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Roulettes.Commands.OpeningRoulette
{
    public class OpeningRouletteCommandHandler : IRequestHandler<OpeningRouletteCommand, OpeningRouletteResponse>
    {
        private readonly IRouletteRepository rouletteRepository;

        public OpeningRouletteCommandHandler(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
        }

        public async Task<OpeningRouletteResponse> Handle(OpeningRouletteCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.RouletteId, out Guid roulleteId);
            var roulette = await rouletteRepository.GetByIdAsync(rouletteId: roulleteId);
            if (roulette == null)
            {
                return new OpeningRouletteResponse() { OperationStatus = "Failed. Roulette not found." };
            }
            if (roulette.Status == RouletteStatus.Open.ToString())
            {
                return new OpeningRouletteResponse() { RouletteId = roulette.Id.ToString(), RouletteCurrentStatus = roulette.Status, OperationStatus = "Roulette is now open" };
            }
            if (roulette.Status == RouletteStatus.Closed.ToString())
            {
                return new OpeningRouletteResponse() { RouletteId = roulette.Id.ToString(), RouletteCurrentStatus = roulette.Status, OperationStatus = "Denied. The roulette is already closed." };
            }
            roulette.Status = RouletteStatus.Open.ToString();
            var rouletteDb = await rouletteRepository.AddOrUpdateAsync(roulette: roulette);

            return new OpeningRouletteResponse() { RouletteId = rouletteDb.Id.ToString(), RouletteCurrentStatus = rouletteDb.Status, OperationStatus = "Successful" };
        }
    }
}
