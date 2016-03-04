using System;

namespace FittingEngine.Model
{
    [Serializable]
    public class ConstantAttribute : IAttribute
    {
        private static readonly IAssociations DUMMY_ASSOCIATIONS = new DummyAssociations();
        public ConstantAttribute()
        {
        }

        public ConstantAttribute(Item owner, int attributeID, string attributeName, double value)
        {
            Owner = owner;
            AttributeID = attributeID;
            AttributeName = attributeName;
            Value = value;
        }

        public IAttribute Copy(Item newOwner)
        {
            return new ConstantAttribute(newOwner, AttributeID, AttributeName, Value);
        }

        public int AttributeID { get; }

        public string AttributeName { get; }

        public bool IsHighGood
        {
            get { return true; }
        }

        public bool IsStackable
        {
            get { return true; }
        }

        public Item Owner { get; }

        public IAssociations AdditionAssociations => DUMMY_ASSOCIATIONS;

        public IAssociations SubstractionAssociations => DUMMY_ASSOCIATIONS;

        public IAssociations PostAssignmentAssociations => DUMMY_ASSOCIATIONS;

        public IAssociations PostDivisionAssociations => DUMMY_ASSOCIATIONS;

        public IAssociations PostMultiplicationAssociations => DUMMY_ASSOCIATIONS;

        public IAssociations PostPercentAssociations => DUMMY_ASSOCIATIONS;

        public IAssociations PreAssignmentAssociations => DUMMY_ASSOCIATIONS;

        public IAssociations PreDivisionAssociations => DUMMY_ASSOCIATIONS;

        public IAssociations PreMultiplicationAssociations => DUMMY_ASSOCIATIONS;

        public void SetValueForced(double? value)
        {
        }

        public double Value { get; }

      
    }
}