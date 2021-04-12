using Application.Enum;
using Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class ValidatorHelper
    {
        public static async Task<bool> RouletteExistAsync(string rouletteId, IRouletteRepository rouletteRepository)
        {
            return await rouletteRepository.GetByIdAsync(rouletteId: rouletteId) != null;
        }

        public static async Task<bool> RouletteIsOpen(string rouletteId, IRouletteRepository rouletteRepository)
        {
            return (await rouletteRepository.GetByIdAsync(rouletteId: rouletteId))?.Status == RouletteStatus.Open.ToString();
        }
    }
}
