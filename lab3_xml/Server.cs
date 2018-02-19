using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace lab3
{

    class Server
    {
        public static void ProcessRequest(object obj)
        {
            var client = obj as TcpClient;
            Console.WriteLine("Установлено соединение с {0}", client.Client.RemoteEndPoint);
            using (var stream = client.GetStream())
            {
                using (var sr = new System.IO.StreamReader(stream))
                {
                    using (var sw = new System.IO.StreamWriter(stream))
                    {
                        string request = sr.ReadToEnd();
                        Console.WriteLine("Запорос: " + request);

                        if (request == "Bills")
                        {
                            sw.Write(File.ReadAllText(""));
                            Console.WriteLine("Файл счетов отправлен.");
                        }

                        if (request == "Payments")
                        {
                            sw.Write(File.ReadAllText(""));
                            Console.WriteLine("Файл платежей отправлен.");
                        }

                    }
                }
            }
        }
        public static List<Payment> ReadPay()
        {
            var payments = new List<Payment>();



            return payments;
        }
    }   
}
