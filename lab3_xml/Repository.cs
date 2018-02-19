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

namespace lab3
{
    class Repository 
    {
        public static List<Payment> ReadPay()
        {
            var payments = new List<Payment>();
            using (StreamReader sr = new StreamReader("Лаб_3_Платежи.csv"))
            {
                sr.ReadLine();
                string line;
                //Console.WriteLine("Список платежей: ");
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    var flds = line.Split(';');
                    payments.Add(new Payment
                    {
                        PayName = flds[0],
                        PayDate = DateTime.ParseExact(flds[1], "dd.MM.yyyy", null),
                        PayNum = flds[2],
                        PaySum = Double.Parse(flds[3], CultureInfo.InvariantCulture)
                    });
                }
            }
            return payments;
        }


        public static List<Bill> ReadBill()
        {
            var bills = new List<Bill>();
            var doc = new XmlDocument();
            doc.Load("Лаб_3_Счета.xml");
            foreach (XmlElement e in doc.SelectNodes("Bills/Bill"))
            {
                bills.Add(new Bill
                {
                    BillName = e.Attributes["Client"].Value,
                    BillDate = DateTime.ParseExact(e.Attributes["Date"].Value, "dd.MM.yyyy", null),
                    BillNum = e.Attributes["Number"].Value,
                    BillSum = Double.Parse(e.Attributes["Sum"].Value, CultureInfo.InvariantCulture)
                });
            }
            return bills;
        }

        /*public static ArrayList ResultList(List<Bill> bills, List<Payment> payments, bool IsServer)
        {
            var toserver = new ArrayList();
            bills.Sort((x, y) => x.BillDate.CompareTo(y.BillDate));
            payments.Sort((x, y) => x.PayDate.CompareTo(y.PayDate));
            if (IsServer)
            {
                for (int i = 0; i < bills.Count; i++)
                {
                    for (int j = 0; j < payments.Count; j++)
                    {
                        if (payments[j].PaySum != 0)
                        {
                            if (i != 0 && j != 0)
                            {
                                if ((bills[i].BillNum != bills[i - 1].BillNum) && (payments[j].PayNum != payments[j-1].PayNum))
                                {
                                    toserver.Add(payments[j].PayName.ToString() + "!");
                                    toserver.Add(payments[j].PayDate.ToString("dd.MM.yyyy") + "!");
                                    toserver.Add(payments[j].PayNum.ToString() + "!");
                                    toserver.Add(bills[i].BillDate.ToString("dd.MM.yyyy") + "!");
                                    toserver.Add(bills[i].BillNum.ToString() + "!");
                                    toserver.Add(payments[j].PaySum.ToString(CultureInfo.InvariantCulture) + "!");
                                }
                            }
                        }
                    }
                }
            }
            else
                for (int i = 0; i < bills.Count; i++)
                {
                    for (int j = 0; j < payments.Count; j++)
                    {
                        if (i != 0 && j != 0)
                        {
                            if (bills[i].BillName == payments[j].PayName && bills[i].BillSum * payments[j].PaySum != 0)
                            {
                                if (bills[i].BillSum >= payments[j].PaySum)
                                {
                                    toserver.Add(payments[j].PayName.ToString());
                                    toserver.Add(payments[j].PayDate.ToString("dd.MM.yyyy"));
                                    toserver.Add(payments[j].PayNum.ToString());
                                    toserver.Add(bills[i].BillDate.ToString("dd.MM.yyyy"));
                                    toserver.Add(bills[i].BillNum.ToString());
                                    toserver.Add(payments[j].PaySum.ToString(CultureInfo.InvariantCulture));
                                }
                            }
                        }
                    }
                }
                return toserver;
        }*/

        public static void WriteXML(ArrayList result)
        {

            XmlDocument doc = new XmlDocument();
            XmlElement els = doc.CreateElement("Платежи");
            for (int i = 0; i < result.Count; i+=6)
            {
                XmlElement child = doc.CreateElement("Платеж");
                child.SetAttribute("Имя_клиента", result[i].ToString());
                child.SetAttribute("Дата_платежа", result[i+1].ToString());
                child.SetAttribute("Номер_платежа", result[i+2].ToString());
                child.SetAttribute("Дата_счета", result[i+3].ToString());
                child.SetAttribute("Номер_счета", result[i+4].ToString());
                child.SetAttribute("Сумма_платежа", result[i+5].ToString());
                els.AppendChild(child);

            }
            doc.AppendChild(els);
            doc.Save("Payments.xml");
            Console.WriteLine("Создание XML файла завершено.");
        }
    }
}

