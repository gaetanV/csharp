using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace AppWebServer
{
    public class WebServer
    {
        private readonly HttpListener listener = new HttpListener();
        private readonly Func<HttpListenerRequest,Task<string>> responderMethod;
 
        public WebServer(string prefixes, Func<HttpListenerRequest,Task<string>> method)
        {
            this.listener.Prefixes.Add(prefixes);
            this.responderMethod = method;
            this.listener.Start();
        }

        public async void RunAsync(){
            Console.WriteLine("Webserver start...");
            while (this.listener.IsListening){
                try{
                    HttpListenerContext context = await this.listener.GetContextAsync();
                    await this.Process(context);
                } catch {}
            }
        }

        public async void Run()
        {
            Console.WriteLine("Webserver start...");
            while (this.listener.IsListening)
            {
                try {
                    HttpListenerContext context = this.listener.GetContext();
                    await this.Process(context);
                }
                catch {}
            }
        }
   
        private async Task Process(HttpListenerContext res){
            string rstr = await this.responderMethod(res.Request);
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