using System.Threading.Tasks;
using CrestSharp.Model;

namespace CrestSharp.Implementation
{
    public class Wars : CrestPartBase, IWars
    {
        public async Task<ICrestWar> FetchAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}wars/{id}/";

            return await Load<ICrestWar>(href);
        }
    }
}