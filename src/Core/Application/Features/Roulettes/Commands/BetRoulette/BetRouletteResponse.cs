using System.Collections.Generic;

namespace Application.Features.Roulettes.Commands.BetRoulette
{
    public class BetRouletteResponse
    {
        public string BetStatus { get; set; }
        public List<string> ValidationFailures { get; set; }
    }
}
