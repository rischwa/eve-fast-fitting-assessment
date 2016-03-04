using System.Collections.Generic;
using FittingEngine.Model;

namespace FittingEngine
{
    public interface IContext
    {
        Item Area { get; }
        Item Char { get; }
        Item Other { get; }

        void RaiseUserError(string error);

        Item Self { get; }
        Item Ship { get; }
        Item Target { get; }
        IReadOnlyList<string> UserErrors { get; }
        List<Attack> Attacks { get; }
    }
}