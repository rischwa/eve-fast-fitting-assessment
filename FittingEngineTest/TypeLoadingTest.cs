using System;
using System.Linq;
using FittingEngine;
using FittingEngine.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FittingEngineTest
{
    [TestClass]
    public class TypeLoadingTest
    {
        private FittingService _service;

        [TestCleanup]
        public void Cleanup()
        {
        }


        [TestMethod]
        public void EnyoTest()
        {
            var ship = _service.CreateItem("Enyo");
            var items = _service.CreateItems("Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "1MN Afterburner II",
                                             "X5 Prototype Engine Enervator",
                                             "Initiated Harmonic Warp Scrambler I",
                                             "Magnetic Field Stabilizer II",
                                             "Coreli A-Type Explosive Plating",
                                             "Magnetic Field Stabilizer II",
                                             "Internal Force Field Array I",
                                             "Small Hybrid Burst Aerator I",
                                             "Small Hybrid Collision Accelerator I");

            var blaster = items.Where(x => x.TypeName == "Light Neutron Blaster II")
                               .ToArray();

            foreach (var curBlaster in blaster)
            {
                var ammo = _service.CreateItem("Federation Navy Antimatter Charge S");
                curBlaster.InstalledItems.Add(ammo);
            }

            var context = new Context()
                              {
                                  Ship = ship,
                                  Target = _service.CreateItem("Rifter"),
                                  Char = new Item()
                              };

            ship.InstalledItems.AddRange(items);
            ship.Activate(context);
            //TODO enums
            var damageAttributIds = new[] {114, 116, 117, 118};
            foreach (var item in blaster)
            {
                var multiplier = item.Attributes.First(x => x.AttributeName == "damageMultiplier").Value;
                var damageByTypes = item.InstalledItems.First()
                                        .Attributes.Where(x => damageAttributIds.Contains(x.AttributeID))
                                        .ToDictionary(x => x.AttributeID, x => x.Value);
                var cycleTime = item.Attributes.First(x => x.AttributeName == "speed");
                var damage = new Damage()
                                 {
                                     EM = damageByTypes[114] * multiplier,
                                     Explosive = damageByTypes[116] * multiplier,
                                     Kinetic = damageByTypes[117] * multiplier,
                                     Thermal = damageByTypes[118] * multiplier,
                                     CycleTime = new TimeSpan(0, 0, 0, 0, (int) cycleTime.Value)
                                 };

                Assert.AreEqual(31.522, damage.DamagePerSecond, 0.001);
            }
        }

        [TestInitialize]
        public void Init()
        {
            _service = new FittingService();
        }

        [TestMethod]
        public void TestAttributeLoadingByName()
        {
            var ship = _service.CreateItem("Rifter");

            Assert.AreEqual(90, ship.Attributes.Count);
            var strukke = ship.Attributes.Single(x => x.AttributeName == "hp");

            Assert.AreEqual(9, strukke.AttributeID);
            Assert.AreEqual(350.0, strukke.Value);
        }

        [TestMethod]
        public void TestBasicLoadingByName()
        {
            var ship = _service.CreateItem("Rifter");

            Assert.IsNotNull(ship);
            Assert.AreEqual("Rifter", ship.TypeName);
            Assert.AreEqual(587, ship.TypeId);
            Assert.AreEqual(1067000, ship.Attributes.First(x => x.AttributeName == "mass").Value);
            Assert.AreEqual(140, ship.Attributes.First(x => x.AttributeName == "capacity").Value);
        }

        [TestMethod]
        public void TestEffectLoading()
        {
            var ship = _service.CreateItem("Rifter");

            Assert.AreEqual(3, ship.Effects.Count);
            Assert.IsTrue(ship.Effects.Any(x => x.EffectId == 508 && x.EffectCategory == EffectCategory.Passive));
        }

        [TestMethod]
        public void TestOverloadEffectLoading()
        {
            var item = _service.CreateItem("Light Neutron Blaster II");

           Assert.AreEqual(5, item.Effects.Count);
            var overloadEffect = item.Effects.Single(x => x.EffectCategory == EffectCategory.Overload);
           Assert.AreEqual(3025,  overloadEffect.EffectId);
        }

        [TestMethod]
        public void TestLoadDamageControl()
        {
            var ship = _service.CreateItem("Rifter");
            var damageControl = _service.CreateItem("Damage Control II");

            var context = new Context()
                              {
                                  Ship = ship
                              };

            ship.InstalledItems.Add(damageControl);
            ship.Activate(context);

            Assert.AreEqual(0.4,
                            ship.Attributes.First(x => x.AttributeName == "emDamageResonance")
                                .Value);
            Assert.AreEqual(0.34,
                            ship.Attributes.First(x => x.AttributeName == "armorEmDamageResonance")
                                .Value);
            Assert.AreEqual(0.875,
                            ship.Attributes.First(x => x.AttributeName == "shieldEmDamageResonance")
                                .Value);
        }

        [TestMethod]
        public void TestLoadDamageInvulnControl()
        {
            var ship = _service.CreateItem("Rifter");
            //   var damageControl = _service.CreateItem("Damage Control II");
            var item = _service.CreateItem("Adaptive Invulnerability Field II");
            var item2 = _service.CreateItem("Adaptive Invulnerability Field II");
            var item3 = _service.CreateItem("Adaptive Invulnerability Field II");
            var context = new Context()
                              {
                                  Ship = ship
                              };
            //      ship.InstalledItems.Add(damageControl);
            ship.InstalledItems.Add(item);
            ship.InstalledItems.Add(item2);
            ship.InstalledItems.Add(item3);
            ship.Activate(context);

            Assert.AreEqual(0.4289,
                            ship.Attributes.First(x => x.AttributeName == "shieldEmDamageResonance")
                                .Value,
                            0.0001);
        }
    }
}
