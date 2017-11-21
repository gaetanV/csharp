using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace git
{
    class Program
    {

        static void Main(string[] args)
        {
            Boot();   
            while (true) { }
        }

        private static async void Boot(){
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/");
            listener.Start();
            while (listener.IsListening)
            {
                try
                {
                    HttpListenerContext res = await listener.GetContextAsync();
                    string rstr = await ProcessRepositories(res.Request);
                    byte[] buf = Encoding.UTF8.GetBytes(rstr);
                    res.Response.Headers.Add("Content-type", "application/json");
                    res.Response.StatusCode = (int)HttpStatusCode.OK;
                    res.Response.ContentLength64 = buf.Length;
                    res.Response.OutputStream.Write(buf, 0, buf.Length);
                    res.Response.OutputStream.Flush();
                    res.Response.OutputStream.Close();
                }
                catch { }
            }
          
        }

        private static async Task<string> ProcessRepositories(HttpListenerRequest request)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Github Travis");
            try {
                return await client.GetStringAsync("https://api.github.com/users/gaetanV");
            } catch {
                return "{}";
            }
        }

    }
}
