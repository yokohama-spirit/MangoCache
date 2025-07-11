using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoCache.Nuget
{
    public interface IMangoCacheClient
    {
        Task<T?> GetOrSetAsync<T>(
            string key,
            Func<Task<T>> valueFactory,
            TimeSpan ttl,
            CancellationToken ct = default);

        Task SetAsync<T>(
            string key,
            T value,
            TimeSpan ttl,
            CancellationToken ct = default);

        Task<T?> GetAsync<T>(
            string key,
            CancellationToken ct = default);

        Task<bool> RemoveAsync(
            string key,
            CancellationToken ct = default);
    }
}
