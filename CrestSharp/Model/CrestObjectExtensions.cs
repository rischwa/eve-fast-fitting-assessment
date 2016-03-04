using System.Threading.Tasks;

namespace CrestSharp.Model
{
    public static class CrestObjectExtensions
    {
        public static T Refreshed<T>(this T crestObject) where T : ICrestObject
        {
            crestObject.Refresh();
            return crestObject;
        }

        public static async Task<T> RefreshedAsync<T>(this T crestObject) where T : ICrestObject
        {
            await crestObject.RefreshAsync();

            return crestObject;
        }
    }
}
