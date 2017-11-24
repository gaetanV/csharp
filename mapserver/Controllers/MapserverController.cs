using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;

namespace mapserver.Controllers
{
    [Route("mapserver/")]
    public class ValuesController : Controller
    {

        [HttpGet("wfs/{x}/{y}")]
        public ActionResult GetWfs(int x,int y)
        {   
            if(y>2 || y<0 || x>2 || x<0 ){
               return Content("ERROR!");
            }
            return File(System.IO.File.ReadAllBytes($"./Ressources/{x}.jpg"), "image/jpg");
        }

        [HttpGet("wms")]
        public ActionResult GetWms(int? x,int? y)
        {   
            if(y == null || y>2 || y<0 || x == null || x>2 || x<0 ){
               return Content("ERROR!");
            }
            return File(System.IO.File.ReadAllBytes($"./Ressources/{x}.jpg"), "image/jpg");
        }
    }
}
