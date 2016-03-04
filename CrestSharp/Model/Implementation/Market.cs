using System.Collections.Generic;
using CrestSharp.Implementation;

namespace CrestSharp.Model.Implementation
{
    public class Market : CrestPartBase, IMarket
    {
        public IEnumerable<IMarketItem> GetSellItems(int solarSystemId, ICrestType type)
        {
            return new PagedEnumerable<IMarketItem>($"{Crest.Settings.PublicEndpointUri}market/{solarSystemId}/orders/sell/?type={type.Href}");
        }

        public IEnumerable<IMarketItem> GetBuyItems(int solarSystemId, ICrestType type)
        {
            return new PagedEnumerable<IMarketItem>($"{Crest.Settings.PublicEndpointUri}market/{solarSystemId}/orders/buy/?type={type.Href}");
        }
    }
}