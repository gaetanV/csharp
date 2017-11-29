using System.Text;
using System.Runtime.InteropServices;
using System;

namespace mapserver.Service
{

    public class Mapservices
    {

        [DllImport("./C/imagemagic.dll", CharSet = CharSet.Ansi)]
        private static extern int image (StringBuilder buffer);

        [DllImport("./C/imagemagic.dll")]
        unsafe private static extern int unsafeimage (byte* buffer);

        public string getImagemagicDll(){
           StringBuilder buff = new StringBuilder(256);
           image(buff);
           return buff.ToString();
        }

        unsafe public string getImagemagicDllUnsafe(){
            byte[] buff = new byte[256];
            fixed (byte* a = buff) {
                unsafeimage(a); 
            }
            return Encoding.ASCII.GetString(buff);
        }
        
    }
}
