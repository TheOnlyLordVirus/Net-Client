using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTest.lib
{
    class Auth
    {
        private string Cheese = "add_user";
        private string Parmesan = "{\"email\": \"test123@mail.com\", \"username\": \"New Test\", \"password\": \"pass123\", \"ip_addy\": \"0.0.0.0\", \"admin\": \"true\", \"time_value\": \"7\"}";
        public Auth()
        {
            // Get host related information.
            IPAddress ipAddress = IPAddress.Parse("159.223.114.162");
            IPEndPoint authApiEndpoint = new IPEndPoint(ipAddress, 5060);

            // Create the socket.
            Socket senderSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the endpoint with our socket.
            try
            {
                senderSocket.Connect(authApiEndpoint);
                byte[] data = Encoding.ASCII.GetBytes("pasafarian;cheesetoast;" + Cheese + ";" + Parmesan);
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
