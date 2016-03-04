using System.Threading.Tasks;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;

namespace CrestSharp.Implementation
{
    public class Killmails : CrestPartBase, IKillmails
    {
        public async Task<ICrestKillmail> FetchAsync(string href)
        {
            //TODO evtl. noch id/hash methode anbieten
            return await Load<CrestKillmail>(href);
        }
    }
}
