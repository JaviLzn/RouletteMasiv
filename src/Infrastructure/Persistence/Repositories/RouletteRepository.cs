using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Roulette> GetByIdAsync(string rouletteId)
        {
            var data = await database.StringGetAsync(rouletteId);
            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Roulette>(data);
        }

        public async Task<Roulette> AddOrUpdateAsync(Roulette roulette)
        {
            var created = await database.StringSetAsync(key: roulette.Id, value: JsonSerializer.Serialize(value: roulette, options: new JsonSerializerOptions() { IgnoreNullValues = true }));
            if (!created)
            {
                logger.LogInformation(message: "Problem occur persisting the item.");
                return null;
            }
            logger.LogInformation(message: "Roulette persisted successfully.");

            return await GetByIdAsync(rouletteId: roulette.Id);
        }

        public async Task<IReadOnlyList<Roulette>> GetAllAsync()
        {
            var server = GetServer();
            var data = server.Keys().ToList();

            var roulettes = new List<Roulette>();
            foreach (var keyRoulette in data)
            {
                var roulette = await GetByIdAsync(rouletteId: keyRoulette);
                if (roulette != null)
                {
                    roulettes.Add(roulette);
                }
            }

            return roulettes;
        }

        private IServer GetServer()
        {
            var endpoint = redis.GetEndPoints();
            return redis.GetServer(endpoint.First());
        }
    }
}
