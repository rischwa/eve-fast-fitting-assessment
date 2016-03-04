using System;
using System.Collections.Generic;
using System.Linq;

namespace FittingEngine.Model
{
    public interface IAssociations
    {
        void Add(params IAttribute[] attributes);

        void Remove(params IAttribute[] attributes);
    }

    internal class DummyAssociations : IAssociations
    {
        public void Add(params IAttribute[] attributes)
        {
        }

        public void Remove(params IAttribute[] attributes)
        {
        }
    }

    public interface IAttribute
    {
        int AttributeID { get; }

        string AttributeName { get; }

        bool IsHighGood { get; }

        bool IsStackable { get; }

        Item Owner { get; }

        //TODO refactor, should be only add/remove functions public, needs to change behaviour of some expressions though

        IAssociations AdditionAssociations { get; }

        IAssociations SubstractionAssociations { get; }

        IAssociations PostAssignmentAssociations { get; }

        IAssociations PostDivisionAssociations { get; }

        IAssociations PostMultiplicationAssociations { get; }

        IAssociations PostPercentAssociations { get; }

        IAssociations PreAssignmentAssociations { get; }

        IAssociations PreDivisionAssociations { get; }

        IAssociations PreMultiplicationAssociations { get; }

        double Value { get; }

        IAttribute Copy(Item newOwner);

        bool Equals(object obj);

        int GetHashCode();

        void SetValueForced(double? value);

        string ToString();
    }

    [Serializable]
    public class Attribute : IAttribute
    {
        private readonly Associations _additionAssociations;
        private readonly Associations _postAssignmentAssociations;
        private readonly Associations _postDivisionAssociations;
        private readonly Associations _postMultiplicationAssociations;
        private readonly Associations _postPercentAssociations;
        private readonly Associations _preAssignmentAssociations;
        private readonly Associations _preDivisionAssociations;
        private readonly Associations _preMultiplicationAssociations;
        private readonly Associations _substractionAssociations;
        private double? _forcedValue;
        //TODO bool sollten flags sein
        public Attribute(Item owner, int attributeID, string attributeName, bool isStackable, bool isHighGood, double value)
        {
            Owner = owner;
            AttributeID = attributeID;
            AttributeName = attributeName;
            IsStackable = isStackable;
            IsHighGood = isHighGood;
            BaseValue = value;

            _additionAssociations = new Associations(this);
            _postAssignmentAssociations = new Associations(this);
            _postDivisionAssociations = new Associations(this);
            _postMultiplicationAssociations = new Associations(this);
            _postPercentAssociations = new Associations(this);
            _preAssignmentAssociations = new Associations(this);
            _preDivisionAssociations = new Associations(this);
            _preMultiplicationAssociations = new Associations(this);
            _substractionAssociations = new Associations(this);
        }

        private double BaseValue { get; }

        public IAttribute Copy(Item newOwner)
        {
            return new Attribute(newOwner, AttributeID, AttributeName, IsStackable, IsHighGood, BaseValue);
        }

        public int AttributeID { get; }

