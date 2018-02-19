using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Xml.Linq;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace lab3
{

    class Program
    {
        static void Main(string[] args)
        {
            Application.Run(new Form1());
            //RadioButton Local = new RadioButton();
            /*string ip = System.Configuration.ConfigurationManager.ConnectionStrings["TestIP"].ConnectionString.ToString();

            string ver = ConfigurationManager.AppSettings["ver"].ToString();
            Console.WriteLine("Используемая версия: " + ver);
            Console.WriteLine("ip: " + ip);
            int port = Int32.Parse(ConfigurationManager.AppSettings["port"]);
   
            if (ver == "Local")
            {
                Console.WriteLine("Читаем XML");
                List<Bill> bills = Repository.ReadBill();
                Console.WriteLine("Прочитано!");

                Console.WriteLine();

                Console.WriteLine("Читаем csv");
                List<Payment> payments = Repository.ReadPay();
                Console.WriteLine("Прочитано!");

                Console.WriteLine();

                Console.WriteLine("Cчета: ");
                for (int i = 0; i < bills.Count; i++)
                    Console.WriteLine(bills[i]);

                Console.WriteLine();

                Console.WriteLine("Платежи: ");
                for (int i = 0; i < payments.Count; i++)
                    Console.WriteLine(payments[i]);

                Console.WriteLine("Создание XML файла");
                Repository.WriteXML(bills, payments);

                Console.WriteLine("\n _______________________ \n");

                PaymentProcessor.ProcessPayments(bills, payments, false);

                Console.WriteLine("\n _______________________ \n");

                Console.WriteLine("Cчета: ");
                for (int i = 0; i < bills.Count; i++)
                    Console.WriteLine(bills[i]);
                Console.WriteLine("Платежи: ");
                for (int i = 0; i < payments.Count; i++)
                    Console.WriteLine(payments[i]);
            }


            ///////////////////////////////////////////////////////////////////////////////////
            if (ver == "TCP")
            {
                var bills = new List<Bill>();
                var payments = new List<Payment>();
                TcpClient client = new TcpClient(ip, port);
                NetworkStream stream = client.GetStream();
                Byte[] test = System.Text.Encoding.UTF32.GetBytes("0");
                stream.Write(test, 0, test.Length);
                while (true)
                {
                    Console.Write("Меню: \n1 - Получение счетов\n2 - Получение платежей\n3 - Отправка результатов\n4 - Выход\n");
                    String message = Console.ReadLine();
                    if (int.Parse(message) == 1)
                    {
                        Byte[] data = System.Text.Encoding.UTF32.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        bills = Client.RecieveBill(stream);
                    }
                    if (int.Parse(message) == 2)
                    {
                        Byte[] data = System.Text.Encoding.UTF32.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        payments = Client.RecievePayment(stream);
                    }
                    if (int.Parse(message) == 3)
                    {
                        var Result = PaymentProcessor.ProcessPayments(bills, payments, true);
                        Byte[] data = System.Text.Encoding.UTF32.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        Client.SendResult(Result, stream);
                    }
                    if (int.Parse(message) == 4)
                    {
                        Byte[] data = System.Text.Encoding.UTF32.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                        break;
                    }
                }
            }
            //////

            if (ver == "SQL")
            {
                using (var cnn = new SqlConnection(@"Data Source = localhost\Alexbogs; Database = Lab5Db; Integrated Security=SSPI; User ID = ALEXBOGS\Alexbogs"))
                {
                    cnn.Open();
                    Console.WriteLine("Доступ к БД получен.");
                    var bills = SQLClient.ReadBill(cnn);
                    var payments = SQLClient.ReadPay(cnn);
                    var result = PaymentProcessor.ProcessPayments(bills, payments, false);
                    SQLClient.SQLWrite(result, cnn);

                }
            }*/
        }
    }
}

