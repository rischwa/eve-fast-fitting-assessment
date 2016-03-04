using System.Collections.Generic;
using FittingEngine.Model;

namespace FittingEngine
{
    public enum ChargeSize
    {
        Small = 1,
        Medium = 2,
        Large = 3,
        XLarge = 4,
        //edge case: missiles don't have charge sizes
        NoSize = 5
    }

    public class Ammo
    {
        private const int CHARGE_SIZE_ID_ATTRIBUTE_ID = 128;
        private const int ENERGY_WEAPON_GROUP_ID = 53;
        private const int PROJECTILE_WEAPON_GROUP_ID = 55;
        private const int HYBRID_WEAPON_GROUP_ID = 74; //SB = 72
        public const int ROCKET_WEAPON_GROUP_ID = 507;
        public const int LIGHT_MISSILE_WEAPON_GROUP_ID = 509;
        public const int HEAVY_MISSILE_WEAPON_GROUP_ID = 510;
        public const int RAPID_LIGHT_MISSILE_WEAPON_GROUP_ID = 511;
        public const int HEAVY_ASSAULT_MISSILE_WEAPON_GROUP_ID = 771;
        public const int RAPID_HEAVY_MISSILE_WEAPON_GROUP_ID = 1245;
        private const int CHARGE_GROUP_2_ID = 605;
        private const int CHARGE_GROUP_3_ID = 606;
        private const int CHARGE_GROUP_4_ID = 609;
        private readonly Dictionary<int, IDictionary<ChargeSize, Item>> _factionAmmoTypesByWeaponType;
        private readonly Dictionary<int, T2> _t2Ammo;

