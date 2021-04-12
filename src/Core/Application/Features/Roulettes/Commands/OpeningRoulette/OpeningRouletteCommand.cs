using MediatR;

namespace Application.Features.Roulettes.Commands.OpeningRoulette
{
    public class OpeningRouletteCommand : IRequest<OpeningRouletteResponse>
    {
        public string RouletteId { get; set; }
    }
}
