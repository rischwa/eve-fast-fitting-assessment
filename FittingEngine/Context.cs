using System.Collections.Generic;
using FittingEngine.Model;

namespace FittingEngine
{
    public class Context : IContext
    {
        private readonly List<Attack> _attacks = new List<Attack>();
        private readonly List<string> _userErrors = new List<string>();
        public Item Area { get; set; }

        public List<Attack> Attacks
        {
            get { return _attacks; }
        }

        public Item Char { get; set; }
        public Item Other { get; set; }

        public void RaiseUserError(string error)
        {
            _userErrors.Add(error);
        }

        public Item Self { get; set; }
        public Item Ship { get; set; }
        public Item Target { get; set; }

        public IReadOnlyList<string> UserErrors
        {
            get { return _userErrors.AsReadOnly(); }
        }


        public DamageOutput GetDamageOutput()
        {
            //TODO method doesn't belong here
            return DamageAnalysis.GetDamageOutput(this);
        }
    }
}
