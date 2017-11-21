using System;
using System.Net;
using System.Threading.Tasks;
using Decoration;
using System.Net.Http;
using System.Net.Http.Headers;
using Interface;

namespace GIT.Controllers
{

    [Route("/")]
    public class GitController : Controller
    {
        [HttpGet("git/")]
        public static async Task<string> getApi()
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Github Travis");
            try
            {
                return await client.GetStringAsync("https://api.github.com/users/gaetanV");
            }
            catch
            {
                return "{}";
            }
        }
    }
}


