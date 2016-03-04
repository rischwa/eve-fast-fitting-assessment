namespace CrestSharp.Model.Implementation
{
    public abstract class CrestNameIdObject : CrestNamedObject, ICrestNameIdObject
    {
        private int? _id;

        public int Id
        {
            get { return EnsureLoadedValue(ref _id); }
            set { throw new System.NotImplementedException(); }
        }
    }
}