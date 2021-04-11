using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Roulettes.Commands.CreateRoulette
{
    public class CreateRouletteCommandHandler : IRequestHandler<CreateRouletteCommand, Guid>
    {
        private readonly IRouletteRepository rouletteRepository;

        public CreateRouletteCommandHandler(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
        }

        public async Task<Guid> Handle(CreateRouletteCommand request, CancellationToken cancellationToken)
        {
            var roullete = new Roulette(id: Guid.NewGuid(), status: "new");
            var roulleteDb = await rouletteRepository.AddOrUpdateAsync(roulette: roullete);
            return roulleteDb.Id;
        }
    }
}
