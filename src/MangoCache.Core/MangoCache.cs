using MessagePack;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoCache.Core
{
    public sealed class MangoCache : IMangoCache, IDisposable
    {
        private readonly ConcurrentDictionary<string, (byte[] Data, DateTime Expiry)> _storage = new();
        private readonly PriorityQueue<string, DateTime> _expiryQueue = new();
        private readonly SemaphoreSlim _lock = new(1, 1); 

        public async ValueTask SetAsync<T>(
            string key,
            T value,
            TimeSpan ttl,
            CancellationToken ct = default)
        {
            var expiry = DateTime.UtcNow.Add(ttl);
            var data = MessagePackSerializer.Serialize(value);

            await _lock.WaitAsync(ct);
            try
            {
                _storage[key] = (data, expiry);
                _expiryQueue.Enqueue(key, expiry);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async ValueTask<T?> GetAsync<T>(
            string key,
            CancellationToken ct = default)
        {
            if (!_storage.TryGetValue(key, out var entry))
                return default;

            if (entry.Expiry < DateTime.UtcNow)
            {
                await RemoveAsync(key, ct);
                return default;
            }

            return MessagePackSerializer.Deserialize<T>(entry.Data);
        }

        public async ValueTask<bool> RemoveAsync(
            string key,
            CancellationToken ct = default)
        {
            await _lock.WaitAsync(ct);
            try
            {
                return _storage.TryRemove(key, out _);
            }
            finally
            {
                _lock.Release();
            }
        }

        public void Dispose() => _lock.Dispose();
    }
}
