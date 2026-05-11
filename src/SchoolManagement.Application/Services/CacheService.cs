using Microsoft.Extensions.Caching.Distributed;
using SchoolManagement.Application.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;


        public CacheService(
            IDistributedCache cache,
            IConnectionMultiplexer redis)
        {
            _cache = cache;
            _redis = redis;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedData =
                await _cache.GetStringAsync(key);

            if (cachedData == null)
                return default;

            return JsonSerializer.Deserialize<T>(
                cachedData);
        }

        public async Task SetAsync<T>(
            string key,
            T value,
            TimeSpan expiration)
        {
            var options =
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        expiration
                };

            var jsonData =
                JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(
                key,
                jsonData,
                options);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task RemoveByPatternAsync(string pattern)
        {
            var server =
                _redis.GetServer(
                    _redis.GetEndPoints().First());

            var keys = server.Keys(
                pattern: $"{pattern}*");

            foreach (var key in keys)
            {
                await _cache.RemoveAsync(key!);
            }
        }
    }
}
