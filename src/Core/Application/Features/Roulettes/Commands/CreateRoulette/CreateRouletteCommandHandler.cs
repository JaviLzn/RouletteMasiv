using Application.Enum;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Roulettes.Commands.CreateRoulette
{
    public class CreateRouletteCommandHandler : IRequestHandler<CreateRouletteCommand, CreateRouletteResponse>
    {
        private readonly IRouletteRepository rouletteRepository;

        public CreateRouletteCommandHandler(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
        }

        public async Task<CreateRouletteResponse> Handle(CreateRouletteCommand request, CancellationToken cancellationToken)
        {
            var roullete = new Roulette(status: RouletteStatus.New.ToString());
            var roulleteDb = await rouletteRepository.AddOrUpdateAsync(roulette: roullete);

            return new CreateRouletteResponse(roulleteDb.Id.ToString());
        }
    }
}
