using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Socket.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var clients = new List<IWebSocketConnection>();


            var server = new WebSocketServer("ws://0.0.0.0:8181");

            server.Start(socket =>
            {
                socket.OnOpen = () => {
                    clients.Add(socket);
                    Console.WriteLine("Open!"); 
                };
                socket.OnClose = () => {
                    clients.Remove(socket);
                    Console.WriteLine("Close!"); 
                };

                socket.OnMessage = message => socket.Send(message);

                socket.OnError = (ex) => Console.WriteLine(ex.Message);
            });

            Thread.Sleep(15000);
            var socket = clients.FirstOrDefault();

            if (socket == null)
            {
                Console.WriteLine("ckeck socket");
                Console.ReadKey();
            }

            var initAndroids = new
            {
                type = "init.androids",
                num = 24
            };

            string order_androids = JsonSerializer.Serialize(initAndroids);

            socket.Send(order_androids);


            Thread.Sleep(2000);

            for (int i = 1; i <= 10; i++)
            {
                var order1 = new
                {
                    android = i,
                    dock = (i < 3) ? 1 : (i < 5) ? 2 : (i < 7) ? 3 : 4
                };

                string _order1 = JsonSerializer.Serialize(order1);

                socket.Send(_order1);
            }

            Thread.Sleep(4000);

            for (int i = 1; i <= 10; i++)
            {
                var order1 = new
                {
                    android = i + 10,
                    dock = (i < 3) ? 11 : 12
                };

                string _order1 = JsonSerializer.Serialize(order1);

                socket.Send(_order1);
            }

            Console.ReadKey();
        }
    }
}
