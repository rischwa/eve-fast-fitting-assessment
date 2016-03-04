using System.Threading.Tasks;

namespace CrestSharp.Infrastructure
{
    public interface ICrestDocumentLoader
    {
        Task<string> AuthenticatedGetRequest(string url, string accessToken);
        Task<string> AuthenticatedPostRequest(string url, string accessToken, string parameters, DataType type = DataType.UrlEncoded);
        Task<string> LoadDocumentAsync(string url);
    }
}