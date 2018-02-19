using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Globalization;

namespace lab3
{

    class Client 
    {
        public static int checkhash = 0;
        public static void SendResult(ArrayList Result, NetworkStream stream)
        {
            foreach (string a in Result)
            {
                Byte[] data = System.Text.Encoding.UTF32.GetBytes(a);
                stream.Write(data, 0, data.Length);
            }
            Byte[] check = System.Text.Encoding.UTF32.GetBytes("=");
            stream.Write(check, 0, check.Length);
        }
        public static bool hashEqual(byte[] a, byte[] b)
        {
            if (checkhash == -1)
                checkhash = 0;
            bool Equal = false;
            if (a.Length == b.Length)
            {
                int i = 0;
                while ((i < a.Length) && (a[i] == b[i]))
                    i++;
                if (i == a.Length)
                    Equal = true;
                if (Equal)
                {
                    Console.WriteLine("Хэш совпадает");
                    checkhash++;
                }
                else
                    Console.WriteLine("Хэш не совпадает");
            }
            else Console.WriteLine("Хэш не совпадает");
            return Equal;
        }
        public static List<Bill> RecieveBill (NetworkStream stream)
        {
            if (checkhash == -1)
                checkhash = 0;
            var bills = new List<Bill>();
            var bytes = new Byte[256];
            var hash = new Byte[20];
            string datat = null;
            int i;
            bool check = true;
            while (check)
            {
                if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    datat += System.Text.Encoding.UTF32.GetString(bytes, 0, i);
                    var flds = datat.Split('!');
                    if (flds[flds.Length - 1] == "=")
                    {
                        stream.Read(hash, 0, hash.Length);
                        Console.WriteLine(Encoding.UTF8.GetString(hash));
                        check = false;
                        for (int counter = 0; counter < flds.Length - 1; counter += 4)
                        {
                            bills.Add(new Bill
                            {
                                BillName = flds[counter].ToString(),
                                BillDate = DateTime.ParseExact(flds[1 + counter], "dd.MM.yyyy", null),
                                BillNum = flds[2 + counter].ToString(),
                                BillSum = Double.Parse(flds[3 + counter])
                            });
                        }
                        var hp = new System.Security.Cryptography.SHA1Managed();
                        Byte[] datathash = hp.ComputeHash(Encoding.UTF8.GetBytes(datat));
                        hashEqual(hash, datathash);
                    }
                    
                }
            }
            Console.WriteLine("Cчета: ");
            for (int k = 0; k < bills.Count; k++)
                Console.WriteLine(bills[k]);
            Console.WriteLine("Передача счетов окончена");
            return bills;
        }
        public static List<Payment> RecievePayment(NetworkStream stream)
        {
            if(checkhash == -1)
             checkhash = 0;
            var payments = new List<Payment>();
            var bytes = new Byte[256];
            var hash = new Byte[20];
            string datat = null;
            int i;
            bool check = true;
            while (check)
            {
                if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    datat += System.Text.Encoding.UTF32.GetString(bytes, 0, i);
                    var flds = datat.Split('!');
                    if (flds[flds.Length - 1] == "=")
                    {
                        stream.Read(hash, 0, hash.Length);
                        Console.WriteLine(Encoding.UTF8.GetString(hash));
                        check = false;
                        for (int counter = 0; counter < flds.Length - 1; counter += 4)
                        {
                            payments.Add(new Payment
                                {
                                    PayName = flds[0 + counter].ToString(),
                                    PayDate = DateTime.ParseExact(flds[1 + counter], "dd.MM.yyyy", null),
                                    PayNum = flds[2 + counter].ToString(),
                                    PaySum = Double.Parse(flds[3 + counter])
                                });
                        }
                        var hp = new System.Security.Cryptography.SHA1Managed();
                        Byte[] datathash = hp.ComputeHash(Encoding.UTF8.GetBytes(datat));
                        hashEqual(hash, datathash);
                    }

                }
                else check = false;
            }
            Console.WriteLine(datat);
            Console.WriteLine("Платежи: ");
            for (int k = 0; k < payments.Count; k++)
                Console.WriteLine(payments[k]);
            Console.WriteLine("Передача платежей окончена");
            return payments;
        }
    }   
}
