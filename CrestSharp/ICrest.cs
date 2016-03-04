using System;
using System.Threading.Tasks;
using CrestSharp.Model;

namespace CrestSharp
{
    public interface ICrest : IDisposable
    {
        IPlayers Players { get; }

        IInventory Inventory { get; }

        IMap Map { get; }

        IDogma Dogma { get; }

        IKillmails Killmails { get; }

        IWars Wars { get; }

        IMarket Market { get; }

        Task<IAuthenticatedCrest> Authenticate(string clientId, string clientSecret, Uri localUrl, AuthenticatedCrestScope scope);

        Task<IAuthenticatedCrest> Authenticate(string clientId, string clientSecret, string refreshToken);
    }
}
