using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Roulettes.Queries.GetAllRoulettes
{
    public class GetAllRoulettesQueryHandler : IRequestHandler<GetAllRoulettesQuery, IEnumerable<Roulette>>
    {
        private readonly IRouletteRepository rouletteRepository;

        public GetAllRoulettesQueryHandler(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
        }

        public async Task<IEnumerable<Roulette>> Handle(GetAllRoulettesQuery request, CancellationToken cancellationToken)
        {
            return await rouletteRepository.GetAllAsync();
        }
    }
}
