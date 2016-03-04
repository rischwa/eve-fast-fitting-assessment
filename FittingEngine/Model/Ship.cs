using System.Collections.Generic;
using CrestSharp.Model;

namespace FittingEngine.Model
{
    internal enum HullDamageResonanceAttribute
    {
        KineticDamage = 109,
        ThermalDamage = 110,

        ExplosiveDamage = 111,
        EmDamage = 113
    }

    internal enum ArmorDamageResonanceAttribute
    {
        KineticDamage = 269,
        ThermalDamage = 270,

        ExplosiveDamage = 268,
        EmDamage = 267
    }

    internal enum ShieldDamageResonanceAttribute
    {
        KineticDamage = 273,
        ThermalDamage = 274,

        ExplosiveDamage = 272,
        EmDamage = 271
    }

    public class Ship : Item
    {
        private const int HULL_HP_ATTRIBUTE_ID = 9;
        private const int ARMOR_HP_ATTRIBUTE_ID = 265;
        private const int SHIELD_CAPACITY_ATTRIBUTE_ID = 263;
        private const int SHIELD_CHARGE_ATTRIBUTE_ID = 264;
        private const int MAX_VELOCITY_ATTRIBUTE_ID = 37;

        public Ship(ICrestType type, int categoryId, int groupId) : base(type, groupId, categoryId)
        {
            AddAttributeRange(new[] {new Attribute(this, SHIELD_CHARGE_ATTRIBUTE_ID, "shieldCharge", true, true, 0.0)});
        }

        public Tank Tank => new Tank
                            {
                                HullHp = GetAttributeById(HULL_HP_ATTRIBUTE_ID)
                                    .Value,
                                ArmorHp = GetAttributeById(ARMOR_HP_ATTRIBUTE_ID)
                                    .Value,
                                ShieldHp = GetAttributeById(SHIELD_CAPACITY_ATTRIBUTE_ID)
                                    .Value,
                                HullResitanceProfile = GetHullResistanceProfile(),
                                ArmorResitanceProfile = GetArmorResistanceProfile(),
                                ShieldResitanceProfile = GetShieldResistanceProfile(),
                                ShieldRecharge = GetShieldRecharge(),
                                ShieldBoostPerSecond = GetShieldBoostPerSecond(),
                                ArmorRepPerSecond = GetArmorRepPerSecond(),
                                HullRepPerSecond = GetHullRepPerSecond()
                            };


        public double MaxVelocityInMetersPerSecond => GetAttributeById(MAX_VELOCITY_ATTRIBUTE_ID)
            .Value;

        private double GetHullRepPerSecond()
        {
            double hullRepPerSecond = 0;
            foreach (var curItem in InstalledItems)
            {
                const int HULL_REPAIR_AMOUNT_ATTRIBUTE_ID = 83;
                IAttribute armorRepairAmountAttribute;
                if (curItem.TryGetAttributeById(HULL_REPAIR_AMOUNT_ATTRIBUTE_ID, out armorRepairAmountAttribute))
                {
                    const int DURATION_ATTRIBUTE_ID = 73;
                    var durationInMs = curItem.GetAttributeById(DURATION_ATTRIBUTE_ID)
                        .Value;

                    hullRepPerSecond += armorRepairAmountAttribute.Value * 1000.0 / durationInMs;
                }
            }

            return hullRepPerSecond;
        }

        private double GetArmorRepPerSecond()
        {
            double armorRepPerSecond = 0;
            foreach (var curItem in InstalledItems)
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

            return armorRepPerSecond;
        }

        private double GetShieldBoostPerSecond()
        {
            const int SHIELD_BONUS_ATTRIBUTE_ID = 68;
            double shieldBoostPerSecond = 0;
            foreach (var curItem in InstalledItems)
            {
                IAttribute shieldBoostAttribute;
                if (curItem.TryGetAttributeById(SHIELD_BONUS_ATTRIBUTE_ID, out shieldBoostAttribute))
                {
                    const int DURATION_ATTRIBUTE_ID = 73;
                    var durationInMs = curItem.GetAttributeById(DURATION_ATTRIBUTE_ID)
                        .Value;
                    shieldBoostPerSecond += shieldBoostAttribute.Value * 1000.0 / durationInMs;
                }
            }

            return shieldBoostPerSecond;
        }

