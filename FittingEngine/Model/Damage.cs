using System;

namespace FittingEngine.Model
{
    public class Damage
    {
        public double AlphaDamage
        {
            get { return (EM + Explosive + Kinetic + Thermal); }
        }

        public TimeSpan CycleTime { get; set; }

        public double DamagePerSecond
        {
            get { return AlphaDamage / CycleTime.TotalSeconds; }
        }

        public double EM { get; set; }

        public double Explosive { get; set; }

        public double Kinetic { get; set; }

        public double Thermal { get; set; }
    }
}