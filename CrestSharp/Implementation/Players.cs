using System.Threading.Tasks;
using CrestSharp.Model;

namespace CrestSharp.Implementation
{
    public class Players : CrestPartBase, IPlayers
    {
        public async Task<ICrestAlliance> FetchAllianceAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}alliances/{id}/";

            return await Load<ICrestAlliance>(href);
        }
    }
}