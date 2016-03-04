namespace FittingEngine.Model
{
    public interface IEffect
    {
        int EffectId { get; }
        EffectCategory EffectCategory { get; }
        IExpression PreExpression { get;  }

        IExpression PostExpression { get; }
    }
}