        private ShieldRecharge GetShieldRecharge()
        {
            const int SHIELD_RECHARGE_RATE_ATTRIBUTE_ID = 479;

            var shieldRechargeRate = GetAttributeById(SHIELD_RECHARGE_RATE_ATTRIBUTE_ID)
                .Value;

            const int SHIELD_CAPACITY_ATTRIBUTE_ID = 263;
            var shieldCapacity = GetAttributeById(SHIELD_CAPACITY_ATTRIBUTE_ID)
                .Value;
            return new ShieldRecharge(shieldCapacity, shieldRechargeRate);
        }

        private ResistanceProfile GetShieldResistanceProfile()
        {
            return new ResistanceProfile
                   {
                       EmResistance = 1.0 - GetAttributeById((int) ShieldDamageResonanceAttribute.EmDamage)
                                                .Value,
                       KineticResistance = 1.0 - GetAttributeById((int) ShieldDamageResonanceAttribute.KineticDamage)
                                                     .Value,
                       ThermalResistance = 1.0 - GetAttributeById((int) ShieldDamageResonanceAttribute.ThermalDamage)
                                                     .Value,
                       ExplosiveResistance = 1.0 - GetAttributeById((int) ShieldDamageResonanceAttribute.ExplosiveDamage)
                                                       .Value
                   };
        }

        private ResistanceProfile GetArmorResistanceProfile()
        {
            return new ResistanceProfile
                   {
                       EmResistance = 1.0 - GetAttributeById((int) ArmorDamageResonanceAttribute.EmDamage)
                                                .Value,
                       KineticResistance = 1.0 - GetAttributeById((int) ArmorDamageResonanceAttribute.KineticDamage)
                                                     .Value,
                       ThermalResistance = 1.0 - GetAttributeById((int) ArmorDamageResonanceAttribute.ThermalDamage)
                                                     .Value,
                       ExplosiveResistance = 1.0 - GetAttributeById((int) ArmorDamageResonanceAttribute.ExplosiveDamage)
                                                       .Value
                   };
        }

        private ResistanceProfile GetHullResistanceProfile()
        {
            return new ResistanceProfile
                   {
                       EmResistance = 1.0 - GetAttributeById((int) HullDamageResonanceAttribute.EmDamage)
                                                .Value,
                       KineticResistance = 1.0 - GetAttributeById((int) HullDamageResonanceAttribute.KineticDamage)
                                                     .Value,
                       ThermalResistance = 1.0 - GetAttributeById((int) HullDamageResonanceAttribute.ThermalDamage)
                                                     .Value,
                       ExplosiveResistance = 1.0 - GetAttributeById((int) HullDamageResonanceAttribute.ExplosiveDamage)
                                                       .Value
                   };
        }
    }

    public enum WeaponSystem
    {
        Missile,
        Turret,
        Drone,
        Smartbomb,
        Bomb,
        None
    }

    public enum AmmoType
    {
        Faction,
        T2Damage,
        T2Range
    }

    public class DamageOutput
    {
        public IEnumerable<WeaponEntry> Entries { get; set; }

        public class WeaponEntry
        {
            public int WeaponCount { get; set; }

            public WeaponSystem WeaponSystem { get; set; }

            public string WeaponType { get; set; }

            public AmmoEntry[] AmmoEntries { get; set; }

            public class AmmoEntry
            {
                public AmmoType AmmoType { get; set; }

                public string Ammunition { get; set; }

                public Damage Damage { get; set; }

                public double OptimalInKm { get; set; }

                public double FalloffInKm { get; set; }

                public double TrackingInRadPerSecond { get; set; }

                public double FlightTimeInMilliseconds { get; set; }

                public double ExplosionVelocity { get; set; }

                public double VelocityInMeterPerSecond { get; set; }
            }
        }
    }
}
