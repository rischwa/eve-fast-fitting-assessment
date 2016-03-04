using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrestSharp;
using CrestSharp.Caches;
using CrestSharp.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrestSharpTests
{
    [TestClass]
    public class DogmaTest
    {
        [TestMethod]
        public void TestAttributeLoading()
        {
            var Crest = new Crest();
            Crest.Settings.Cache = new CrestSqliteCache();
            var result = Crest.Dogma.FetchAttributeAsync(180)
                .Result;

            Assert.IsTrue(result.Published);

             Crest = new Crest();
             result = Crest.Dogma.FetchAttributeAsync(180)
                .Result;

            Assert.IsTrue(result.Published);
        }

        [TestMethod]
        public void GroupTest()
        {
            var crest = new Crest();
            Crest.Settings.Cache = new CrestSqliteCache();

            var group = crest.Inventory.FetchGroupAsync(15)
                .Result;

            Assert.AreEqual(58, group.Types.Count);

             group = crest.Inventory.FetchGroupAsync(15)
              .Result;

            Assert.AreEqual(58, group.Types.Count);
        }
    }
}
