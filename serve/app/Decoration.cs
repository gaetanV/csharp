using System;

namespace Decoration
{

    public class RouteAttribute : Attribute
    {
        public string Path;

        public RouteAttribute(string path)
        {
            this.Path = path;
        }

    }

    public class HttpGetAttribute : Attribute
    {
        public string Description;

        public HttpGetAttribute(string myValue)
        {
            this.Description = myValue;

        }

    }
}