        public Ammo(FittingService service)
        {
            IDictionary<ChargeSize, Item> factionHybridAmmo = new Dictionary<ChargeSize, Item>
                                                              {
                                                                  {
                                                                      ChargeSize.Small,
                                                                      service.CreateItem("Federation Navy Antimatter Charge S")
                                                                  },
                                                                  {
                                                                      ChargeSize.Medium,
                                                                      service.CreateItem("Federation Navy Antimatter Charge M")
                                                                  },
                                                                  {
                                                                      ChargeSize.Large,
                                                                      service.CreateItem("Federation Navy Antimatter Charge L")
                                                                  },
                                                                  {ChargeSize.XLarge, service.CreateItem("Antimatter Charge XL")}
                                                              };

            IDictionary<ChargeSize, Item> factionEnergyAmmo = new Dictionary<ChargeSize, Item>
                                                              {
                                                                  {ChargeSize.Small, service.CreateItem("Imperial Navy Multifrequency S")},
                                                                  {ChargeSize.Medium, service.CreateItem("Imperial Navy Multifrequency M")},
                                                                  {ChargeSize.Large, service.CreateItem("Imperial Navy Multifrequency L")},
                                                                  {ChargeSize.XLarge, service.CreateItem("Multifrequency XL")}
                                                              };

            IDictionary<ChargeSize, Item> factionProjectileAmmo = new Dictionary<ChargeSize, Item>
                                                                  {
                                                                      {ChargeSize.Small, service.CreateItem("Republic Fleet EMP S")},
                                                                      {ChargeSize.Medium, service.CreateItem("Republic Fleet EMP M")},
                                                                      {ChargeSize.Large, service.CreateItem("Republic Fleet EMP L")},
                                                                      {ChargeSize.XLarge, service.CreateItem("EMP XL")}
                                                                  };

            _factionAmmoTypesByWeaponType = new Dictionary<int, IDictionary<ChargeSize, Item>>
                                            {
                                                {HYBRID_WEAPON_GROUP_ID, factionHybridAmmo},
                                                {ENERGY_WEAPON_GROUP_ID, factionEnergyAmmo},
                                                {PROJECTILE_WEAPON_GROUP_ID, factionProjectileAmmo},
                                                {
                                                    LIGHT_MISSILE_WEAPON_GROUP_ID, new Dictionary<ChargeSize, Item>
                                                                                   {
                                                                                       {
                                                                                           ChargeSize.NoSize,
                                                                                           service.CreateItem(
                                                                                                              "Caldari Navy Inferno Light Missile")
                                                                                       }
                                                                                   }
                                                },
                                                {
                                                    HEAVY_MISSILE_WEAPON_GROUP_ID, new Dictionary<ChargeSize, Item>
                                                                                   {
                                                                                       {
                                                                                           ChargeSize.NoSize,
                                                                                           service.CreateItem(
                                                                                                              "Caldari Navy Inferno Heavy Missile")
                                                                                       }
                                                                                   }
                                                },
                                                {
                                                    HEAVY_ASSAULT_MISSILE_WEAPON_GROUP_ID, new Dictionary<ChargeSize, Item>
                                                                                           {
                                                                                               {
                                                                                                   ChargeSize.NoSize,
                                                                                                   service.CreateItem(
                                                                                                                      "Caldari Navy Inferno Heavy Assault Missile")
                                                                                               }
                                                                                           }
                                                },
                                                {
                                                    ROCKET_WEAPON_GROUP_ID, new Dictionary<ChargeSize, Item>
                                                                            {
                                                                                {
                                                                                    ChargeSize.NoSize,
                                                                                    service.CreateItem("Caldari Navy Inferno Rocket")
                                                                                }
                                                                            }
                                                },
                                                {
                                                    RAPID_LIGHT_MISSILE_WEAPON_GROUP_ID, new Dictionary<ChargeSize, Item>
                                                                                         {
                                                                                             {
                                                                                                 ChargeSize.NoSize,
                                                                                                 service.CreateItem(
                                                                                                                    "Caldari Navy Inferno Light Missile")
                                                                                             }
                                                                                         }
                                                },
                                                {
                                                    RAPID_HEAVY_MISSILE_WEAPON_GROUP_ID, new Dictionary<ChargeSize, Item>
                                                                                         {
                                                                                             {
                                                                                                 ChargeSize.NoSize,
                                                                                                 service.CreateItem(
                                                                                                                    "Caldari Navy Inferno Heavy Missile")
                                                                                             }
                                                                                         }
                                                },
                                            };

            //TODO torpedo + bomb

            IDictionary<ChargeSize, Item> t2BlasterHighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                                    {
                                                                        {ChargeSize.Small, service.CreateItem("Void S")},
                                                                        {ChargeSize.Medium, service.CreateItem("Void M")},
                                                                        {ChargeSize.Large, service.CreateItem("Void L")}
                                                                    };

            IDictionary<ChargeSize, Item> t2BlasterRangeAmmo = new Dictionary<ChargeSize, Item>
                                                               {
                                                                   {ChargeSize.Small, service.CreateItem("Null S")},
                                                                   {ChargeSize.Medium, service.CreateItem("Null M")},
                                                                   {ChargeSize.Large, service.CreateItem("Null L")}
                                                               };

            IDictionary<ChargeSize, Item> t2RailgunHighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                                    {
                                                                        {ChargeSize.Small, service.CreateItem("Spike S")},
                                                                        {ChargeSize.Medium, service.CreateItem("Spike M")},
                                                                        {ChargeSize.Large, service.CreateItem("Spike L")}
                                                                    };

            IDictionary<ChargeSize, Item> t2RailgunRangeAmmo = new Dictionary<ChargeSize, Item>
                                                               {
                                                                   {ChargeSize.Small, service.CreateItem("Javelin S")},
                                                                   {ChargeSize.Medium, service.CreateItem("Javelin M")},
                                                                   {ChargeSize.Large, service.CreateItem("Javelin L")}
                                                               };

            IDictionary<ChargeSize, Item> t2PulseHighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                                  {
                                                                      {ChargeSize.Small, service.CreateItem("Conflagration S")},
                                                                      {ChargeSize.Medium, service.CreateItem("Conflagration M")},
                                                                      {ChargeSize.Large, service.CreateItem("Conflagration L")}
                                                                  };

            IDictionary<ChargeSize, Item> t2PulseRangeAmmo = new Dictionary<ChargeSize, Item>
                                                             {
                                                                 {ChargeSize.Small, service.CreateItem("Scorch S")},
                                                                 {ChargeSize.Medium, service.CreateItem("Scorch M")},
                                                                 {ChargeSize.Large, service.CreateItem("Scorch L")}
                                                             };

            IDictionary<ChargeSize, Item> t2BeamHighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                                 {
                                                                     {ChargeSize.Small, service.CreateItem("Gleam S")},
                                                                     {ChargeSize.Medium, service.CreateItem("Gleam M")},
                                                                     {ChargeSize.Large, service.CreateItem("Gleam L")}
                                                                 };

            IDictionary<ChargeSize, Item> t2BeamRangeAmmo = new Dictionary<ChargeSize, Item>
                                                            {
                                                                {ChargeSize.Small, service.CreateItem("Aurora S")},
                                                                {ChargeSize.Medium, service.CreateItem("Aurora M")},
                                                                {ChargeSize.Large, service.CreateItem("Aurora L")}
                                                            };

            IDictionary<ChargeSize, Item> t2ArtilleryHighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                                      {
                                                                          {ChargeSize.Small, service.CreateItem("Quake S")},
                                                                          {ChargeSize.Medium, service.CreateItem("Quake M")},
                                                                          {ChargeSize.Large, service.CreateItem("Quake L")}
                                                                      };

            IDictionary<ChargeSize, Item> t2ArtilleryRangeAmmo = new Dictionary<ChargeSize, Item>
                                                                 {
                                                                     {ChargeSize.Small, service.CreateItem("Tremor S")},
                                                                     {ChargeSize.Medium, service.CreateItem("Tremor M")},
                                                                     {ChargeSize.Large, service.CreateItem("Tremor L")}
                                                                 };

            IDictionary<ChargeSize, Item> t2AutocannonHighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                                       {
                                                                           {ChargeSize.Small, service.CreateItem("Hail S")},
                                                                           {ChargeSize.Medium, service.CreateItem("Hail M")},
                                                                           {ChargeSize.Large, service.CreateItem("Hail L")}
                                                                       };

            IDictionary<ChargeSize, Item> t2AutocannonRangeAmmo = new Dictionary<ChargeSize, Item>
                                                                  {
                                                                      {ChargeSize.Small, service.CreateItem("Barrage S")},
                                                                      {ChargeSize.Medium, service.CreateItem("Barrage M")},
                                                                      {ChargeSize.Large, service.CreateItem("Barrage L")}
                                                                  };

            _t2Ammo = new Dictionary<int, T2>
                      {
                          {
                              372, new T2
                                   {
                                       HighDamageAmmo = t2AutocannonHighDamageAmmo,
                                       LongRangeAmmo = t2AutocannonRangeAmmo
                                   }
                          },
                          {
                              373, new T2
                                   {
                                       HighDamageAmmo = t2RailgunHighDamageAmmo,
                                       LongRangeAmmo = t2RailgunRangeAmmo
                                   }
                          },
                          {
                              374, new T2
                                   {
                                       HighDamageAmmo = t2BeamHighDamageAmmo,
                                       LongRangeAmmo = t2BeamRangeAmmo
                                   }
                          },
                          {
                              375, new T2
                                   {
                                       HighDamageAmmo = t2PulseHighDamageAmmo,
                                       LongRangeAmmo = t2PulseRangeAmmo
                                   }
                          },
                          {
                              376, new T2
                                   {
                                       HighDamageAmmo = t2ArtilleryHighDamageAmmo,
                                       LongRangeAmmo = t2ArtilleryRangeAmmo
                                   }
                          },
                          {
                              377, new T2
                                   {
                                       HighDamageAmmo = t2BlasterHighDamageAmmo,
                                       LongRangeAmmo = t2BlasterRangeAmmo
                                   }
                          },
                          {
                              653, new T2
                                   {
                                       HighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                        {
                                                            {ChargeSize.NoSize, service.CreateItem("Inferno Fury Light Missile")},
                                                        },
                                       LongRangeAmmo = new Dictionary<ChargeSize, Item>
                                                       {
                                                           {ChargeSize.NoSize, service.CreateItem("Inferno Precision Light Missile")},
                                                       }
                                   }
                          },
                          {
                              648, new T2
                                   {
                                       HighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                        {
                                                            {ChargeSize.NoSize, service.CreateItem("Inferno Rage Rocket")},
                                                        },
                                       LongRangeAmmo = new Dictionary<ChargeSize, Item>
                                                       {
                                                           {ChargeSize.NoSize, service.CreateItem("Inferno Javelin Rocket")},
                                                       }
                                   }
                          },
                          {
                              655, new T2
                                   {
                                       HighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                        {
                                                            {ChargeSize.NoSize, service.CreateItem("Inferno Fury Heavy Missile")},
                                                        },
                                       LongRangeAmmo = new Dictionary<ChargeSize, Item>
                                                       {
                                                           {ChargeSize.NoSize, service.CreateItem("Inferno Precision Heavy Missile")},
                                                       }
                                   }
                          },
                          {
                              654, new T2
                                   {
                                       HighDamageAmmo = new Dictionary<ChargeSize, Item>
                                                        {
                                                            {ChargeSize.NoSize, service.CreateItem("Inferno Rage Heavy Assault Missile")},
                                                        },
                                       LongRangeAmmo = new Dictionary<ChargeSize, Item>
                                                       {
                                                           {ChargeSize.NoSize, service.CreateItem("Inferno Javelin Heavy Assault Missile")},
                                                       }
                                   }
                          }
                      };
        }

