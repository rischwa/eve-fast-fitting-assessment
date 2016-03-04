using System;
using CrestSharp.Model;

namespace FittingEngine.Model
{
    public enum SkillLevel
    {
        I = 1,
        II = 2,
        III = 3,
        IV = 4,
        V = 5
    }
    [Serializable]
    public class Skill : Item
    {
        public Skill(ICrestType type, int groupId, int categoryId,  SkillLevel skillLevel = SkillLevel.V)
            : base(type, groupId, categoryId)
        {
            AddAttributeRange(
                              new IAttribute[]
                              {
                                  new ConstantAttribute(this, 280, "skillLevel", (int) skillLevel),
                                  new Attribute(this, 276, "skillPoints", true, true, 0)
                              });
        }
    }
}