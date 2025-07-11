using MangoCache.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoCache.Nuget
{
    public class MangoCacheClient : IMangoCacheClient
    {
        private readonly IMangoCache _cache;
        private readonly ILogger<MangoCacheClient> _logger;
        private readonly MangoCacheClientOptions _options;

        public MangoCacheClient(
            IMangoCache cache,
            IOptions<MangoCacheClientOptions> options,
            ILogger<MangoCacheClient> logger = null)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger;
            _options = options.Value;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
        {
            try
            {
                _logger?.LogDebug("Getting cache value for key: {Key}", key);
                return await _cache.GetAsync<T>(key, ct);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting cache value for key: {Key}", key);

                if (_options.ThrowOnErrors)
                    throw;

                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken ct = default)
        {
            try
            {
                _logger?.LogDebug("Setting cache value for key: {Key} with TTL: {Ttl}", key, ttl);
                await _cache.SetAsync(key, value, ttl, ct);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error setting cache value for key: {Key}", key);

                if (_options.ThrowOnErrors)
                    throw;
            }
        }

        public async Task<bool> RemoveAsync(string key, CancellationToken ct = default)
        {
            try
            {
                _logger?.LogDebug("Removing cache value for key: {Key}", key);
                return await _cache.RemoveAsync(key, ct);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error removing cache value for key: {Key}", key);

                if (_options.ThrowOnErrors)
                    throw;

                return false;
            }
        }

        public async Task<T?> GetOrSetAsync<T>(
            string key,
            Func<Task<T>> valueFactory,
            TimeSpan ttl,
            CancellationToken ct = default)
        {
            var cachedValue = await GetAsync<T>(key, ct);
            if (cachedValue != null)
                return cachedValue;

            var value = await valueFactory();
            await SetAsync(key, value, ttl, ct);
            return value;
        }
    }
}
