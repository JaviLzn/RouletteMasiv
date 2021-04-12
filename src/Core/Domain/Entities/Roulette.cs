using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Roulette
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public List<Bet> Bets { get; set; }
        public int WinnerNumber { get; set; }
        public Roulette()
        {
        }
        public Roulette(string status)
        {
            Id = Guid.NewGuid().ToString();
            Status = status;
            WinnerNumber = -1;
        }
    }
}
