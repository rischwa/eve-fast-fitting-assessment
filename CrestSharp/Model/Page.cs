using System.Collections.Generic;
using CrestSharp.Model.Implementation;

namespace CrestSharp.Model
{
    public class Page<T> : CrestObject where T:ICrestObject
    {
        private List<T> _items;
        private Page<T> _next;
        private int _pageCount;
        private int _totalCount;

        public int TotalCount
        {
            get
            {
                return EnsureLoadedValue(ref _totalCount);
            }
            set { _totalCount = value; }
        }

       

        public int PageCount
        {
            get
            {
                return EnsureLoadedValue(ref _pageCount);
            }
            set { _pageCount = value; }
        }

        public List<T> Items
        {
            get
            {
                return EnsureLoadedValue(ref _items);
            }
            set { _items = value; }
        }

        public Page<T> Next
        {
            get
            {
                return EnsureLoadedValue(ref _next);
            }
            set { _next = value; }
        }
    }
}