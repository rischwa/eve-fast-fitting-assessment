using System;
using System.Collections.Generic;
using System.Linq;
using FittingEngine.Model;

namespace FittingEngine
{
    //TODO non static service
    public class DamageAnalysis
    {
        private const int OPTIMAL_ID = 54;
        private const int FALLOFF_ID = 158;
        private const int TRACKING_ID = 160;
        private const int VELOCITY_ID = 37;
        private const int EXPLOSION_VELOCITY_ID = 170;
        private const int FLIGHT_TIME_ID = 281;
        public static readonly int[] DAMAGE_ATTRIBUT_IDS = {114, 116, 117, 118};
        private static readonly Item _hobgoblin;
        private static readonly Item _hammerhead;
        private static readonly Item _ogre;
        private static readonly Item ALL_V_CHARACTER;

        static DamageAnalysis()
        {
            using (var service = new FittingService())
            {
                ALL_V_CHARACTER = service.GetAllVCharacter();
                _hobgoblin = service.CreateItem("Hobgoblin II");
                _hammerhead = service.CreateItem("Hammerhead II");
                _ogre = service.CreateItem("Ogre II");
            }
        }

        public static Damage GetDamageOfWeapon(Item item)
        {
            IAttribute attribute = item.Attributes.First(x => x.AttributeName == "damageMultiplier");
            var multiplier = attribute.Value;

            var damageByTypes = item.InstalledItems.First()
                .Attributes.Where(x => DAMAGE_ATTRIBUT_IDS.Contains(x.AttributeID))
                .ToDictionary(x => x.AttributeID, x => x.Value);

            var cycleTime = item.Attributes.First(x => x.AttributeName == "speed");

            const double _100_ns_PER_ms = 10000;
            return new Damage
                   {
                       EM = damageByTypes[114] * multiplier,
                       Explosive = damageByTypes[116] * multiplier,
                       Kinetic = damageByTypes[117] * multiplier,
                       Thermal = damageByTypes[118] * multiplier,
                       CycleTime = new TimeSpan((long) (cycleTime.Value * _100_ns_PER_ms))
                   };
        }

        public static DamageOutput GetDamageOutput(Context context)
        {
            var ship = (Ship)context.Ship;

            WeaponSystem weaponSystem;
           
            var entries = ship.InstalledItems.Where(x => TryGetWeaponSystem(x, out weaponSystem))
                .GroupBy(x => x.TypeId)
                .Select(
                        x =>
                        {
                            var entry = GetDamageEntry(context, x.First());
                            entry.WeaponCount = x.Count();
                            return entry;
                        })
                .ToList();

            return new DamageOutput
                   {
                       Entries = entries
                   };
        }

        public static void AddDrones(Ship ship)
        {
            const int DRONE_BANDWIDTH_ID = 1271;
            var bandwidth = ship.GetAttributeById(DRONE_BANDWIDTH_ID)
                .Value;

            if (bandwidth <= 25)
            {
                AddDrones(ship, _hobgoblin, (int) bandwidth / 5);
                return;
            }

            if (bandwidth <= 50)
            {
                AddDrones(ship, _hammerhead, (int) bandwidth / 10);
                AddDrones(ship, _hobgoblin, ((int) bandwidth / 10) / 5);
                return;
            }
            //TODO verteilunug fuer <= 100 sieht normalerweise anders aus
            AddDrones(ship, _ogre, (int) bandwidth / 25);

            var rest = (int) bandwidth % 25;
            AddDrones(ship, _hammerhead, rest / 10);

            var restMed = rest % 10;
            AddDrones(ship, _hobgoblin, restMed / 5);
        }

        public static void AddDrones(Ship ship, Item droneType, int amount)
        {
            for (var i = 0; i < amount; ++i)
            {
                ship.InstalledItems.Add(droneType.Copy());
            }
        }

        public static DamageOutput.WeaponEntry GetDamageEntry(Context context, Item item)
        {
            WeaponSystem weaponSystem;
            if (!TryGetWeaponSystem(item, out weaponSystem))
            {
                throw new ArgumentException("invalid item group for weapon analysis: " + item.GroupId);
            }

            List<DamageOutput.WeaponEntry.AmmoEntry> ammoEntries = new List<DamageOutput.WeaponEntry.AmmoEntry>();

            if (!IsNeedingAmmo(weaponSystem))
            {
                ammoEntries.Add(
                                new DamageOutput.WeaponEntry.AmmoEntry
                                {
                                    Ammunition = "(none)",
                                    Damage = GetDamage(item, item),
                                    OptimalInKm = item.GetAttributeById(OPTIMAL_ID)
                                                      .Value / 1000.0,
                                    FalloffInKm = item.GetAttributeById(FALLOFF_ID)
                                                      .Value / 1000.0,
                                    TrackingInRadPerSecond = item.GetAttributeById(TRACKING_ID)
                                        .Value
                                });
            }
            else
            {
                //TODO create service/DI
                using (var service = new FittingService())
                {
                    var ammo = new Ammo(service);

                    var factionAmmo = ammo.GetFactionAmmoForWeapon(item);
                    if (factionAmmo != null)
                    {
                        var factionEntry = GetAmmoEntry(context, item, factionAmmo);
                        ammoEntries.Add(factionEntry);
                    }

                    var t2Range = ammo.GetLongRangeT2AmmoForWeapon(item);
                    if (t2Range != null)
                    {
                        var t2RangeEntry = GetAmmoEntry(context, item, t2Range);
                        ammoEntries.Add(t2RangeEntry);
                    }

                    var t2Dps = ammo.GetHighDamageT2AmmoForWeapon(item);
                    if (t2Dps != null)
                    {
                        var t2DpsEntry = GetAmmoEntry(context, item, t2Dps);
                        ammoEntries.Add(t2DpsEntry);
                    }
                }
            }

            return new DamageOutput.WeaponEntry
                   {
                       AmmoEntries = ammoEntries.ToArray(),
                       WeaponSystem = weaponSystem,
                       WeaponType = item.TypeName
                   };
        }

