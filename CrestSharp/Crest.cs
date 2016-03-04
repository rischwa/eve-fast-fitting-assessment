using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CrestSharp.Implementation;
using CrestSharp.Infrastructure;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;
using Serilog;

namespace CrestSharp
{
    //TODO authenticatedcrest object erstellen

    public class Crest : ICrest
    {
        private static CrestSettings _settings;
        private static readonly CrestInternalAuthenticationWebserver WEBSERVER = new CrestInternalAuthenticationWebserver();
        private readonly IDictionary<Guid, AuthenticatedCrest> _authenticatedCrests = new Dictionary<Guid, AuthenticatedCrest>();
        private readonly object _monitor = new object();

        static Crest()
        {
            Settings = new CrestSettings();
        }

        public Crest()
        {
            Players = new Players();
            Inventory = new Inventory();
            Map = new Map();
            Dogma = new Dogma();
            Killmails = new Killmails();
            Wars = new Wars();
            Market = new Market();

            WEBSERVER.Authenticated += WebserverOnAuthenticated;
        }

        public static string UserAgent { get; set; }

        public static CrestSettings Settings
        {
            get { return _settings; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                _settings = value;
            }
        }

        public IWars Wars { get; set; }

        public IMarket Market { get; set; }

        public IKillmails Killmails { get; set; }

        public IPlayers Players { get; set; }

        public IInventory Inventory { get; set; }

        public IMap Map { get; set; }

        public IDogma Dogma { get; set; }

        public async Task<IAuthenticatedCrest> Authenticate(string clientId,
                                                            string clientSecret,
                                                            Uri listenUri,
                                                            AuthenticatedCrestScope scope)
        {
            var result = new AuthenticatedCrest
                         {
                             ClientId = clientId,
                             ClientSecret = clientSecret
                         };
            var state = Guid.NewGuid();
            try
            {
                return await Task.Factory.StartNew(
                                                   () =>
                                                   {
                                                       _authenticatedCrests[state] = result;

                                                       WEBSERVER.ListenFor(listenUri);

                                                       var scopeString = GetScopeString(scope);
                                                       Process.Start(
                                                                     $"{Settings.SsoUrl}oauth/authorize?response_type=code&redirect_uri={HttpUtility.UrlEncode(listenUri.ToString())}&client_id={clientId}&state={state}&scope={scopeString}");

                                                       var start = DateTime.UtcNow;
                                                       while (result.State == AuthenticatedCrestState.WaitingForAuthentication
                                                              && (DateTime.UtcNow - start) < Settings.AuthenticationTimeout)
                                                       {
                                                           lock (_monitor)
                                                           {
                                                               Monitor.Wait(_monitor, Settings.AuthenticationTimeout);
                                                           }
                                                       }

                                                       //check for timeout
                                                       if (result.State == AuthenticatedCrestState.WaitingForAuthentication)
                                                       {
                                                           result.State = AuthenticatedCrestState.AuthenticationFailed;
                                                       }

                                                       return result;
                                                   },
                                                   TaskCreationOptions.LongRunning)
                                 .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, $"Could not authenticate/authorize {clientId}");

                result.State = AuthenticatedCrestState.AuthenticationFailed;
            }
            finally
            {
                _authenticatedCrests.Remove(state);
            }

            return result;
        }

        public void Dispose()
        {
            WEBSERVER.Authenticated -= WebserverOnAuthenticated;
            WEBSERVER.Stop();
        }

        public async Task<IAuthenticatedCrest> Authenticate(string clientId, string clientSecret, string refreshToken)
        {
            var result = new AuthenticatedCrest
                         {
                             ClientId = clientId,
                             ClientSecret = clientSecret,
                             CrestAuthorization = new CrestAuthorization(null, DateTime.MinValue, null, refreshToken)
                         };
            await result.Refresh().ConfigureAwait(false);

            return result;
        }

        private async void WebserverOnAuthenticated(Guid state, string accessToken)
        {
            //authorization request machen
            //danach ergebnis in await methode packen
            AuthenticatedCrest authenticatedCrest;
            var isThisInstanceResponsible = _authenticatedCrests.TryGetValue(state, out authenticatedCrest);
            if (!isThisInstanceResponsible)
            {
                return;
            }

            authenticatedCrest.AccessToken = accessToken;
            try
            {
                await Task.Factory.StartNew(
                                            async () =>
                                            {
                                                authenticatedCrest.CrestAuthorization =
                                                    await CrestWebHelper.RequestCrestAuthorization(authenticatedCrest)
                                                              .ConfigureAwait(false);

                                                await authenticatedCrest.UpdateCharacter()
                                                    .ConfigureAwait(false);

                                                authenticatedCrest.State = AuthenticatedCrestState.Authorized;

                                                lock (_monitor)
                                                {
                                                    Monitor.PulseAll(_monitor);
                                                }
                                            })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, $"Could not authorize {authenticatedCrest.ClientId}");
                authenticatedCrest.State = AuthenticatedCrestState.AuthorizationFailed;

                lock (_monitor)
                {
                    Monitor.PulseAll(_monitor);
                }
            }
        }

        private static string GetScopeString(AuthenticatedCrestScope scope)
        {
            var strings = new List<string>();
            if (scope.HasFlag(AuthenticatedCrestScope.CharacterFittingsRead))
            {
                strings.Add("characterFittingsRead");
            }
            if (scope.HasFlag(AuthenticatedCrestScope.CharacterFittingsWrite))
            {
                strings.Add("characterFittingsWrite");
            }
            if (scope.HasFlag(AuthenticatedCrestScope.CharacterContactsRead))
            {
                strings.Add("characterContactsRead");
            }
            if (scope.HasFlag(AuthenticatedCrestScope.CharacterContactsWrite))
            {
                strings.Add("characterContactsWrite");
            }
            if (scope.HasFlag(AuthenticatedCrestScope.CharacterLocationRead))
            {
                strings.Add("characterLocationRead");
            }
            if (scope.HasFlag(AuthenticatedCrestScope.CharacterNavigationWrite))
            {
                strings.Add("characterNavigationWrite");
            }
            return string.Join("%20", strings);
        }
    }
}
