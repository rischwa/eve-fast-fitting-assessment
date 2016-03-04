using System;
using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface ICrestKillmail : ICrestObject
    {
        int KillID { get; }

        DateTime KillTime { get; }

        ICollection<IAttacker> Attackers { get; }

        ICrestSolarSystem SolarSystem { get; }

        ICrestMoon Moon { get; }

        IVictim Victim { get; }

        ICrestWar War { get; }
    }
}
