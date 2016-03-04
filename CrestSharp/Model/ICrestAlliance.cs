using System.Collections.Generic;

namespace CrestSharp.Model
{
    public interface ICrestAlliance : ICrestNameIdIconObject
    {
        bool Deleted { get; }
        string ShortName { get; }
        ICollection<ICrestCorporation> Corporations { get; }
    }
}