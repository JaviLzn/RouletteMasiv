using Domain.Entities;
using System;
using System.Threading.Tasks;


namespace Application.Interfaces
{
    public interface IRouletteRepository
    {
        Task<Roulette> GetByIdAsync(Guid rouletteId);
        Task<Roulette> AddOrUpdateAsync(Roulette roulette);
    }
}
