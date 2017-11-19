using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using AppWebServer;

namespace git
{
    class Program
    {

        static void Main(string[] args)
        {
            WebServer ws = new WebServer("http://*:8080/", ProcessRepositories);
            ws.RunAsync().Wait();
            //ws.Run().Wait();
            //Console.ReadKey();
            //ws.Stop();
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
