using System.Threading.Tasks;

namespace CrestSharp.Model
{
    public interface ICrestObject
    {
        void EnsureInitialization();

        void Refresh();

        Task RefreshAsync();

        string Href { get; set; }
        
    }
}