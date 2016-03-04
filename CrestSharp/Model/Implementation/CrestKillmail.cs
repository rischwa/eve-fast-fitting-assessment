using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CrestSharp.Model.Implementation
{
    public class CrestKillmail : CrestObject, ICrestKillmail
    {
        private ICollection<IAttacker> _attackers;
        private int? _killId;
        private DateTime? _killTime;
        private ICrestMoon _moon;
        private ICrestSolarSystem _solarSystem;
        private IVictim _victim;
        private ICrestWar _war;

        public int KillID => EnsureLoadedValue(ref _killId);

        public DateTime KillTime => EnsureLoadedValue(ref _killTime);

        public ICollection<IAttacker> Attackers => EnsureLoadedValue(ref _attackers);

        public ICrestSolarSystem SolarSystem => EnsureLoadedValue(ref _solarSystem);

        public ICrestMoon Moon => EnsureLoadedValue(ref _moon);

        public IVictim Victim => EnsureLoadedValue(ref _victim);

        public ICrestWar War => EnsureLoadedValue(ref _war);
    }
}