        public Item GetFactionAmmoForWeapon(Item weapon)
        {
            IDictionary<ChargeSize, Item> chargsizeMapping;
            Item ammo;
            if (!_factionAmmoTypesByWeaponType.TryGetValue(weapon.GroupId, out chargsizeMapping)
                || !chargsizeMapping.TryGetValue(GetChargeSize(weapon), out ammo))
            {
                return null;
            }
            return ammo;
        }

        public Item GetLongRangeT2AmmoForWeapon(Item weapon)
        {
            var t2Ammo = GetT2AmmoTypes(weapon);
            return t2Ammo?.LongRangeAmmo[GetChargeSize(weapon)];
        }

        private static ChargeSize GetChargeSize(Item weapon)
        {
            return (ChargeSize) (int) ((weapon.GetAttributeByIdOrNull(CHARGE_SIZE_ID_ATTRIBUTE_ID)
                                           ?.Value) ?? (int?) ChargeSize.NoSize);
        }

        private T2 GetT2AmmoTypes(Item weapon)
        {
            var attribute = weapon.GetAttributeByIdOrNull(CHARGE_GROUP_2_ID);
            if (attribute != null && _t2Ammo.ContainsKey((int) attribute.Value))
            {
                return _t2Ammo[(int) attribute.Value];
            }

            attribute = weapon.GetAttributeByIdOrNull(CHARGE_GROUP_3_ID);
            if (attribute != null && _t2Ammo.ContainsKey((int) attribute.Value))
            {
                return _t2Ammo[(int) attribute.Value];
            }

            attribute = weapon.GetAttributeByIdOrNull(CHARGE_GROUP_4_ID);
            if (attribute != null && _t2Ammo.ContainsKey((int) attribute.Value))
            {
                return _t2Ammo[(int) attribute.Value];
            }
            return null;
        }

        public Item GetHighDamageT2AmmoForWeapon(Item weapon)
        {
            var t2Ammo = GetT2AmmoTypes(weapon);
            return t2Ammo?.HighDamageAmmo[GetChargeSize(weapon)];
        }

        private class T2
        {
            public IDictionary<ChargeSize, Item> HighDamageAmmo;
            public IDictionary<ChargeSize, Item> LongRangeAmmo;
        }
    }
}
