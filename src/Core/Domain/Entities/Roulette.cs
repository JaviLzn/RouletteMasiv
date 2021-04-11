using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Roulette
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public List<Bet> Bets { get; set; }
        public int WinnerNumber { get; set; }
        public Roulette()
        {
        }
        public Roulette(Guid id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}
