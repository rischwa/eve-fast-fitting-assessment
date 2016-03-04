using System.Collections;
using System.Collections.Generic;
using CrestSharp.Model;

namespace CrestSharp.Implementation
{
    public class PagedEnumerable<T> : IEnumerable<T> where T : class, ICrestObject
    {
        private readonly string _baseUri;

        public PagedEnumerable(string baseUri)
        {
            _baseUri = baseUri;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new PagedEnumerator<T>(_baseUri);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}