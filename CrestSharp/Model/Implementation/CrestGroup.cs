using System.Collections.Generic;

namespace CrestSharp.Model.Implementation
{
    public class CrestGroup : CrestImplicitIdNameObject, ICrestGroup
    {
        //we use IUriResource instead of ICrestCategory, to not get into infinite loops on deserialization (category->group->category->...)
        private IUriResource _category;
        private string _description;
        private bool? _published;
        private ICollection<ICrestType> _types;

        public ICrestCategory Category
        {
            get
            {
                EnsureInitialization();
                return new CrestCategory { Href = _category.Href };
            }
        }

     
        public string Description => EnsureLoadedValue(ref _description);

        public ICollection<ICrestType> Types => EnsureLoadedValue(ref _types);

        public bool Published => EnsureLoadedValue(ref _published);
    }
}
