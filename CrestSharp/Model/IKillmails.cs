using System.Threading.Tasks;

namespace CrestSharp.Model
{
    public interface IKillmails
    {
        Task<ICrestKillmail> FetchAsync(string crestKillmailUrl);
    }
}