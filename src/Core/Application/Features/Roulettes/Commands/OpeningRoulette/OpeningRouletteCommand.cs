using MediatR;

namespace Application.Features.Roulettes.Commands.OpeningRoulette
{
    public class OpeningRouletteCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}
