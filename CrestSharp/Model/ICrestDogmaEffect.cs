using Newtonsoft.Json;

namespace CrestSharp.Model
{
    public interface ICrestDogmaEffect : ICrestNameIdObject
    {
        bool IsAssistance { get; }

        string Description { get; }

        bool IsOffensive { get; }

        bool DisallowAutoRepeat { get; }

        bool IsWarpSafe { get; }

        bool ElectronicChange { get; }

        bool RangeChance { get; }

        int EffectCategory { get; }
        
        int PostExpressionId { get; }

        bool Published { get; }
        
        int PreExpressionId { get; }

        string DisplayName { get; }
    }
}
