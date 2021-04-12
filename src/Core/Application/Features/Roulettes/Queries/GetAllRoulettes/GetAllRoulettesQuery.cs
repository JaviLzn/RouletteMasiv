using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Roulettes.Queries.GetAllRoulettes
{
    public class GetAllRoulettesQuery : IRequest<IEnumerable<Roulette>>
    {
    }
}
