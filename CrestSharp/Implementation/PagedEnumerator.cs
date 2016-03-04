using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrestSharp.Model;

namespace CrestSharp.Implementation
{
    public class PagedEnumerator<T> : IEnumerator<T> where T : class, ICrestObject
    {
        private readonly string _baseUri;
        private int _curPageIndex;
        private Page<T> _lastPage;

        public PagedEnumerator(string baseUri)
        {
            _baseUri = baseUri;

            Task.Run(
                                 () =>  Init()
                                                       )
                .Wait();
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {

            if (_curPageIndex + 1 > _lastPage.Items.Count)
            {
                if (_lastPage.Next != null)
                {
                    _curPageIndex = 0;
                    _lastPage = _lastPage.Next;
                }
                else
                {
                    return false;
                }
            }

            Current = _lastPage.Items[_curPageIndex++];

            return true;
        }

        public void Reset()
        {
            Task.Factory.StartNew(async()=>await Init().ConfigureAwait(false)).Unwrap().Wait();
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        private async Task  Init()
        {
            var cached = await Crest.Settings.Cache.Get<Page<T>>(_baseUri).ConfigureAwait(false);
            _lastPage = cached ?? await new Page<T>
                                  {
                                      Href = _baseUri
                                  }.RefreshedAsync().ConfigureAwait(false);
            _curPageIndex = 0;
        }
    }
}