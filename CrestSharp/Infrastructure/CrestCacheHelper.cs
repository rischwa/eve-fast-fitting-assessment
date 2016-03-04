using System;
using CrestSharp.Implementation;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;
using Newtonsoft.Json;

namespace CrestSharp.Infrastructure
{
    public static class CrestCacheHelper
    {
        public static readonly JsonSerializerSettings SETTINGS = new JsonSerializerSettings();

        static CrestCacheHelper()
        {
            SETTINGS.Converters.Add(new CrestJsonConverter());
            SETTINGS.ContractResolver = new CrestJsonContractResolver();
            SETTINGS.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            SETTINGS.ObjectCreationHandling = ObjectCreationHandling.Replace;
        }

        public static bool TryCreateObject(string href, out ICrestObject result)
        {
            if (string.IsNullOrEmpty(href))
            {
                result = null;
                return false;
            }

            var type = GetCrestTypeOrNull(href);
            if (type == null)
            {
                result= null;
                return false;
            }

            result = (ICrestObject) Activator.CreateInstance(type);
            return true;

        }
        public static Type GetCrestTypeOrNull(string href)
        {
            var uri = new Uri(href);
            var path = uri.AbsolutePath;
            var query = uri.Query;
            if (path.StartsWith("/inventory/categories/"))
            {
                return !string.IsNullOrEmpty(query) ? typeof (Page<ICrestCategory>) : typeof (CrestCategory);
            }
            if (path.StartsWith("/inventory/groups/"))
            {
                return !string.IsNullOrEmpty(query) ? typeof (Page<ICrestGroup>) : typeof (CrestGroup);
            }
            if (path.StartsWith("/types/"))
            {
                return typeof (CrestType);
            }
            if (path.StartsWith("/dogma/attributes/"))
            {
                return typeof (CrestDogmaAttribute);
            }
            if (path.StartsWith("/dogma/effects/"))
            {
                return typeof (CrestDogmaEffect);
            }
            if (path.StartsWith("/killmails/"))
            {
                return typeof (CrestKillmail);
            }

            if (path.StartsWith("/solarsystems/"))
            {
                return typeof (CrestSolarSystem);
            }
            if (path.StartsWith("/alliances/"))
            {
                return typeof (CrestAlliance);
            }
            return null;
        }
    }
}