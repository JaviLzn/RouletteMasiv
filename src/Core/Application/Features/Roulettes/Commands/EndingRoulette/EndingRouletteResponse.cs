using Domain.Entities;
using System.Collections.Generic;

namespace Application.Features.Roulettes.Commands.EndingRoulette
{
    public class EndingRouletteResponse
    {
        public string RouletteId { get; set; }
        public string RouletteCurrentStatus { get; set; }
        public string OperationStatus { get; set; }
        public List<Bet> Bets { get; set; }
        public int? WinnerNumber { get; set; }
        public List<string> ValidationFailures { get; set; }
    }
}
