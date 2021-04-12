namespace Application.Features.Roulettes.Commands.BetRoulette
{
    public class BetRouletteRequest
    {
        public int? Number { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
    }
}
