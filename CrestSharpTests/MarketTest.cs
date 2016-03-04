using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrestSharpTests
{
    [TestClass]
    public class MarketTest
    {
        private const int JITA_ID = 10000002;
        [TestMethod]
        public void TestSellPrice()
        {
            var crest = new Crest();

            var type = crest.Inventory.FetchTypeAsync(22291)
                .Result;
            var sellItems = crest.Market.GetSellItems(JITA_ID, type);
            var marketItems = sellItems.ToArray();
            Assert.IsTrue(marketItems.Count() > 5);

                var jitaItems = marketItems.Where(x => x.Location.Name.StartsWith("Jita IV")).ToArray();

            Assert.IsTrue(jitaItems.Count() > 5);
            var minPrice = jitaItems
                .DefaultIfEmpty()
                .Min(x => x.Price);

            Assert.IsTrue(minPrice > 0);
        }
    }
}
