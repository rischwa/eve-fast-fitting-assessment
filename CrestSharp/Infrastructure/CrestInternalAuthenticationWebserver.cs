using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using Serilog;

namespace CrestSharp.Infrastructure
{
    internal delegate void Authenticated(Guid state, string authorizationCode);
    internal sealed class CrestInternalAuthenticationWebserver
    {
        private readonly HttpListener _httpListener = new HttpListener();
        private readonly ISet<Uri> _listeningUris = new HashSet<Uri>();

        public event Authenticated Authenticated;

        private void Run()
        {
            ThreadPool.QueueUserWorkItem(
                                         (o) =>
                                         {
                                             try
                                             {
                                                 while (_httpListener.IsListening)
                                                 {
                                                     ThreadPool.QueueUserWorkItem(
                                                                                  (c) =>
                                                                                  {
                                                                                      var ctx = (HttpListenerContext) c;
                                                                                      try
                                                                                      {
                                                                                          var query =
                                                                                              HttpUtility.ParseQueryString(
                                                                                                                           ctx.Request.Url
                                                                                                                               .Query);
                                                                                          var code = query["code"];
                                                                                          var state = Guid.Parse(query["state"]);

                                                                                          Authenticated?.Invoke(state, code);

                                                                                          using (
                                                                                              var writer =
                                                                                                  new StreamWriter(
                                                                                                      ctx.Response.OutputStream))
                                                                                          {
                                                                                              writer.Write(
                                                                                                           Crest.Settings
                                                                                                               .AuthenticationResponseHtml);
                                                                                          }
                                                                                      }
                                                                                      catch (Exception e)
                                                                                      {
                                                                                          Log.Logger.Warning(
                                                                                                             e,
                                                                                                             $"Error in receiving Crest authentication on {ctx.Request.Url}");
                                                                                      }
                                                                                      finally
                                                                                      {
                                                                                          ctx.Response.OutputStream.Close();
                                                                                      }
                                                                                  },
                                                                                  _httpListener.GetContext());
                                                 }
                                             }
                                             catch (Exception e)
                                             {
                                                 Log.Logger.Error(
                                                                  e,
                                                                  $"Error in listening for Crest authentication. Authentication will fail.");
                                             }
                                         });
        }

        public void ListenFor(Uri localUri)
        {
            if (!HttpListener.IsSupported)
            {
                throw new Exception(
                    "HttpListener is unsupported on this operating system, but is required by CrestSharp for authenticated Crest.");
            }

            if (localUri.Scheme == "https")
            {
                throw new Exception("HTTPS is not supported by CrestSharp.");
            }

            if (!_listeningUris.Contains(localUri))
            {
                var uriPrefix = localUri + (localUri.AbsolutePath.EndsWith("/") ? "" : "/");
                _httpListener.Prefixes.Add(uriPrefix);
                _listeningUris.Add(localUri);
            }

            if (!_httpListener.IsListening)
            {
                _httpListener.Start();
                Run();
            }
        }

        public void Stop()
        {
            if (!_httpListener.IsListening)
            {
                _httpListener.Stop();
            }
        }
    }
}