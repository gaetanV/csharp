using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

using Server.Http.Decoration;
using Server.Http.Interface;

namespace Controllers
{

    [Route("/")]
    public class GitController : Controller
    {
        [HttpGet("index.html")]
        public static async Task<string> getApi()
        {
            return File.ReadAllText("view/index.html");
        }

    }
}


