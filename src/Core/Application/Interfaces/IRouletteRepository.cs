using Domain.Entities;
using System.Threading.Tasks;


namespace Application.Interfaces
{
    public interface IRouletteRepository
    {
        Task<Roulette> AddOrUpdateAsync(Roulette roulette);
    }
}
