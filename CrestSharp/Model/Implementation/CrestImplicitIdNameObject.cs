using System;

namespace CrestSharp.Model.Implementation
{
    public abstract class CrestImplicitIdNameObject : CrestObject
    {
        private int? _id;
        private string _name;

        public int Id
        {
            // ReSharper disable once PossibleInvalidOperationException
            get {
                if (_id == null)
                {
                    InitIdFromHref();
                }
                return _id.Value;
            }
            set { _id = value; }
        }

        public string Name
        {
            get { return EnsureLoadedValue(ref _name); }

            set { _name = value; }
        }

        public override sealed string Href
        {
            get { return base.Href; }
            set
            {
                base.Href = value;
                InitIdFromHref();
            }
        }

        private void InitIdFromHref()
        {
            if (string.IsNullOrEmpty(Href))
            {
                _id = null;
                return;
            }

            var lastIndex = Href.Substring(0, Href.Length -1).LastIndexOf("/", StringComparison.InvariantCulture);

            var idStr = Href.Substring(lastIndex + 1, Href.Length - lastIndex - 2);

            _id = int.Parse(idStr);
        }
    }
}
