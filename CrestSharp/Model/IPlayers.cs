using System.Threading.Tasks;

namespace CrestSharp.Model
{
    public interface IPlayers
    {
        Task<ICrestAlliance> FetchAllianceAsync(int id);
    }
}