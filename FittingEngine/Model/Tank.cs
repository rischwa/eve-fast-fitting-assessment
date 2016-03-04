namespace FittingEngine.Model
{
    public class Tank
    {
        public ResistanceProfile ShieldResitanceProfile { get; set; }

        public ResistanceProfile ArmorResitanceProfile { get; set; }

        public ResistanceProfile HullResitanceProfile { get; set; }

        public ShieldRecharge ShieldRecharge { get; set; }

        public double ShieldHp { get; set; }

        public double ArmorHp { get; set; }

        public double HullHp { get; set; }

        public double GetEhp(DamageProfile damageProfile = null)
        {
            return GetHullEhp(damageProfile) + GetShieldEhp(damageProfile) + GetArmorEhp(damageProfile);
        }

        public double GetShieldEhp(DamageProfile damageProfile = null)
        {
            return CalculateTankInDps(ShieldHp, ShieldResitanceProfile, damageProfile);
        }

        public double GetArmorEhp(DamageProfile damageProfile = null)
        {
            return CalculateTankInDps(ArmorHp, ArmorResitanceProfile, damageProfile);
        }

        public double GetHullEhp(DamageProfile damageProfile = null)
        {
            return CalculateTankInDps(HullHp, HullResitanceProfile, damageProfile);
        }

        public double ShieldBoostPerSecond { get; set; }

        public double ArmorRepPerSecond { get; set; }

        public double HullRepPerSecond { get; set; }

        public double GetActiveShieldTankPerSecond(DamageProfile damageProfile = null)
        {
            return CalculateTankInDps(ShieldBoostPerSecond, ShieldResitanceProfile, damageProfile);
        }

        public double GetActiveArmorTankPerSecond(DamageProfile damageProfile = null)
        {
            return CalculateTankInDps(ArmorRepPerSecond, ArmorResitanceProfile, damageProfile);
        }

        public double GetPeakPassiveTank(DamageProfile damageProfile = null)
        {
            return CalculateTankInDps(ShieldRecharge.PeakRecharge, ShieldResitanceProfile, damageProfile);
        }

        public static double CalculateTankInDps(double healPerSecond,
                                                ResistanceProfile resistanceProfile,
                                                DamageProfile damageProfile = null)
        {
            damageProfile = damageProfile ?? DamageProfile.OMNI_DAMAGE_PROFILE;
            return healPerSecond
                   / (damageProfile.EmRatio * (1.0 - resistanceProfile.EmResistance)
                      + damageProfile.KineticRatio * (1.0 - resistanceProfile.KineticResistance)
                      + damageProfile.ThermalRatio * (1.0 - resistanceProfile.ThermalResistance)
                      + damageProfile.ExplosiveRatio * (1.0 - resistanceProfile.ExplosiveResistance));
        }

        public double GetActiveHullTankPerSecond(DamageProfile damageProfile = null)
        {
            damageProfile = damageProfile ?? DamageProfile.OMNI_DAMAGE_PROFILE;
            return CalculateTankInDps(HullRepPerSecond, HullResitanceProfile, damageProfile);
        }
    }
}