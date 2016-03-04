using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrestSharp.Model
{
    public interface IInventory
    {
        Task<ICrestType> FetchTypeAsync(int id);

        Task<ICrestCategory> FetchCategoryAsync(int id);

       IEnumerable<ICrestCategory> GetCategories();

        Task<ICrestGroup> FetchGroupAsync(int id);

        IEnumerable<ICrestGroup> GetGroups();

    }
}