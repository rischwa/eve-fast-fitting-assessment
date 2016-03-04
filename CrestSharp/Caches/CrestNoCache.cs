using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrestSharp.Infrastructure;
using CrestSharp.Model;

namespace CrestSharp.Caches
{
    public class CrestNoCache : ICrestCache
    {
        public async Task<T> Get<T>(string href) where T : class, ICrestObject
        {
            return null;
        }

        public async Task Put(ICrestObject value)
        {

        }

        public void ClearCache()
        {
        }
    }
}
