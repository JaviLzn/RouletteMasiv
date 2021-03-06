using System;

namespace Domain.Entities
{
    public class Bet
    {
        public Guid Id { get; set; }
        public int? Number { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }
        public decimal? AmountEarned { get; set; }
        public Bet()
        {
            Id = Guid.NewGuid();
        }
    }
}
