using System.Threading.Tasks;
using CrestSharp.Model;

namespace CrestSharp.Caches
{
    public class CrestCacheWithSessionCache : ICrestCache
    {
        private readonly ICrestCache _firstTier = new CrestSessionCache();
        private readonly ICrestCache _secondTier;

        public CrestCacheWithSessionCache(ICrestCache secondTier)
        {
            _secondTier = secondTier;
        }

        public async Task<T> Get<T>(string href) where T : class, ICrestObject
        {
            var result = await _firstTier.Get<T>(href);
            if (result != null)
            {
                return result;
            }
            result = await _secondTier.Get<T>(href);
            if (result == null)
            {
                return null;
            }

            await _firstTier.Put(result);
            return result;
        }

        public async Task Put(ICrestObject value)
        {
            await _firstTier.Put(value);
            await _secondTier.Put(value);
        }

        public void ClearCache()
        {
            _firstTier.ClearCache();
            _secondTier.ClearCache();
        }
    }
}
