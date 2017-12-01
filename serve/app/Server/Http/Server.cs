using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;

using Server.Http.Interface;
using Server.Http.Route;

namespace Server.Http.Server
{

    public class WebServer
    {
        private readonly HttpListener listener = new HttpListener();
        private List<RouteReflect>[] routes = new List<RouteReflect>[6];
        
        public WebServer(string prefixes)
        {
                this.listener.Prefixes.Add(prefixes);
                this.listener.Start();
                this.RunAsync();
        }

        public void Add<Controller>(Controller Class) {
            List<RouteReflect> b = RouteReflexion.reflect(Class);
            foreach (RouteReflect item in b){
                if( this.routes[item.Lv] == null){
                    this.routes[item.Lv] = new List<RouteReflect>();
                }
                this.routes[item.Lv].Add(item);
            }
        }

        private async void RunAsync()
        {
            Console.WriteLine("Webserver start...");
            while (this.listener.IsListening)
            {
                try
                {
                    HttpListenerContext context = await this.listener.GetContextAsync();
                    //HttpListenerContext context = this.listener.GetContext();
                    this.Process(context);
                }
                catch { }
            }
        }

        private async void Process(HttpListenerContext res)
        {
            if( this.routes[res.Request.Url.Segments.Length] != null){
                foreach (RouteReflect item in this.routes[res.Request.Url.Segments.Length])
                {
                    Match match = new Regex(item.Path).Match(res.Request.Url.OriginalString);
                    if (match.Success) {
                        Console.WriteLine("Let's go...");
                        string rstr = await (Task<string>)item.Method.Invoke(item.Class, null);
                        byte[] buf = Encoding.UTF8.GetBytes(rstr);
                        res.Response.Headers.Add("Content-type", "text/html");
                        res.Response.StatusCode = (int)HttpStatusCode.OK;
                        res.Response.ContentLength64 = buf.Length;
                        res.Response.OutputStream.Write(buf, 0, buf.Length);
                        res.Response.OutputStream.Flush();
                        res.Response.OutputStream.Close();
                    }
                }
            }
            res.Response.OutputStream.Close();

        }

        public void Stop()
        {
            this.listener.Stop();
            this.listener.Close();
        }
    }
}