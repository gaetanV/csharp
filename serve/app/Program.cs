using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Linq;
using AppWebServer;
using Decoration;
using GIT.Controllers;
using Interface;
using Stream;

namespace git
{
    class Program
    {

        public static void Route<Controller>(Controller obj)
        {
            string path = "";
            Type type = typeof(Controller);
            if (!type.IsClass) return;

            Attribute[] lesRoute = Attribute.GetCustomAttributes(type, typeof(RouteAttribute));
            if (lesRoute.Length == 1)
            {
                path = (lesRoute[0] as RouteAttribute).Path;
            }

            MethodInfo[] methods = type.GetMethods();
            foreach (var method in methods)
            {
                Attribute[] get = Attribute.GetCustomAttributes(method, typeof(HttpGetAttribute));
                if (get.Length == 1)
                {
                    WebServer ws = new WebServer("http://*:8080" + path + (get[0] as HttpGetAttribute).Description, obj, method);
                    ws.RunAsync();
                }
            }

        }

        static void Main(string[] args)
        {
            GitController c = new GitController();
            Program.Route(c);
            StreamServer d = new StreamServer();
       
            while (true) { }
        }

    }
}
