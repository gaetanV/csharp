using System.Text;
using System.Runtime.InteropServices;

namespace mapserver.Service
{

    public class Mapservices
    {

        [DllImport("./C/imagemagic.dll")]
        private static extern void image (StringBuilder buffer);

        public string getImagemagicDll(){
            StringBuilder buff = new StringBuilder(256);
            image(buff);
            return buff.ToString();
        }

    }
}
