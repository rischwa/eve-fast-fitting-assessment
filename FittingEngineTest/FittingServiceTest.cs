using System;
using System.Linq;
using CrestSharp.Caches;
using FittingEngine;
using FittingEngine.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FittingEngineTest
{


    [TestClass]
    public class FittingServiceTest
    {
        private FittingService _service;

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestInitialize]
        public void Init()
        {
            _service = new FittingService();
        }

        [TestMethod]
        public void LoadCharacterTest()
        {
            var character = _service.GetAllVCharacter();
            Assert.AreEqual(414, character.InstalledItems.Count);
        }

        [TestMethod]
        public void TestTurretStats()
        {
            var ship = _service.CreateShip("Republic Fleet Firetail");
            var items = _service.CreateItems(

                                             "150mm Light AutoCannon II", "Gyrostabilizer II"
                                            );

            var context = new Context
            {
                Ship = ship,
                Target = _service.CreateItem("Rifter"),
                Char = _service.GetAllVCharacter(),
                Area = new Item()
            };
            context.Char.InstalledItems.Add(ship);
            ship.InstalledItems.AddRange(items);
            context.Char.Activate(context);
            var damageOutput = context.GetDamageOutput();
            Assert.AreEqual(73.753, damageOutput.Entries.Sum(x => x.AmmoEntries.First().Damage.DamagePerSecond), 0.001);

            
        }

        [TestMethod]
        public void TestShieldBoosterStats()
        {
            var ship = _service.CreateShip("Enyo");
            var items = _service.CreateItems(
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Medium Ancillary Shield Booster",
                                             "Small Shield Booster II",
                                             "Small Ancillary Armor Repairer",
                                             "Coreli A-Type Explosive Plating",
                                             "Small Armor Repairer II",
                                             "Internal Force Field Array I",
                                             "Small Hybrid Burst Aerator I",
                                             "Small Hybrid Collision Accelerator I");

            var context = new Context
                          {
                              Ship = ship,
                              Target = _service.CreateItem("Rifter"),
                              Char = _service.GetAllVCharacter(),
                              Area = new Item()
                          };
            context.Char.InstalledItems.Add(ship);
            ship.InstalledItems.AddRange(items);

            context.Char.Activate(context);

        Assert.AreEqual(145.88, ship.Tank.GetActiveShieldTankPerSecond(), 0.005);
            
        }


        [TestMethod]
        public void TestDamageOutput()
        {

            var start = DateTime.UtcNow;
        //    CrestSharp.Crest.Settings.Cache = new CrestCacheWithSessionCache(new CrestSqliteCache());
        CrestSharp.Crest.Settings.Cache = new CrestSqliteCache();
            var allVCharacter = _service.GetAllVCharacter();
            var copy = allVCharacter.Copy();


            var ship = _service.CreateShip("Ishtar");
            //TODO distinct und bei mehreren dann copy verwenden
            var items = _service.CreateItems("Drone Damage Amplifier II", "Drone Damage Amplifier II", "Dual 180mm AutoCannon II", "Dual 180mm AutoCannon II", "Dual 180mm AutoCannon II");
            var gun = _service.CreateItem("Dual 180mm AutoCannon II");
            ship.InstalledItems.AddRange(items);

            //TODO new DamageAnalysis(_service);
            DamageAnalysis.AddDrones(ship);

            Assert.AreEqual(5, ship.InstalledItems.Count(x => x.TypeName == "Ogre II"));

            var ammo = new Ammo(_service).GetFactionAmmoForWeapon(gun);
            foreach (var curItem in items.Where(x => x.TypeName == gun.TypeName))
            {
                curItem.InstalledItems.Add(ammo);
            }

            var context = new Context
            {
                Ship = ship,
                Target = _service.CreateItem("Rifter"),
                Char = allVCharacter,
                Area = new Item()
            };
            context.Char.InstalledItems.Add(ship);
            context.Char.Activate(context);
            Console.WriteLine($"time: {(DateTime.UtcNow - start).TotalSeconds}s");
            var damage = DamageAnalysis.GetDamageOutput(context);
            Assert.AreEqual(2, damage.Entries.Count());


        }

        [TestMethod]
        public void TestActiveTankStats()
        {
            var ship = _service.CreateShip("Enyo");
            var items = _service.CreateItems(
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Medium Ancillary Shield Booster",
                                             "Small Shield Booster II",
                                             "Small Ancillary Armor Repairer",
                                             "Coreli A-Type Explosive Plating",
                                             "Small Armor Repairer II",
                                             "Internal Force Field Array I",
                                             "Small Nanobot Accelerator I",
                                             "Small Hybrid Collision Accelerator I");

            var context = new Context
                          {
                              Ship = ship,
                              Target = _service.CreateItem("Rifter"),
                              Char = _service.GetAllVCharacter(),
                              Area = new Item()
                          };
            context.Char.InstalledItems.Add(ship);
            ship.InstalledItems.AddRange(items);

            context.Char.Activate(context);

            Assert.AreEqual(202.27, ship.Tank.GetActiveArmorTankPerSecond(), 0.005);
            Assert.AreEqual(145.88, ship.Tank.GetActiveShieldTankPerSecond(), 0.005);
        }

        [TestMethod]
        public void TestArmorRepairerStats()
        {
            var ship = _service.CreateShip("Enyo");
            var items = _service.CreateItems(
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Light Neutron Blaster II",
                                             "Medium Ancillary Shield Booster",
                                             "Small Shield Booster II",
                                             "Small Ancillary Armor Repairer",
                                             "Coreli A-Type Explosive Plating",
                                             "Small Armor Repairer II",
                                             "Internal Force Field Array I",
                                             "Small Nanobot Accelerator I",
                                             "Small Hybrid Collision Accelerator I");

            var context = new Context
                          {
                              Ship = ship,
                              Target = _service.CreateItem("Rifter"),
                              Char = _service.GetAllVCharacter(),
                              Area = new Item()
                          };
            context.Char.InstalledItems.Add(ship);
            ship.InstalledItems.AddRange(items);

            context.Char.Activate(context);

            var resistanceProfile = ship.Tank.ArmorResitanceProfile;

            double armorRepPerSecond = 0;
            foreach (var curItem in ship.InstalledItems)
            {
                const int ARMOR_REPAIR_AMOUNT_ATTRIBUTE_ID = 84;
                const int CHARGED_ARMOR_DAMAGE_MULTIPLIER_ATTRIUBTE_ID = 1886;
                IAttribute armorRepairAmountAttribute;
                if (curItem.TryGetAttributeById(ARMOR_REPAIR_AMOUNT_ATTRIBUTE_ID, out armorRepairAmountAttribute))
                {
                    const int DURATION_ATTRIBUTE_ID = 73;
                    var durationInMs = curItem.GetAttributeById(DURATION_ATTRIBUTE_ID)
                        .Value;

                    IAttribute chargedDamageMultiplier;
                    double multiplier = curItem.TryGetAttributeById(
                                                                    CHARGED_ARMOR_DAMAGE_MULTIPLIER_ATTRIUBTE_ID,
                                                                    out chargedDamageMultiplier)
                                            ? chargedDamageMultiplier.Value
                                            : 1.0;

                    armorRepPerSecond += armorRepairAmountAttribute.Value * multiplier * 1000.0 / durationInMs;
                }
            }
            
            Assert.AreEqual(202.27, Tank.CalculateTankInDps(armorRepPerSecond, resistanceProfile), 0.005);
        }

        [TestMethod]
        public void TestStats()
        {
            var ship = _service.CreateItem("Enyo");
            var items = _service.CreateItems(
                                             "Light Neutron Blaster II",
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

            var context = new Context
                          {
                              Ship = ship,
                              Target = _service.CreateItem("Rifter"),
                              Char = _service.GetAllVCharacter(),
                              Area = new Item()
                          };
            context.Char.InstalledItems.Add(ship);
            ship.InstalledItems.AddRange(items);

            context.Char.Activate(context);

            var hullResistance = new ResistanceProfile
                                 {
                                     EmResistance = 1.0 - ship.GetAttributeById((int) HullDamageResonanceAttribute.EmDamage)
                                                              .Value,
                                     KineticResistance = 1.0 - ship.GetAttributeById((int) HullDamageResonanceAttribute.KineticDamage)
                                                                   .Value,
                                     ThermalResistance = 1.0 - ship.GetAttributeById((int) HullDamageResonanceAttribute.ThermalDamage)
                                                                   .Value,
                                     ExplosiveResistance = 1.0 - ship.GetAttributeById((int) HullDamageResonanceAttribute.ExplosiveDamage)
                                                                     .Value
                                 };

            Assert.AreEqual(0.58, hullResistance.EmResistance, 0.0001);
            Assert.AreEqual(0.58, hullResistance.KineticResistance, 0.0001);
            Assert.AreEqual(0.58, hullResistance.ThermalResistance, 0.0001);
            Assert.AreEqual(0.58, hullResistance.ExplosiveResistance, 0.0001);

            var shieldResistance = new ResistanceProfile
                                   {
                                       EmResistance = 1.0 - ship.GetAttributeById((int) ShieldDamageResonanceAttribute.EmDamage)
                                                                .Value,
                                       KineticResistance = 1.0 - ship.GetAttributeById((int) ShieldDamageResonanceAttribute.KineticDamage)
                                                                     .Value,
                                       ThermalResistance = 1.0 - ship.GetAttributeById((int) ShieldDamageResonanceAttribute.ThermalDamage)
                                                                     .Value,
                                       ExplosiveResistance =
                                           1.0 - ship.GetAttributeById((int) ShieldDamageResonanceAttribute.ExplosiveDamage)
                                                     .Value
                                   };

            Assert.AreEqual(0.115, shieldResistance.EmResistance, 0.0001);
            Assert.AreEqual(0.8673, shieldResistance.KineticResistance, 0.0001);
            Assert.AreEqual(0.646, shieldResistance.ThermalResistance, 0.0001);
            Assert.AreEqual(0.5575, shieldResistance.ExplosiveResistance, 0.0001);

            var armorResistance = new ResistanceProfile
                                  {
                                      EmResistance = 1.0 - ship.GetAttributeById((int) ArmorDamageResonanceAttribute.EmDamage)
                                                               .Value,
                                      KineticResistance = 1.0 - ship.GetAttributeById((int) ArmorDamageResonanceAttribute.KineticDamage)
                                                                    .Value,
                                      ThermalResistance = 1.0 - ship.GetAttributeById((int) ArmorDamageResonanceAttribute.ThermalDamage)
                                                                    .Value,
                                      ExplosiveResistance = 1.0 - ship.GetAttributeById((int) ArmorDamageResonanceAttribute.ExplosiveDamage)
                                                                      .Value
                                  };

            Assert.AreEqual(0.57, armorResistance.EmResistance, 0.0001);
            Assert.AreEqual(0.8603, armorResistance.KineticResistance, 0.0001);
            Assert.AreEqual(0.7205, armorResistance.ThermalResistance, 0.0001);
            Assert.AreEqual(0.567, armorResistance.ExplosiveResistance, 0.0001);

            const int SHIELD_RECHARGE_RATE_ATTRIBUTE_ID = 479;

            var shieldRechargeRate = ship.GetAttributeById(SHIELD_RECHARGE_RATE_ATTRIBUTE_ID)
                .Value;

            const int SHIELD_CAPACITY_ATTRIBUTE_ID = 263;
            var shieldCapacity = ship.GetAttributeById(SHIELD_CAPACITY_ATTRIBUTE_ID)
                .Value;

            var shieldRecharge = new ShieldRecharge(shieldCapacity, shieldRechargeRate);
            
            Assert.AreEqual(
                            5.47,
                            Tank.CalculateTankInDps(shieldRecharge.PeakRecharge, shieldResistance, DamageProfile.OMNI_DAMAGE_PROFILE),
                            0.005);
        }

       
    }
}
