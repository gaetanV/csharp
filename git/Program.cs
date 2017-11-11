using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace git
{
    class Program
    {
        private static async Task ProcessRepositories()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Github Travis");
            var stringTask = client.GetStringAsync("https://api.github.com/users/gaetanV");
            var msg = await stringTask;
            Console.Write(msg);
        }

        static void Main(string[] args)
        {
            ProcessRepositories().Wait();
        }
    }
}
