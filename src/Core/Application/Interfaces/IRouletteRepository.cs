using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Application.Interfaces
{
    public interface IRouletteRepository
    {
        Task<IReadOnlyList<Roulette>> GetAllAsync();
        Task<Roulette> GetByIdAsync(string rouletteId);
        Task<Roulette> AddOrUpdateAsync(Roulette roulette);
    }
}
