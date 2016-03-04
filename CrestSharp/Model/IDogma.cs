using System.Threading.Tasks;

namespace CrestSharp.Model
{
    public interface IDogma
    {
        Task<ICrestDogmaAttribute> FetchAttributeAsync(int id);

        Task<ICrestDogmaEffect> FetchEffectAsync(int id);
    }
}