using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server;
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 777);
            server.Start();
            while (true)
            {
                
                Console.Write("\nWaiting for a connection... ");
                ThreadPool.QueueUserWorkItem(Server.ServerProcess, server.AcceptTcpClient());
                Console.Write("Done!");
            }
            server.Stop();
        }
    }
}