        public string AttributeName { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Attribute) obj);
        }

        public override int GetHashCode()
        {
            return AttributeID;
        }

        public bool IsHighGood { get; }

        public bool IsStackable { get; }

        public Item Owner { get; }

        public IAssociations AdditionAssociations => _additionAssociations;

        public IAssociations SubstractionAssociations => _substractionAssociations;

        public IAssociations PostAssignmentAssociations => _postAssignmentAssociations;

        public IAssociations PostDivisionAssociations => _postDivisionAssociations;

        public IAssociations PostMultiplicationAssociations => _postMultiplicationAssociations;

        public IAssociations PostPercentAssociations => _postPercentAssociations;

        public IAssociations PreAssignmentAssociations => _preAssignmentAssociations;

        public IAssociations PreDivisionAssociations => _preDivisionAssociations;

        public IAssociations PreMultiplicationAssociations => _preMultiplicationAssociations;

        public void SetValueForced(double? value)
        {
            _forcedValue = value;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}) = {2}", AttributeName, AttributeID, Value);
        }

        public double Value
        {
            get
            {
                if (_forcedValue.HasValue)
                {
                    return _forcedValue.Value;
                }
                //TODO caching, needs propagation of dirty flag through parent associations, because their values will be affected, too
                return CalculateValue();
            }
        }

        protected bool Equals(Attribute other)
        {
            return AttributeID == other.AttributeID;
        }

        private double CalculateValue()
        {
            //TODO integrate max value
            var value = BaseValue;
            var overridingValue = _postAssignmentAssociations.Attributes.FirstOrDefault()
                                  ?? _preAssignmentAssociations.Attributes.FirstOrDefault();
            if (overridingValue != null)
            {
                value = overridingValue.Value;
            }

            value += _additionAssociations.Attributes.Sum(x => x.Value);
            value -= _substractionAssociations.Attributes.Sum(x => x.Value);

            value *= _preMultiplicationAssociations.Attributes.ModifierProduct(this);
            value *= _preDivisionAssociations.Attributes.ModifierProduct(this, x => 1.0 / x.Value);
            value *= _postMultiplicationAssociations.Attributes.ModifierProduct(this);
            value *= _postDivisionAssociations.Attributes.ModifierProduct(this, x => 1.0 / x.Value);
            value *= _postPercentAssociations.Attributes.ModifierProduct(this, x => 1.0 + x.Value / 100.0);

            return value;
        }

        private class Associations : IAssociations
        {
            private readonly Attribute _attribute;
            //TODO sollte das ein set sein??
            //  public readonly ISet<IAttribute> Attributes = new HashSet<IAttribute>();
            public readonly List<IAttribute> Attributes = new List<IAttribute>();

            public Associations(Attribute attribute)
            {
                _attribute = attribute;
            }

            public void Add(params IAttribute[] attributes)
            {
                //foreach (var curAttribute in attributes)
                //{
                //    Attributes.Add(curAttribute);
                //}
                Attributes.AddRange(attributes.Where(x=>!Attributes.Any(a=>a==x)));
            }

            public void Remove(params IAttribute[] attributes)
            {
                //foreach (var curAttribute in attributes)
                //{
                //    Attributes.Remove(curAttribute);
                //}
                Attributes.RemoveAll(attributes.Contains);
            }
        }
    }

    internal static class EnumerableExtension
    {
        private static readonly int[] CATEGORY_IDS_FOR_STACKING_CHECK = {7, 8, 18, 23};

        public static double ModifierProduct(this IEnumerable<IAttribute> enumerable,
                                             IAttribute target,
                                             Func<IAttribute, double> transformValue = null)
        {
            if (transformValue == null)
            {
                transformValue = d => d.Value;
            }

            if (target.IsStackable)
            {
                return enumerable.Product(transformValue);
            }
            var transformed = (from x in enumerable
                               let curValue = transformValue(x)
                               select new
                                      {
                                          NeedsStackingCheck = CATEGORY_IDS_FOR_STACKING_CHECK.Contains(x.Owner.CategoryId),
                                          Value = curValue,
                                          isNegative = (target.IsHighGood && curValue < 1.0) || (!target.IsHighGood && curValue > 1.0)
                                      }).ToArray();
            var negativeStackable = transformed.Where(x => x.isNegative && x.NeedsStackingCheck)
                .OrderByDescending(attribute => (target.IsHighGood) ? attribute.Value : -1 * attribute.Value);

            var stackable = transformed.Where(x => x.NeedsStackingCheck && !x.isNegative)
                .OrderBy(
                         attribute =>
                         (target.IsHighGood || (attribute.Value > 0.0 && attribute.Value < 1.0)) ? attribute.Value : -1 * attribute.Value);

            var normal = transformed.Where(x => !x.NeedsStackingCheck);

            return normal.Product(x => x.Value) * stackable.StackableProduct(x => x.Value) * negativeStackable.StackableProduct(x => x.Value);
        }

        public static double Product<T>(this IEnumerable<T> enumerable, Func<T, double> selector)
        {
            return enumerable.Select(selector)
                .Aggregate(1.0, (current, val) => current * val);
        }

        public static double StackableProduct<T>(this IEnumerable<T> enumerable, Func<T, double> selector)
        {
            var value = 1.0;
            var j = 0;
            foreach (double curVal in enumerable.Select(selector))
            {
                value *= 1.0 + (curVal - 1.0) * Math.Exp(-j * j / 7.1289);
                ++j;
            }

            return value;
        }
    }
}