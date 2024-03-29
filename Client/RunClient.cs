﻿using Server;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Tests;

namespace Client
{
    public static class RunClient
    {

        private const int Port = 5000; 
        static void Main(string[] args)
        {
            var request = new
            {
                Method = "echo",
                Date = "123445566456456",
                Body = "Hello World"
            };


            while (true)
            {

                //Constraint_ConnectionWithoutRequest_ShouldConnect();
                var client = new TcpClient();
                client.Connect(IPAddress.Loopback, Port);

                var stream = client.GetStream();
                //Console.WriteLine("Send message:");
                //var msg = Console.ReadLine();
                
                var req = JsonSerializer.Serialize(request, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var buffer = Encoding.UTF8.GetBytes(req);
                stream.Write(buffer, 0, buffer.Length);

                //if (msg == "exit") break;

                int rcnt = stream.Read(buffer, 0, buffer.Length);
                var response = Encoding.UTF8.GetString(buffer, 0, rcnt);
                Console.WriteLine("Client ", response);
                //var response = client.ReadResponse();
                Console.WriteLine($"Response from server is: {response}");
                stream.Close();
               
            }
        }

       /* public static Response ReadResponse(this TcpClient client)
        {
            var strm = client.GetStream();
            //strm.ReadTimeout = 250;
            byte[] resp = new byte[2048];
            using (var memStream = new MemoryStream())
            {
                int bytesread = 0;
                do
                {
                    bytesread = strm.Read(resp, 0, resp.Length);
                    memStream.Write(resp, 0, bytesread);

                } while (bytesread == 2048);

                var responseData = Encoding.UTF8.GetString(memStream.ToArray());
                Console.WriteLine(responseData);
                var response = JsonSerializer.Deserialize<Response>(responseData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return response;
            }
        }*/

        /*public static void Constraint_ConnectionWithoutRequest_ShouldConnect()
        {
            TcpClient newClient = new TcpClient();
            newClient.Connect(IPAddress.Loopback, 5000);
            Assert.True(newClient.Connected);
        }*/

    }
}
