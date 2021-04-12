using MediatR;

namespace Application.Features.Roulettes.Commands.EndingRoulette
{
    public class EndingRouletteCommand : IRequest<EndingRouletteResponse>
    {
        public string RouletteId { get; set; }
    }
}
