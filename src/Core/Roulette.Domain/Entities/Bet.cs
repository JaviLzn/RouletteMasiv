using System;

namespace Roulette.Domain.Entities
{
    public class Bet
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }
    }
}
