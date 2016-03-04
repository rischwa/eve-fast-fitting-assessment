using System.Threading.Tasks;
using CrestSharp.Model;

namespace CrestSharp.Caches
{
    public interface ICrestCache
    {
        Task<T> Get<T>(string href) where T : class, ICrestObject;

        Task Put(ICrestObject value);

        void ClearCache();
    }
}