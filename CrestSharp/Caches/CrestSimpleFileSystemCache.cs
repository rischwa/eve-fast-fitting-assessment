using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using CrestSharp.Implementation;
using CrestSharp.Infrastructure;
using CrestSharp.Model;
using Newtonsoft.Json;

namespace CrestSharp.Caches
{
    public class CrestSimpleFileSystemCache : CrestSessionCache
    {
        private static readonly string BASE_PATH = Path.Combine(
                                                                Environment.GetEnvironmentVariable("APPDATA"),
                                                                "CrestSharp",
                                                                "NaiveFilesystemCache");

        static CrestSimpleFileSystemCache()
        {
            if (!Directory.Exists(BASE_PATH))
            {
                Directory.CreateDirectory(BASE_PATH);
            }

          
        }

        public override void ClearCache()
        {
            base.ClearCache();
            Directory.Delete(BASE_PATH, true);

            Directory.CreateDirectory(BASE_PATH);
        }

        public override async Task<T> Get<T>(string href) 
        {
            try
            {
                var sessionObject = await base.Get<T>(href);
                if (sessionObject != null)
                {
                    return sessionObject;
                }

                return GetFromFileCache<T>(href);
            }
            catch (Exception e)
            {
                //TODO log
                Console.WriteLine($"could not get {href}: {e.Message}\n{e.StackTrace}\n");
                return null;
            }
        }

        private T GetFromFileCache<T>(string href) where T : class, ICrestObject
        {
            var absolutePath = new Uri(href).PathAndQuery;

            var type = CrestCacheHelper.GetCrestTypeOrNull(href);
            if (type == null)
            {
                return null;
            }

            var path = GetPath(absolutePath);
            if (File.Exists(path))
            {
                return ReadFile(path, href, type) as T;
            }

            return null;
        }

        private static string GetPath(string absolutePath)
        {
            var parts = absolutePath.Split('/');
            var offset = string.IsNullOrEmpty(parts[parts.Length - 1]) ? 0 : 1;
            var takePartCount = parts.Length - (parts[1] == "killmails" ? 4 : 3) + offset;
            var typePart = string.Join(
                                       "_",
                                       parts.Skip(1)
                                           .Take(takePartCount));

            var idPartIndex = parts.Length - 1;
            if (string.IsNullOrEmpty(parts[idPartIndex]))
            {
                --idPartIndex;
            }
            var idPart = parts[idPartIndex];
            idPart = idPart.Replace('?', '_');

            var directory = Path.Combine(BASE_PATH, typePart);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return Path.Combine(directory, idPart + ".json.gz");
        }

        public override async Task Put(ICrestObject value)
        {
            try
            {
                await base.Put(value);

                var init = value as IIsInitializable;
                if (!(init?.IsInitialized).GetValueOrDefault())
                {
                    return;
                }
                var absolutePath = new Uri(value.Href).PathAndQuery;
                var path = GetPath(absolutePath);

                var json = JsonConvert.SerializeObject(value, CrestCacheHelper.SETTINGS);
                lock (this)
                {
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        using (var gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
                        {
                            using (var writer = new StreamWriter(gzipStream))
                            {
                                writer.Write(json);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //TODO log
                Console.WriteLine($"error in cache put for {value.Href}: {e.Message}\n{e.StackTrace}\n");
            }
        }

        private ICrestObject ReadFile(string path, string href, Type type)
        {
            var instance = (ICrestObject) Activator.CreateInstance(type);
            instance.Href = href;

            base.Put(instance)
                .Wait();

            string document;
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                using (var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(gzipStream))
                    {
                        document = reader.ReadToEnd();
                    }
                }
            }

            JsonConvert.PopulateObject(document, instance, CrestCacheHelper.SETTINGS);

            return instance;
        }

        //TODO crest faction
    }
}
