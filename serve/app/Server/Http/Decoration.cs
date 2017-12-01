using System;

namespace Server.Http.Decoration
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
        public string Path;

        public HttpGetAttribute(string path)
        {
            this.Path = path;

        }

    }
}