using System;

namespace FittingEngine.Model
{
    public class DamageProfile
    {
        public static readonly DamageProfile OMNI_DAMAGE_PROFILE = new DamageProfile(0.25, 0.25, 0.25, 0.25);

        public DamageProfile(double emRatio, double kineticRatio, double thermalRatio, double explosiveRatio)
        {
            if (Math.Abs(emRatio + kineticRatio + thermalRatio + explosiveRatio - 1.0) > 0.0001)
            {
                throw new ArgumentException("The sum of damage ratios in damage profile must equal 1");
            }
            EmRatio = emRatio;
            KineticRatio = kineticRatio;
            ThermalRatio = thermalRatio;
            ExplosiveRatio = explosiveRatio;
        }

        public double EmRatio { get; }

        public double KineticRatio { get; }

        public double ThermalRatio { get; }

        public double ExplosiveRatio { get; }
    }
}