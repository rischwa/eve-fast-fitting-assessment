using System;

namespace CrestSharp.Model
{
    public interface ICrestWar : ICrestObject
    {
        int Id { get; }

        DateTime TimeStarted { get; }

        DateTime TimeFinished { get; }

        bool OpenForAllies { get; }

        int AllyCount { get; }

        DateTime TimeDeclared { get; }

        bool Mutual { get; }

        ICrestWarParticipant Aggressor { get; }

        ICrestWarParticipant Defender { get; }
    }
}
