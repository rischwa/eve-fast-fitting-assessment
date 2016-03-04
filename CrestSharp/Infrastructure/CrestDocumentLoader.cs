using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace CrestSharp.Infrastructure
{
    public class CrestDocumentLoader : ICrestDocumentLoader
    {
        private readonly RateLimiter _publicCrestLimiter = new RateLimiter(150, TimeSpan.FromSeconds(1));
        private readonly RateLimiter _authenticatedCrestLimiter = new RateLimiter(30, TimeSpan.FromSeconds(1));

        public async Task<string> AuthenticatedPostRequest(string url, string accessToken, string parameters, DataType type = DataType.UrlEncoded)
        {
            _authenticatedCrestLimiter.EnqueueRequest();
            var request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            request.UserAgent = Crest.UserAgent ?? "";
            request.Headers["Authorization"] = accessToken;
            request.ContentType = type == DataType.UrlEncoded ? "application/x-www-form-urlencoded" : "application/json; charset=utf-8";

            var data = Encoding.ASCII.GetBytes(parameters);
            request.ContentLength = data.Length;

            using (var stream = await request.GetRequestStreamAsync()
                                          .ConfigureAwait(false))
            {
                stream.Write(data, 0, data.Length);
            }

            return await GetResponseStringFromRequest(request);
        }

        public async Task<string> AuthenticatedGetRequest(string url, string accessToken)
        {
            Log.Logger.Debug($"Requesting {url}");
            try
            {
                _authenticatedCrestLimiter.EnqueueRequest();
                var request = CreateRequest(url);
                request.Headers["Authorization"] = accessToken;

                return await GetResponseStringFromRequest(request);
            }
            catch (Exception e)
            {
                Log.Logger.Warning(e, $"Error Requesting {url}");
                throw;
            }
        }

        public async Task<string> LoadDocumentAsync(string url)
        {
            Log.Logger.Debug($"Requesting {url}");
            try
            {
                _publicCrestLimiter.EnqueueRequest();
                var request = CreateRequest(url);

                return await GetResponseStringFromRequest(request);
            }
            catch (Exception e)
            {
                Log.Logger.Warning(e, $"Error Requesting {url}");
                throw;
            }
        }

        private static async Task<string> GetResponseStringFromRequest(HttpWebRequest request)
        {
            try
            {
                using (var response = await request.GetResponseAsync()
                                                .ConfigureAwait(false))
                {
                    return GetResponseString(response);
                }
            }
            catch (WebException e)
            {
                using (var response = e.Response.GetResponseStream())
                {
                    if (response == null)
                    {
                        throw;
                    }
                    JObject responseItem;
                    try
                    {
                        responseItem = JObject.Load(new JsonTextReader(new StreamReader(response)));
                    }
                    catch (Exception)
                    {
                        throw e;
                    }
                    ThrowIfIsException(responseItem);

                    throw;
                }
            }
        }

        public static void ThrowIfIsException(JObject item)
        {
            var exceptionType = item["exceptionType"];
            if (exceptionType != null)
            {
                throw new CrestException((string)exceptionType, (string)item["message"]);
            }
        }

        private static string GetResponseString(WebResponse response)
        {
            var stream = response.GetResponseStream();
            if (stream == null)
            {
                throw new Exception("could not get response stream");
            }

            var contentEncodingValue = response.Headers["Content-Encoding"] ?? "";
            if (contentEncodingValue.ToLowerInvariant() == "gzip")
            {
                stream = new GZipStream(stream, CompressionMode.Decompress);
            }

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static HttpWebRequest CreateRequest(string url)
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = "GET";
            request.UserAgent = Crest.UserAgent ?? "";
            request.Headers["Accept-Encoding"] = "gzip";
            return request;
        }
    }
}
