using System;

namespace CrestSharp.Model
{
    public interface IUriResource
    {
        string Href { get; set; }

        Uri Uri { get; }
    }
}