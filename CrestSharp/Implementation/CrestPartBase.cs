using System;
using System.Threading.Tasks;
using CrestSharp.Model;

namespace CrestSharp.Implementation
{
    public class CrestPartBase
    {
        protected static async Task<T> Load<T>(string href) where T : class, ICrestObject
        {
            var crestObject = await Crest.Settings.Cache.Get<T>(href).ConfigureAwait(false);
            if (crestObject != null)
            {
                return crestObject;
            }

            crestObject = (T) Activator.CreateInstance(typeof (T));
            crestObject.Href = href;
            
            await crestObject.RefreshAsync().ConfigureAwait(false);

            return crestObject;
        }
    }
}
