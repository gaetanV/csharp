using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using mapserver.Service;

namespace mapserver.Controllers
{
    [Route("mapserver/")]
    public class MapserverController : Controller
    {

        private Mapservices Mapservices = new Mapservices();

        [HttpGet("wfs/{x}/{y}")]
        public ActionResult GetWfs(int x,int y)
        {   
            try{
                if(y>2 || y<0 || x>2 || x<0 ) {
                    throw  new ArgumentException("ERROR IN PARAMETER");
                }
                return File(System.IO.File.ReadAllBytes($"./Ressources/{x}.jpg"), "image/jpg");
            } 
            catch (ArgumentException e) {
                return BadRequest($"{e.Message}");
            } 
            catch {
                return NotFound();
            }
        }

        [HttpGet("wms")]
        public ActionResult GetWms(int? x,int? y)
        {   
            try{
                if(y == null || y>2 || y<0 || x == null || x>2 || x<0 ) {
                    throw  new ArgumentException("ERROR IN PARAMETER");
                }
                return File(System.IO.File.ReadAllBytes($"./Ressources/{x}.jpg"), "image/jpg");
            } 
            catch (ArgumentException e) {
                return BadRequest($"{e.Message}");
            } 
            catch {
               return NotFound();
            }
        }

        [HttpGet("imagemagic/string")]
        public string getImagemagic(){
            return Mapservices.getImagemagicDll();
        }

        [HttpGet("imagemagic/unsafe")]
        public string getImagemagicUnsafe(){
            return Mapservices.getImagemagicDllUnsafe();
        }

    }
}
