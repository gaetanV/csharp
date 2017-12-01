

using Controllers;

using Server.Http.Route;

using System;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

using Server.Tcp.Server;
using Server.Http.Server;
using Server.Http.Decoration;
using Server.Http.Interface;

namespace git
{
    class Program
    {

     

        static void Main(string[] args)
        {
          
            WebServer ws = new WebServer("http://*:8080/");
            GitController c = new GitController();
            ws.Add(c);
            StreamServer d = new StreamServer();
       
            while (true) { }
        }

    }
}