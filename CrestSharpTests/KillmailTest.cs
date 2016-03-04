using System;
using System.Linq;
using CrestSharp;
using CrestSharp.Caches;
using CrestSharp.Infrastructure;
using CrestSharp.Model.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrestSharpTests
{
    [TestClass]
    public class KillmailTest
    {
        [TestMethod]
        public void Playground()
        {
           Assert.AreEqual(4, "/a/b/".Split('/').Count());
        }
        [TestMethod]
        public void TestGetKillmail()
        {
            var crest = new Crest();

            var killmail = crest.Killmails.FetchAsync("https://public-crest.eveonline.com/killmails/52099016/bdb36806e2ff4e92b6e1db70654592d3a7ba70f7/").Result;
            Assert.AreEqual(52099016, killmail.KillID);
            Assert.AreEqual(DateTime.Parse("2016-02-18 09:15:27"), killmail.KillTime);
            Assert.AreEqual(4, killmail.Attackers.Count);

            var firstAttacker = killmail.Attackers.First();
            Assert.AreEqual(101000, firstAttacker.ShipType.Volume);

            //TODO richtigen cache test bauen

            var c2 = crest.Killmails.FetchAsync("https://public-crest.eveonline.com/killmails/52099016/bdb36806e2ff4e92b6e1db70654592d3a7ba70f7/").Result;
            Assert.AreEqual(101000, c2.Attackers.First().ShipType.Volume);
        }

        [TestMethod]
        public void TestDogma()
        {
            var naiveFileSystemCache = new CrestSimpleFileSystemCache();
            Crest.Settings.Cache = naiveFileSystemCache;

            var crest = new Crest() ;

            var killmail = crest.Killmails.FetchAsync("https://public-crest.eveonline.com/killmails/52099016/bdb36806e2ff4e92b6e1db70654592d3a7ba70f7/").Result;

            var type = killmail.Victim.ShipType;
            Assert.IsNotNull(type);

            Assert.IsNotNull(type.Dogma);

            Assert.IsNotNull(type.Dogma.Attributes);
            var firstAttribute = type.Dogma.Attributes.First();
            Assert.IsNotNull(firstAttribute.Attribute);

            var firstAttributeDogma = firstAttribute.Attribute;
            Assert.IsNotNull(firstAttributeDogma.Name);

            Assert.IsNotNull(firstAttributeDogma.Description);


        }
    }
}
