using System.Threading.Tasks;
using CrestSharp.Infrastructure;
using CrestSharp.Model;

namespace CrestSharp
{
    public interface IAuthenticatedCrest
    {
        string AccessToken { get; }

        string ClientId { get; }

        string ClientSecret { get; }

        AuthenticatedCrestState State { get; }

        CrestAuthorization CrestAuthorization { get; }

        AuthenticatedCrestCharacter Character { get; }

        Task<Location> GetCharacterLocationAsync();

        Task AddWaypoint( ICrestSolarSystem waypoint, WaypointList waypointList);

        Task SetWaypointAsync(ICrestSolarSystem waypoint);

        Task SaveFittingAsync(Fitting fitting);

        Task Refresh();
    }
}
