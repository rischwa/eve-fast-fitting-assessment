using System;

namespace FittingEngine.Model
{
    [Serializable()]
    public class Effect : IEffect
    {
        public Effect(int effectId, EffectCategory effectCategory)
        {
            EffectId = effectId;
            EffectCategory = effectCategory;
        }

        public int EffectId { get; private set; }
        public EffectCategory EffectCategory { get; private set; }

        public IExpression PreExpression { get; set; }

        public IExpression PostExpression { get; set; }
    }
}