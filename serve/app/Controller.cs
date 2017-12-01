using System;
using System.Net;
using System.Threading.Tasks;
using Decoration;
using System.Net.Http;
using System.Net.Http.Headers;
using Interface;
using System.IO;

namespace GIT.Controllers
{

    [Route("/")]
    public class GitController : Controller
    {
        [HttpGet("index.html/")]
        public static async Task<string> getApi()
        {
            return File.ReadAllText("view/index.html");
        }

        
    }
}


