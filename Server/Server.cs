using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Xml.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace lab4
{
    class Server 
    {
        public static void ServerProcess(object client_obj)
        {
            var bills = ReadBill();
            var payments = ReadPay();
            
            /*for (int z = 0; z < bills.Count; z++)
            {
                Console.WriteLine(bills[z].ToString());
            }
            for (int z = 0; z < payments.Count; z++)
            {
                Console.WriteLine(payments[z].ToString());
            }*/

            TcpClient client = client_obj as TcpClient;
            NetworkStream stream = client.GetStream();
            int i;
            string data = null;
            Byte[] bytes = new Byte[256];
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = System.Text.Encoding.UTF32.GetString(bytes, 0, i);
                if (Int32.Parse(data) == 0)
                {
                    Console.Write("Клиент подсоединен\n");
                    //string response = "Соединение установлено\n";
                    //byte[] msg = System.Text.Encoding.UTF32.GetBytes(response);
                    //stream.Write(msg, 0, msg.Length);
                }
                if (Int32.Parse(data) == 1)
                {
                    SendBills(bills, stream);
                }
                if (Int32.Parse(data) == 2)
                {
                    SendPayments(payments, stream);
                }
                if (Int32.Parse(data) == 3)
                {
                    var Result = RecieveResult(stream);
                    WriteXML(Result);
                    //PrintResults(Result);
                }
                if (Int32.Parse(data) == 4)
                {
                    Console.WriteLine("Server closing \n");
                    client.Close();
                }
            }
            client.Close();
        }
        public static ArrayList RecieveResult(NetworkStream stream)
        {
            var Result = new ArrayList();
            Byte[] bytes = new Byte[256];
            String data = null;
            int i;
            bool check = true;
            while (check)
            {
                if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data += System.Text.Encoding.UTF32.GetString(bytes, 0, i);
                    var flds = data.Split('!');
                    if (flds[flds.Length-1] == "=")
                    {
                        check = false;
                        for (int k = 0; k < flds.Length - 1; k += 6)
                        {
                            Result.Add(flds[k]);
                            Result.Add(flds[k + 1]);
                            Result.Add(flds[k + 2]);
                            Result.Add(flds[k + 3]);
                            Result.Add(flds[k + 4]);
                            Result.Add(flds[k + 5]);
                        }
                    }
                }
            }
            Console.WriteLine("Результаты получены");
            return Result;
        }
        public static void SendPayments(ArrayList payments, NetworkStream stream)
        {
            string stringtohash = null;
            foreach (string a in payments)
            {
                Byte[] data = System.Text.Encoding.UTF32.GetBytes(a);
                stream.Write(data, 0, data.Length);
                stringtohash += a;
            }
            Byte[] check = System.Text.Encoding.UTF32.GetBytes("=");
            stringtohash += "=";
            stream.Write(check, 0, check.Length);
            Console.WriteLine("Передача платежей окончена");
            var hp = new System.Security.Cryptography.SHA1Managed();
            Byte[] hash = hp.ComputeHash(Encoding.UTF8.GetBytes(stringtohash));
            Console.WriteLine(Encoding.UTF8.GetString(hash));
            stream.Write(hash, 0, hash.Length);
            
            Console.WriteLine("Передача хеша окончена");
        }
        public static void SendBills(ArrayList bills, NetworkStream stream)
        {
            string stringtohash = null;
            foreach (string a in bills)
            {
                Byte[] data = System.Text.Encoding.UTF32.GetBytes(a);
                stream.Write(data, 0, data.Length);
                stringtohash += a;
            }
            Byte[] check = System.Text.Encoding.UTF32.GetBytes("=");
            stringtohash += "=";
            stream.Write(check, 0, check.Length);
            Console.WriteLine("Передача счетов окончена");
            var hp = new System.Security.Cryptography.SHA1Managed();
            Byte[] hash = hp.ComputeHash(Encoding.UTF8.GetBytes(stringtohash));
            Console.WriteLine(Encoding.UTF8.GetString(hash));
            stream.Write(hash, 0, hash.Length);
            Console.WriteLine("Передача хеша окончена");
        }
        public static ArrayList ReadPay()
        {
            var payments = new ArrayList();
            using (StreamReader sr = new StreamReader("Лаб_3_Платежи.csv"))
            {
                sr.ReadLine();
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    var flds = line.Split(';');
                    payments.Add(flds[0].ToString() + "!");
                    payments.Add(DateTime.ParseExact(flds[1], "dd.MM.yyyy", null).ToShortDateString() + "!");
                    payments.Add(flds[2].ToString() + "!");
                    payments.Add(Double.Parse(flds[3], CultureInfo.InvariantCulture).ToString() + "!");
                }
            }
            return payments;
        }
        public static ArrayList ReadBill()
        {
            var bills = new ArrayList();
            var doc = new XmlDocument();
            doc.Load("Лаб_3_Счета.xml");
            foreach ( XmlElement e in doc.SelectNodes("Bills/Bill"))
            {
                bills.Add(e.Attributes["Client"].Value.ToString() + "!");
                bills.Add(DateTime.ParseExact(e.Attributes["Date"].Value, "dd.MM.yyyy", null).ToShortDateString() + "!");
                bills.Add(e.Attributes["Number"].Value.ToString() + "!");
                bills.Add(Double.Parse(e.Attributes["Sum"].Value, CultureInfo.InvariantCulture).ToString() + "!");
            }
            return bills;
        }
        public static void PrintResults(ArrayList Result)
        {
            for (int i = 0; i < Result.Count; i++)
                Console.WriteLine(Result[i]);
        }
        public static void WriteXML(ArrayList Result)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement els = doc.CreateElement("Платежи");
            for (int i = 0; i < Result.Count; i+=6)
            {
                    XmlElement child = doc.CreateElement("Платеж");
                    child.SetAttribute("Имя_клиента", Result[i].ToString());
                    child.SetAttribute("Дата_платежа", Result[i+1].ToString());
                    child.SetAttribute("Номер_платежа", Result[i+2].ToString());
                    child.SetAttribute("Дата_счета", Result[i+3].ToString());
                    child.SetAttribute("Номер_счета", Result[i+4].ToString());
                    child.SetAttribute("Сумма_платежа", Result[i+5].ToString());
                    els.AppendChild(child);
            }
            doc.AppendChild(els);
            doc.Save("Payments.xml");
            Console.WriteLine("Создание XML файла завершено.");
        }
    }
}
