using Application.Enum;
using Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class ValidationHelper
    {
        public static async Task<bool> RouletteExistAsync(string RouletteId, IRouletteRepository rouletteRepository)
        {
            Guid.TryParse(RouletteId, out Guid roulleteId);
            return await rouletteRepository.GetByIdAsync(rouletteId: roulleteId) != null;
        }

        public static async Task<bool> RouletteIsOpen(string RouletteId, IRouletteRepository rouletteRepository)
        {
            Guid.TryParse(RouletteId, out Guid roulleteId);
            return (await rouletteRepository.GetByIdAsync(rouletteId: roulleteId))?.Status == RouletteStatus.Open.ToString();
        }
    }
}
