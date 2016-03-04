using System.Threading.Tasks;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;

namespace CrestSharp.Implementation
{
    public class Dogma : CrestPartBase, IDogma
    {
        public async Task<ICrestDogmaAttribute> FetchAttributeAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}dogma/attributes/{id}/";

            return await Load<CrestDogmaAttribute>(href);
        }

        public async Task<ICrestDogmaEffect> FetchEffectAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}dogma/effects/{id}/";

            return await Load<CrestDogmaEffect>(href);
        }
    }
}