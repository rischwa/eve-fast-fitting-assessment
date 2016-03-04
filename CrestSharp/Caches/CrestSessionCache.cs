using System.Collections.Concurrent;
using System.Threading.Tasks;
using CrestSharp.Infrastructure;
using CrestSharp.Model;

namespace CrestSharp.Caches
{
    public class CrestSessionCache : ICrestCache
    {
        private readonly ConcurrentDictionary<string, ICrestObject> _cachedObjects = new ConcurrentDictionary<string, ICrestObject>();
#pragma warning disable 1998
        public virtual async Task Put(ICrestObject value)
#pragma warning restore 1998
        {
            _cachedObjects[value.Href] = value;
        }
#pragma warning disable 1998
        public virtual async Task<T> Get<T>(string href) where T : class, ICrestObject
#pragma warning restore 1998
        {
            ICrestObject cachedValue;
            _cachedObjects.TryGetValue(href, out cachedValue);

            return (T) cachedValue;
        }

        public virtual void ClearCache()
        {
            _cachedObjects.Clear();
        }
    }
}
