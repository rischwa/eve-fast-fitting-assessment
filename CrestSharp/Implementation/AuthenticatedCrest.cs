using System;
using System.Text;
using System.Threading.Tasks;
using CrestSharp.Infrastructure;
using CrestSharp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CrestSharp.Implementation
{
    public class AuthenticatedCrest : IAuthenticatedCrest
    {
        private const string WAYPOINT_TEMPLATE = @"{{
    ""solarSystem"": {{""href"": ""{0}"", ""id"": {1}}},
    ""first"": {2},
    ""clearOtherWaypoints"": {3}
}}";

        private string BearerToken => $"Bearer {CrestAuthorization.AccessToken}";

        private string BasicToken => $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ClientId}:{ClientSecret}"))}";

        public AuthenticatedCrestCharacter Character { get; private set; }

        public string AccessToken { get; internal set; }

        public string ClientId { get; internal set; }

        public string ClientSecret { get; internal set; }

        public AuthenticatedCrestState State { get; internal set; }

        public CrestAuthorization CrestAuthorization { get; internal set; }

        public async Task<Location> GetCharacterLocationAsync()
        {
            await EnsureTokenIsValid()
                .ConfigureAwait(false);

            var response = await Crest.Settings.DocumentLoader.AuthenticatedGetRequest(Character.Location.Href, BearerToken)
                                     .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Location>(response, CrestCacheHelper.SETTINGS);
        }

        public async Task AddWaypoint(ICrestSolarSystem waypoint, WaypointList waypointList)
        {
            await EnsureTokenIsValid()
                .ConfigureAwait(false);

            var json = string.Format(
                                     WAYPOINT_TEMPLATE,
                                     waypoint.Href,
                                     waypoint.Id,
                                     waypointList == WaypointList.AddToFront ? "false" : "true",
                                     "false");
            await Crest.Settings.DocumentLoader.AuthenticatedPostRequest(Character.Waypoints.Href, BearerToken, json, DataType.Json)
                .ConfigureAwait(false);
        }

        public async Task SetWaypointAsync(ICrestSolarSystem waypoint)
        {
            await EnsureTokenIsValid()
                .ConfigureAwait(false);

            var json = string.Format(WAYPOINT_TEMPLATE, waypoint.Href, waypoint.Id, "true", "true");
            await Crest.Settings.DocumentLoader.AuthenticatedPostRequest(Character.Waypoints.Href, BearerToken, json, DataType.Json)
                .ConfigureAwait(false);
        }

        public async Task SaveFittingAsync(Fitting fitting)
        {
            await EnsureTokenIsValid()
                .ConfigureAwait(false);

            var fittingJson = JsonConvert.SerializeObject(
                                                          fitting,
                                                          new JsonSerializerSettings
                                                          {
                                                              ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                          });
            await Crest.Settings.DocumentLoader.AuthenticatedPostRequest(Character.Fittings.Href, BearerToken, fittingJson, DataType.Json);
        }

        public async Task Refresh()
        {
            State = AuthenticatedCrestState.WaitingForAuthentication;
            try
            {
                var now = DateTime.UtcNow;

                var parameters = $"grant_type=refresh_token&refresh_token={CrestAuthorization.RefreshToken}";

                var refreshStr =
                    await
                    Crest.Settings.DocumentLoader.AuthenticatedPostRequest($"{Crest.Settings.SsoUrl}oauth/token/", BasicToken, parameters)
                        .ConfigureAwait(false);

                var jsonModel = JsonConvert.DeserializeObject<CrestAuthorizationJsonModel>(refreshStr);

                var crestAuthorization = new CrestAuthorization(
                    jsonModel.access_token,
                    now + TimeSpan.FromSeconds(jsonModel.expires_in),
                    jsonModel.token_type,
                    CrestAuthorization.RefreshToken);

                CrestAuthorization = crestAuthorization;

                await UpdateCharacter()
                    .ConfigureAwait(false);

                State = AuthenticatedCrestState.Authorized;
            }
            catch (Exception)
            {
                State = AuthenticatedCrestState.AuthorizationFailed;
                throw;
            }
        }

        internal async Task UpdateCharacter()
        {
            var charDoc =
                await
                Crest.Settings.DocumentLoader.AuthenticatedGetRequest($"{Crest.Settings.AuthenticatedCrestBaseUrl}decode/", BearerToken)
                    .ConfigureAwait(false);

            var charAccess = JsonConvert.DeserializeObject<CharacterAccess>(charDoc);

            var authenticatedCharacterInfoStr =
                await Crest.Settings.DocumentLoader.AuthenticatedGetRequest(charAccess.Character.Href, BearerToken)
                          .ConfigureAwait(false);
            Character = JsonConvert.DeserializeObject<AuthenticatedCrestCharacter>(authenticatedCharacterInfoStr);
        }

        private async Task EnsureTokenIsValid()
        {
            if (CrestAuthorization.EndOfValidity > DateTime.UtcNow)
            {
                return;
            }

            await Refresh();
        }
    }
}
