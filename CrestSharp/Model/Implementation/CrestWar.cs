using System;

namespace CrestSharp.Model.Implementation
{
    public class CrestWar : CrestObject, ICrestWar
    {
        private int? _id;
        private DateTime? _started;
        private DateTime? _finished;
        private DateTime? _declared;
        private bool? _openForAllies;
        private int? _allyCount;
        private bool? _mutual;
        private ICrestWarParticipant _aggressor;
        private ICrestWarParticipant _defender;

        public int Id
        {
            get { return EnsureLoadedValue(ref _id); }
            set { _id = value; }
        }

        public DateTime TimeStarted
        {
            get { return EnsureLoadedValue(ref _started); }
            set { _started = value; }
        }

        public DateTime TimeFinished
        {
            get { return EnsureLoadedValue(ref _finished); }
            set { _finished = value; }
        }

        public bool OpenForAllies
        {
            get { return EnsureLoadedValue(ref _openForAllies); }
            set { _openForAllies = value; }
        }

        public int AllyCount
        {
            get { return EnsureLoadedValue(ref _allyCount); }
            set { _allyCount = value; }
        }

        public DateTime TimeDeclared
        {
            get { return EnsureLoadedValue(ref _declared); }
            set { _declared = value; }
        }

        public bool Mutual
        {
            get { return EnsureLoadedValue(ref _mutual); }
            set { _mutual = value; }
        }

        public ICrestWarParticipant Aggressor
        {
            get { return EnsureLoadedValue(ref _aggressor); }
            set { _aggressor = value; }
        }

        public ICrestWarParticipant Defender
        {
            get { return EnsureLoadedValue(ref _defender); }
            set { _defender = value; }
        }
    }
}