        private static DamageOutput.WeaponEntry.AmmoEntry GetAmmoEntry(Context parentContext, Item weapon, Item ammoItem)
        {
            //TODO evtl. ship copy um damage fuer modul etc. richtig zu haben :(
            var context = new Context
                          {
                              Other = weapon
                          };
            weapon.UninstallItems(context);
            weapon.InstalledItems.Add(ammoItem);

            parentContext.Char.Online(parentContext);
            parentContext.Char.Activate(parentContext);


            const double MILLISECONDS_PER_SECOND = 1000.0;
            const double METERS_PER_KM = 1000.0;

            var flightTimeInMilliseconds = (ammoItem.GetAttributeByIdOrNull(FLIGHT_TIME_ID)
                ?.Value).GetValueOrDefault();
            var velocityInMeterPerSecond = (ammoItem.GetAttributeByIdOrNull(VELOCITY_ID)
                ?.Value).GetValueOrDefault();

            return new DamageOutput.WeaponEntry.AmmoEntry
                   {
                       AmmoType = AmmoType.Faction,
                       Ammunition = ammoItem.TypeName,
                       Damage = GetDamage(weapon, ammoItem),
                       OptimalInKm = (weapon.GetAttributeByIdOrNull(OPTIMAL_ID)
                                         ?.Value ?? (flightTimeInMilliseconds * velocityInMeterPerSecond / METERS_PER_KM)) / MILLISECONDS_PER_SECOND,
                       FalloffInKm = (weapon.GetAttributeByIdOrNull(FALLOFF_ID)
                                         ?.Value).GetValueOrDefault() / MILLISECONDS_PER_SECOND,
                       TrackingInRadPerSecond = (weapon.GetAttributeByIdOrNull(TRACKING_ID)
                           ?.Value).GetValueOrDefault(),
                       ExplosionVelocity = (ammoItem.GetAttributeByIdOrNull(EXPLOSION_VELOCITY_ID)
                           ?.Value).GetValueOrDefault(),
                       VelocityInMeterPerSecond = velocityInMeterPerSecond,
                       FlightTimeInMilliseconds = flightTimeInMilliseconds,
                   };
        }

        private static Damage GetDamage(Item item, Item damateTypeItem)
        {
            const int DAMAGE_MULTIPLIER_ID = 64;
            var multiplier = item.GetAttributeByIdOrNull(DAMAGE_MULTIPLIER_ID)?
                .Value ?? 1.0;

            var damageByTypes = damateTypeItem.Attributes.Where(x => DAMAGE_ATTRIBUT_IDS.Contains(x.AttributeID))
                .ToDictionary(x => x.AttributeID, x => x.Value);

            var cycleTime = item.Attributes.First(x => x.AttributeName == "speed"); //TODO getbyid benutzen

            const double _100_ns_PER_ms = 10000;
            return new Damage
                   {
                       EM = damageByTypes[114] * multiplier,
                       Explosive = damageByTypes[116] * multiplier,
                       Kinetic = damageByTypes[117] * multiplier,
                       Thermal = damageByTypes[118] * multiplier,
                       CycleTime = new TimeSpan((long) (cycleTime.Value * _100_ns_PER_ms))
                   };
        }

        private static bool IsNeedingAmmo(WeaponSystem weaponSystem)
        {
            return weaponSystem == WeaponSystem.Turret || weaponSystem == WeaponSystem.Missile;
        }

        public static bool TryGetWeaponSystem(Item item, out WeaponSystem weaponSystem)
        {
            const int ENERGY_WEAPON_GROUP_ID = 53;
            const int PROJECTILE_WEAPON_GROUP_ID = 55;
            const int HYBRID_WEAPON_GROUP_ID = 74;
            const int SMARTBOMB_ID = 72;
            const int COMBAT_DRONE_ID = 100;
            switch (item.GroupId)
            {
                case ENERGY_WEAPON_GROUP_ID:
                case PROJECTILE_WEAPON_GROUP_ID:
                case HYBRID_WEAPON_GROUP_ID:
                    weaponSystem = WeaponSystem.Turret;
                    return true;
                case Ammo.ROCKET_WEAPON_GROUP_ID:
                    weaponSystem = WeaponSystem.Missile;
                    return true;
                case Ammo.LIGHT_MISSILE_WEAPON_GROUP_ID:
                    weaponSystem = WeaponSystem.Missile;
                    return true;
                case Ammo.HEAVY_MISSILE_WEAPON_GROUP_ID:
                    weaponSystem = WeaponSystem.Missile;
                    return true;
                case Ammo.HEAVY_ASSAULT_MISSILE_WEAPON_GROUP_ID:
                    weaponSystem = WeaponSystem.Missile;
                    return true;
                case Ammo.RAPID_LIGHT_MISSILE_WEAPON_GROUP_ID:
                    weaponSystem = WeaponSystem.Missile;
                    return true;
                case Ammo.RAPID_HEAVY_MISSILE_WEAPON_GROUP_ID:
                    weaponSystem = WeaponSystem.Missile;
                    return true;
                case COMBAT_DRONE_ID:
                    weaponSystem = WeaponSystem.Drone;
                    return true;
                case SMARTBOMB_ID:
                    weaponSystem = WeaponSystem.Smartbomb;
                    return true;
                default:
                    weaponSystem = WeaponSystem.None;
                    return false;
            }
        }
    }
}
