using System.Linq;
using CrestSharp;
using CrestSharp.Caches;
using CrestSharp.Implementation;
using CrestSharp.Infrastructure;
using CrestSharp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CrestSharpTests
{
    [TestClass]
    public class InventoryTest
    {
        [TestMethod]
        public void TestSerializationOfPage()
        {
            var p = new Page<ICrestGroup>();
            p.Href = "https://public-crest.eveonline.com/inventory/groups/?page=1";
            p.Refresh();

            var settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.ContractResolver = new CrestJsonContractResolver();
            settings.Converters.Add(new CrestJsonConverter());

            var x = JsonConvert.SerializeObject(p, settings);
            Assert.IsFalse(string.IsNullOrEmpty(x));
        }
        [TestMethod]
        public void TestGroups()
        {
            var naiveFileSystemCache = new CrestSimpleFileSystemCache();
          //  naiveFileSystemCache.ClearCache();
            Crest.Settings.Cache = naiveFileSystemCache;
            

            var crest = new Crest();
            var groups = crest.Inventory.GetGroups().ToArray();
            Assert.AreEqual(1215, groups.Count());
            Assert.AreEqual(2, groups.First(x=>x.Id == 9).Types.Count);
            var crestGroup = groups.First();
            Assert.AreEqual(false, crestGroup.Category.Published);

        }
    }
}
