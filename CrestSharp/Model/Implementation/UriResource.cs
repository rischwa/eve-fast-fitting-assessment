using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrestSharp.Model.Implementation
{
    public class UriResource : IUriResource
    {
        private string _href;

        public string Href
        {
            get { return _href; }
            set { _href = value; }
        }

        public Uri Uri => new Uri(Href);
    }
}
