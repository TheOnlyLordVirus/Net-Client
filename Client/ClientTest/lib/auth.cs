using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTest.lib
{
    class Auth
    {
        public Auth()
        {
            // Get host related information.
            IPAddress ipAddress = IPAddress.Parse("142.93.158.149");
            IPEndPoint authApiEndpoint = new IPEndPoint(ipAddress, 5060);

            // Create the socket.
            Socket senderSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the endpoint with our socket.
            try
            {
                senderSocket.Connect(authApiEndpoint);
                byte[] data = Encoding.ASCII.GetBytes("Hello To Server");
                senderSocket.Send(data);
            }

            // We failed to connect.
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Failed to create a open socket (node) on: " + ipAddress.MapToIPv4() + ":5060");
            }
        }
    }
}
