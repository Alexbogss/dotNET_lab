using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections;

namespace lab3
{
    class PaymentProcessor
    {
        public static ArrayList ProcessPayments(List<Bill> bills, List<Payment> payments, bool IsServer)
        {
            var res = new ArrayList();
            bills.Sort((x, y) => x.BillDate.CompareTo(y.BillDate));
            payments.Sort((x, y) => x.PayDate.CompareTo(y.PayDate));
            if (IsServer)
                for (int i = 0; i < bills.Count; i++)
                {
                    for (int j = 0; j < payments.Count; j++)
                    {
                        if (bills[i].BillName == payments[j].PayName && bills[i].BillSum * payments[j].PaySum != 0)
                        {
                            if (bills[i].BillSum >= payments[j].PaySum)
                            {
                                Console.WriteLine("Клиент {0} счет {4} от {1} платеж от {2} на сумму {3}", bills[i].BillName, bills[i].BillDate.ToShortDateString(), payments[j].PayDate.ToShortDateString(), payments[j].PaySum, bills[i].BillSum);
                                res.Add(payments[j].PayName.ToString() + "!");
                                res.Add(payments[j].PayDate.ToString("dd.MM.yyyy") + "!");
                                res.Add(payments[j].PayNum.ToString() + "!");
                                res.Add(bills[i].BillDate.ToString("dd.MM.yyyy") + "!");
                                res.Add(bills[i].BillNum.ToString() + "!");
                                res.Add(payments[j].PaySum.ToString(CultureInfo.InvariantCulture) + "!");
                                bills[i].BillSum -= payments[j].PaySum;
                                payments[j].PaySum = 0;
                                Console.WriteLine("Осталось оплатить: {0}", bills[i].BillSum);
                            }
                            if (bills[i].BillSum < payments[j].PaySum)
                            {
                                Console.WriteLine("Клиент {0} счет {4} от {1} платеж от {2} на сумму {3}", bills[i].BillName, bills[i].BillDate.ToShortDateString(), payments[j].PayDate.ToShortDateString(), payments[j].PaySum, bills[i].BillSum);
                                res.Add(payments[j].PayName.ToString() + "!");
                                res.Add(payments[j].PayDate.ToString("dd.MM.yyyy") + "!");
                                res.Add(payments[j].PayNum.ToString() + "!");
                                res.Add(bills[i].BillDate.ToString("dd.MM.yyyy") + "!");
                                res.Add(bills[i].BillNum.ToString() + "!");
                                res.Add(payments[j].PaySum.ToString(CultureInfo.InvariantCulture) + "!");
                                payments[j].PaySum -= bills[i].BillSum;
                                bills[i].BillSum = 0;
                                Console.WriteLine("Переплата: {0}", payments[j].PaySum);
                            }
                        }
                    }
                }
            else
            {
                for (int i = 0; i < bills.Count; i++)
                {
                    for (int j = 0; j < payments.Count; j++)
                    {
                        if (bills[i].BillName == payments[j].PayName && bills[i].BillSum * payments[j].PaySum != 0)
                        {
                            if (bills[i].BillSum >= payments[j].PaySum)
                            {
                                Console.WriteLine("Клиент {0} счет {4} от {1} платеж от {2} на сумму {3}", bills[i].BillName, bills[i].BillDate.ToShortDateString(), payments[j].PayDate.ToShortDateString(), payments[j].PaySum, bills[i].BillSum);
                                res.Add(payments[j].PayName.ToString());
                                res.Add(payments[j].PayDate.ToString("dd.MM.yyyy"));
                                res.Add(payments[j].PayNum.ToString());
                                res.Add(bills[i].BillDate.ToString("dd.MM.yyyy"));
                                res.Add(bills[i].BillNum.ToString());
                                res.Add(payments[j].PaySum.ToString(CultureInfo.InvariantCulture));
                                bills[i].BillSum -= payments[j].PaySum;
                                payments[j].PaySum = 0;
                                Console.WriteLine("Осталось оплатить: {0}", bills[i].BillSum);
                            }
                            if (bills[i].BillSum < payments[j].PaySum)
                            {
                                Console.WriteLine("Клиент {0} счет {4} от {1} платеж от {2} на сумму {3}", bills[i].BillName, bills[i].BillDate.ToShortDateString(), payments[j].PayDate.ToShortDateString(), payments[j].PaySum, bills[i].BillSum);
                                res.Add(payments[j].PayName.ToString());
                                res.Add(payments[j].PayDate.ToString("dd.MM.yyyy"));
                                res.Add(payments[j].PayNum.ToString());
                                res.Add(bills[i].BillDate.ToString("dd.MM.yyyy"));
                                res.Add(bills[i].BillNum.ToString());
                                res.Add(payments[j].PaySum.ToString(CultureInfo.InvariantCulture));
                                payments[j].PaySum -= bills[i].BillSum;
                                bills[i].BillSum = 0;
                                Console.WriteLine("Переплата: {0}", payments[j].PaySum);
                            }
                        }
                    }
                }
            }
            return res;
        }
    }
}

