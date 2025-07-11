using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoCache.Core
{
    public interface IMangoCache
    {
        ValueTask SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken ct = default);
        ValueTask<T?> GetAsync<T>(string key, CancellationToken ct = default);
        ValueTask<bool> RemoveAsync(string key, CancellationToken ct = default);
    }
}
