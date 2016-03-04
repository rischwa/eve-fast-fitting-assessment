using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface IMarket
    {
        IEnumerable<IMarketItem> GetSellItems(int solarSystemId, ICrestType type);

        IEnumerable<IMarketItem> GetBuyItems(int solarSystemId, ICrestType type);
    }
}