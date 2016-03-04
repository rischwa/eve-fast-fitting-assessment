using System;

namespace FittingEngineTest
{
    public class ShieldRecharge
    {
        private static readonly double LONGER_FACTOR = Math.Sqrt(0.22);
        private static readonly double SHORTER_FACTOR = Math.Sqrt(0.2);
        private static readonly double PEAK_FACTOR = Math.Sqrt(0.25);

        public ShieldRecharge(double shieldCapacity, double rechargeRate)
        {
            // 10.0f / (rechargeRate / 1000.0f) * SHIELD_PEAK_RECHARGE * (1 - SHIELD_PEAK_RECHARGE) * capacity;
            var baseRate = 10000.0 / rechargeRate * shieldCapacity;
            PeakRecharge = baseRate * PEAK_FACTOR * (1 - PEAK_FACTOR);

            AverageRechargeOnLongerBattles = baseRate * LONGER_FACTOR * (1 - LONGER_FACTOR);

            AverageRechargeOnShorterBattles = baseRate * SHORTER_FACTOR * (1 - SHORTER_FACTOR);
        }

        public double PeakRecharge { get; }

        public double AverageRechargeOnLongerBattles { get; }

        public double AverageRechargeOnShorterBattles { get; }
    }
}