
using System;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Collections.Generic;

using Server.Http.Server;
using Server.Http.Decoration;
using Server.Http.Interface;
 
namespace Server.Http.Route
{

    public class RouteReflect
    {
        public string Path { get; set; }
        public object Class { get; set; }
        public MethodInfo Method { get; set; } 
        public int Lv { get; set; } 
    }

    public class RouteReflexion
    {
       
        static public List<RouteReflect> reflect<Controller>(Controller obj) 
        {
            string path = "";
            List<RouteReflect> response = new List<RouteReflect>{};
            Type type = obj.GetType();
            if (!type.IsClass){
                return response;
            }
        
            Attribute[] lesRoute = Attribute.GetCustomAttributes(type, typeof(RouteAttribute));
            if (lesRoute.Length == 1)
            {
                path = (lesRoute[0] as RouteAttribute).Path;
            }
     
            MethodInfo[] methods = type.GetMethods();
         
            foreach (var method in methods)
            {
                    
                Attribute[] get = Attribute.GetCustomAttributes(method, typeof(HttpGetAttribute));
           
                if (get.Length == 1)
                {
                    string finalPath =  path + (get[0] as HttpGetAttribute).Path.Replace("\"", "\\\"").Trim();
                   
                    response.Add (
                        new RouteReflect(){ 
                            Path =  finalPath + "$",
                            Class = obj,
                            Method = method,
                            Lv = finalPath.Split("/").Length
                        }
                    );

                   
                }
            } 
            return response;
        }

       

    }

}