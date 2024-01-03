//using System;
using UnityEngine;
//using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP.GetAddress
{
    class MyUdpClient

    {


        static public string Start()
        {
            // Set the server IP and port
            IPAddress serverIP = IPAddress.Parse("127.0.0.1");
            int serverPort = 12345;



            // Create a UDP client

            UdpClient client2 = new UdpClient();
            client2.EnableBroadcast = true;

            // Enable broadcasting
            //client2.E = true;
            // Create a discovery message

            string discoveryMessage = "DiscoveryMessage";


            // Convert the message to bytes

            byte[] discoveryBytes = Encoding.ASCII.GetBytes(discoveryMessage);

            // Broadcast the discovery message

            client2.Send(discoveryBytes, discoveryBytes.Length, new IPEndPoint(IPAddress.Broadcast, serverPort));


            Debug.Log("Discovery message sent.");

            // Receive responses from the server

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, serverPort);

            byte[] responseBytes = client2.Receive(ref serverEndPoint);

            string responseMessage = Encoding.ASCII.GetString(responseBytes);
            var ipaddress = serverEndPoint.Address.ToString();
            Debug.Log("Response received from server: " + responseMessage);
            Debug.Log("Press any key to exit.");
            return ipaddress;

        }
        // Set the server IP and port

    }
}
