using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Net.Sockets;
using System.Reflection;
using Interface;

namespace AppWebServer
{

    public class WebServer
    {
        private readonly HttpListener listener = new HttpListener();
        private readonly object responderClass;
        private readonly MethodInfo responderMethod;

        public WebServer(string prefixes, object Class, MethodInfo Method)
        {

            if (Class is Controller)
            {
                this.listener.Prefixes.Add(prefixes);
                this.responderClass = Class;
                this.responderMethod = Method;
                this.listener.Start();
            }

        }

        public async void RunAsync()
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
            var task = (Task<string>)responderMethod.Invoke(responderClass, null);
            string rstr = await task;
            byte[] buf = Encoding.UTF8.GetBytes(rstr);
            res.Response.Headers.Add("Content-type", "application/json");
            res.Response.StatusCode = (int)HttpStatusCode.OK;
            res.Response.ContentLength64 = buf.Length;
            res.Response.OutputStream.Write(buf, 0, buf.Length);
            res.Response.OutputStream.Flush();
            res.Response.OutputStream.Close();
        }

        public void Stop()
        {
            this.listener.Stop();
            this.listener.Close();
        }
    }
}