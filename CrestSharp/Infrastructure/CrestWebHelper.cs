using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CrestSharp.Implementation;
using Newtonsoft.Json;

namespace CrestSharp.Infrastructure
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class CrestAuthorizationJsonModel
    {
// ReSharper disable InconsistentNaming
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string token_type { get; set; }

        public string refresh_token { get; set; }
    }

    internal static class CrestWebHelper
    {
        public static async Task<CrestAuthorization> RequestCrestAuthorization(AuthenticatedCrest authenticatedCrest)
        {
            var startOfRequest = DateTime.UtcNow;

            var request = (HttpWebRequest) WebRequest.Create($"{Crest.Settings.SsoUrl}oauth/token");

            var data = Encoding.ASCII.GetBytes($"grant_type=authorization_code&code={authenticatedCrest.AccessToken}");

            //token is one time use only
            authenticatedCrest.AccessToken = null;

            request.Method = "POST";
            request.UserAgent = Crest.UserAgent;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            var base64ClientData = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{authenticatedCrest.ClientId}:{authenticatedCrest.ClientSecret}"));
            request.Headers["Authorization"] = $"Basic {base64ClientData}";

            using (var stream = await request.GetRequestStreamAsync())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = (HttpWebResponse) await request.GetResponseAsync())
            {
                using (var resStream = response.GetResponseStream())
                {
                    if (resStream == null)
                    {
                        throw new Exception($"Could not get response stream for {request}");
                    }

                    using (var reader = new StreamReader(resStream))
                    {
                        var stringResult = reader.ReadToEnd();
                        
                        var  jsonModel = JsonConvert.DeserializeObject<CrestAuthorizationJsonModel>(stringResult);

                        return new CrestAuthorization(
                            jsonModel.access_token,
                            startOfRequest + TimeSpan.FromSeconds(jsonModel.expires_in),
                            jsonModel.token_type,
                            jsonModel.refresh_token);
                    }
                }
            }
        }
    }
}
