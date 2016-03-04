using System.Threading.Tasks;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;

namespace CrestSharp.Implementation
{
    public class Map : CrestPartBase, IMap
    {
        public async Task<ICrestSolarSystem> FetchSolarSystemAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}solarsystems/{id}/";

            return await Load<CrestSolarSystem>(href);
        }

        public async Task<ICrestConstellation> FetchConstellationAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}constellations/{id}/";

            return await Load<CrestConstellation>(href);
        }

        public async Task<ICrestRegion> FetchRegionAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}regions/{id}/";

            return await Load<CrestRegion>(href);
        }

        public async Task<ICrestPlanet> FetchPlanetAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}planets/{id}/";

            return await Load<CrestPlanet>(href);
        }
    }
}