namespace CrestSharp.Model.Implementation
{
    public class CrestDogmaAttribute : CrestImplicitIdNameObject, ICrestDogmaAttribute
    {
        private string _description;
        private int? _unitId;
        private int? _iconId;
        private bool? _highIsGood;
        private bool? _stackable;
        private bool? _published;
        private double? _defaultValue;
         
        public string Description => EnsureLoadedValue(ref _description);

        public int UnitId => EnsureLoadedValue(ref _unitId);

        public int IconId => EnsureLoadedValue(ref _iconId);

        public bool HighIsGood => EnsureLoadedValue(ref _highIsGood);

        public bool Stackable => EnsureLoadedValue(ref _stackable);

        public bool Published => EnsureLoadedValue(ref _published);

        public double DefaultValue => EnsureLoadedValue(ref _defaultValue);
    }
}
