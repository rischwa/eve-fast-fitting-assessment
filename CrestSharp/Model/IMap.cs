using System.Threading.Tasks;

namespace CrestSharp.Model
{
    public interface IMap
    {
        Task<ICrestRegion> FetchRegionAsync(int id);

        Task<ICrestPlanet> FetchPlanetAsync(int id);

        Task<ICrestSolarSystem> FetchSolarSystemAsync(int id);

        Task<ICrestConstellation> FetchConstellationAsync(int id);
    }
}