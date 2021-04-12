using MediatR;

namespace Application.Features.Roulettes.Commands.BetRoulette
{
    public class BetRouletteCommand : IRequest<BetRouletteResponse>
    {
        public string RouletteId { get; set; }
        public int? Number { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }
    }
}
