using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Diagnostics;
using System.Text;
namespace mapserver.Controllers
{
    [Route("mapserver/")]
    public class ValuesController : Controller
    {

        [HttpGet("wfs/{x}/{y}")]
        public ActionResult GetWfs(int x,int y)
        {   
            try{
                if(y>2 || y<0 || x>2 || x<0 ) {
                    throw  new ArgumentException("ERROR IN PARAMETER");
                }
                string path = $"./Ressources/{x}.jpg";
                if (System.IO.File.Exists(path))
                {
                    FileStream fs = System.IO.File.OpenRead(path);
                    byte[] buff  = new byte[fs.Length];
                    fs.Read(buff, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    return File(buff, "image/jpg");
                }else{
                    return Content("FILE NOT FOUND!");
                }
              
            } 
            catch (ArgumentException e) {
                return Content($"{e.Message}");
            } 
            catch {
                return Content("ERROR");
            }
         
        }

        [HttpGet("imagemagic")]
        public string getImagemagic(){
            System.Diagnostics.Process imagemagic = new System.Diagnostics.Process();
            imagemagic.StartInfo.FileName = "./C/imagemagic.exe";
            imagemagic.StartInfo.CreateNoWindow = false;
            imagemagic.StartInfo.RedirectStandardOutput = true;
            imagemagic.StartInfo.RedirectStandardError = true;
            imagemagic.Start(); 
            imagemagic.WaitForExit();
            //imagemagic.StandardError.ReadToEnd();
            string output = imagemagic.StandardOutput.ReadToEnd();
            int result = imagemagic.ExitCode;
            if(result == 1){
                return output;
            } else {
                return "ERROR";
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
            catch (FileNotFoundException) {
                return Content("FILE NOT FOUND!");
            }
            catch (ArgumentException e) {
                return Content($"{e.Message}");
            } 
            catch {
                return Content("ERROR");
            }
         
        }
    }
}
