namespace CrestSharp.Model.Implementation
{
    public abstract class CrestNamedObject : CrestObject, ICrestNamedObject
    {

        private string _name;

        public override sealed string Href { get { return base.Href; } set { base.Href = value; } }

        public string Name
        {
            get { return EnsureLoadedValue(ref _name); }
            set { throw new System.NotImplementedException(); }
        }
    }
}