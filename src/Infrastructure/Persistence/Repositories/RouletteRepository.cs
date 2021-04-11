using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly ILogger<RouletteRepository> logger;
        private readonly ConnectionMultiplexer redis;
        private readonly IDatabase database;

        public RouletteRepository(ILogger<RouletteRepository> logger,
                                  ConnectionMultiplexer redis)
        {
            this.logger = logger;
            this.redis = redis;
            database = this.redis.GetDatabase();
        }

        public async Task<Roulette> GetByIdAsync(Guid rouletteId)
        {
            var data = await database.StringGetAsync(rouletteId.ToString());
            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Roulette>(data);
        }

        public async Task<Roulette> AddOrUpdateAsync(Roulette roulette)
        {
            var created = await database.StringSetAsync(roulette.Id.ToString(), JsonSerializer.Serialize(roulette));
            if (!created)
            {
                logger.LogInformation("Problem occur persisting the item.");
                return null;
            }
            logger.LogInformation("Roulette persisted successfully.");

            return await GetByIdAsync(roulette.Id);
        }
    }
}
