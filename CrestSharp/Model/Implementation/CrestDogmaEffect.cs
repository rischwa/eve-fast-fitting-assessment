namespace CrestSharp.Model.Implementation
{
    public class CrestDogmaEffect : CrestImplicitIdNameObject, ICrestDogmaEffect
    {
        private string _description;
        private bool? _disalowAutoRepeat;
        private string _displayName;
        private int? _effectCategory;
        private bool? _electronicChance;
        private bool? _isAssistance;
        private bool? _isOffensive;
        private bool? _isWarpSafe;
        private int? _postExpression;
        private int? _preExpression;
        private bool? _published;
        private bool? _rangeChance;

        public bool IsAssistance => EnsureLoadedValue(ref _isAssistance);

        public string Description => EnsureLoadedValue(ref _description);

        public bool IsOffensive => EnsureLoadedValue(ref _isOffensive);

        public bool DisallowAutoRepeat => EnsureLoadedValue(ref _disalowAutoRepeat);

        public bool IsWarpSafe => EnsureLoadedValue(ref _isWarpSafe);

        public bool ElectronicChange => EnsureLoadedValue(ref _electronicChance);

        public bool RangeChance => EnsureLoadedValue(ref _rangeChance);

        public int EffectCategory => EnsureLoadedValue(ref _effectCategory);

        public int PostExpressionId => EnsureLoadedValue(ref _postExpression);

        public bool Published => EnsureLoadedValue(ref _published);

        public int PreExpressionId => EnsureLoadedValue(ref _preExpression);

        public string DisplayName => EnsureLoadedValue(ref _displayName);
    }
}
