namespace Application.Features.Roulettes.Commands.CreateRoulette
{
    public class CreateRouletteResponse
    {
        public string RouletteId { get; set; }

        public CreateRouletteResponse(string rouletteId)
        {
            RouletteId = rouletteId;
        }
    }
}
