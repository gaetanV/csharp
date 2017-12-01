
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading;
using System.Collections.Generic;
using System.Json;

namespace Stream
{
    class StreamServer {

        private Socket serverSocket;

        public StreamServer()
        {

            Console.WriteLine("TCP OPEN");
            
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 9998);

            listener.Start();
            while (true)
            {
                if (listener.Pending())
                {
                    this.Process(listener.AcceptTcpClient());
                }
                Thread.Sleep(100);
            }
            listener.Stop();
        }
       

        private async void Process(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            Byte[] bytes = new Byte[client.Available];
            stream.Read(bytes, 0, bytes.Length);
            String header = Encoding.UTF8.GetString(bytes);
  
            Console.WriteLine("Http Server Try input connection damn...");

            if (StreamServer.getToken(header) == "root")
            {
                Byte[] response  = StreamServer.SendConnect(
                    Encoding.UTF8.GetBytes ( 
                        new Regex("Sec-WebSocket-Key: (.*)").Match(header).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                    )
                );
                stream.Write(response, 0, response.Length);
            } else{
                client.Close();
            }
           
          
        }

        private static Byte[] SendConnect(Byte[] webSocketKey){
            return Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                                            + "Connection: Upgrade" + Environment.NewLine
                                            + "Upgrade: websocket" + Environment.NewLine
                                            + "Sec-WebSocket-Accept: " + Convert.ToBase64String ( SHA1.Create().ComputeHash (webSocketKey) ) + Environment.NewLine 
                                            + Environment.NewLine);
        }

        private static string getToken(String header)
        {
            var a = new Regex("GET /auth[?]{1}token[=]{1}(.*) ").Match(header).Groups;
            if(a.Count == 2) {
                return a[1].Value;
            }
            return "";
        }

        private static Dictionary<string, string> getCookie(String header)
        {
           Dictionary<string, string> result = new Dictionary<string, string>();
           GroupCollection a = new Regex("Cookie: (.*)").Match(header).Groups;
           if( a.Count > 0){
                foreach (string c in a[1].Value.Trim().Split(' '))
                {
                    string[] d = c.Trim().Split("=");
                    if(d.Length == 2) {
                        result.Add(d[0].Trim(),d[1].Trim());  
                    }
                
                }
            }
           return result;
        }

    }
   



}