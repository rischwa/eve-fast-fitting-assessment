using System.Collections.Generic;
using System.Threading.Tasks;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;

namespace CrestSharp.Implementation
{
    public class Inventory : CrestPartBase, IInventory
    {
        public async Task<ICrestType> FetchTypeAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}types/{id}/";
            //TODO hier muss noch das icon gesetzt werden wahrscheinlich, evtl auch bei anderen?
            return await Load<CrestType>(href);
        }

        public async Task<ICrestCategory> FetchCategoryAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}inventory/categories/{id}/";
            return await Load<CrestCategory>(href);
        }

        public async Task<ICrestGroup> FetchGroupAsync(int id)
        {
            var href = $"{Crest.Settings.PublicEndpointUri}inventory/groups/{id}/";
            return await Load<CrestGroup>(href);
        }

        public  IEnumerable<ICrestGroup> GetGroups()
        {
            return new PagedEnumerable<ICrestGroup>($"{Crest.Settings.PublicEndpointUri}inventory/groups/?page=1");
        }

        public  IEnumerable<ICrestCategory> GetCategories()
        {
            return new PagedEnumerable<ICrestCategory>($"{Crest.Settings.PublicEndpointUri}inventory/categories/?page=1");
        }
    }
}
