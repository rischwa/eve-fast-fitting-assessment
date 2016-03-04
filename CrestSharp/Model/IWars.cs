using System.Threading.Tasks;

namespace CrestSharp.Model
{
    public interface IWars
    {
        Task<ICrestWar> FetchAsync(int id);
    }
}