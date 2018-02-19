using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace lab3
{
    class SQLClient
    {
        public static List<Payment> ReadPay(SqlConnection cnn)
        {
            var payments = new List<Payment>();
            var paytable = new DataTable();
            var adapter = new SqlDataAdapter("SELECT * FROM dbo.Payments", cnn);
            adapter.Fill(paytable);
            DataRowCollection payrow = paytable.Rows;
            for (int i = 0; i < payrow.Count; i++)
            {
                string tdate = null;
                string temp = payrow[i][2].ToString();
                for (int k = 0; k < 10; k++)
                {
                    tdate += temp[k];
                }
                payments.Add(new Payment
                    {
                        PayName = payrow[i][1].ToString(),
                        PayDate = DateTime.ParseExact(tdate, "dd.MM.yyyy", null),
                        PayNum = payrow[i][3].ToString(),
                        PaySum = Double.Parse(payrow[i][4].ToString())
                    });
            }
            Console.WriteLine("Платежи: ");
            for (int k = 0; k < payments.Count; k++)
                Console.WriteLine(payments[k]);
            return payments;
        }
        public static List<Bill> ReadBill(SqlConnection  cnn)
        {
            var bills = new List<Bill>();
            var billtable = new DataTable();
            var adapter = new SqlDataAdapter("SELECT * FROM dbo.Bills", cnn);
            adapter.Fill(billtable);
            DataRowCollection billrow = billtable.Rows;
            for (int i = 0; i < billrow.Count; i++ )
            {
                string tdate = null;
                string temp = billrow[i][2].ToString();
                for (int k = 0; k < 10; k++)
                {
                    tdate += temp[k];
                }
                bills.Add(new Bill
                    {
                        BillName = billrow[i][1].ToString(),
                        BillDate = DateTime.ParseExact(tdate, "dd.MM.yyyy", null),
                        BillNum = billrow[i][3].ToString(),
                        BillSum = Double.Parse(billrow[i][4].ToString())
                    });
            }
            Console.WriteLine("Cчета: ");
            for (int k = 0; k < bills.Count; k++)
                Console.WriteLine(bills[k]);
            return bills;
        }
        public static void SQLWrite(ArrayList Result, SqlConnection cnn)
        {
            for (int i = 0; i < Result.Count; i+=6)
            {
                var insert = new SqlCommand(@"INSERT INTO dbo.Result (Client, PayDate, PayNumber, BillDate, BillNumber, Sum)
                                        VALUES (@Name, @PDate, @PNum, @BDate, @BNum, @sum)", cnn);
                insert.Parameters.AddWithValue("@Name", Result[i].ToString());
                insert.Parameters.AddWithValue("@PDate", Result[i + 1].ToString());
                insert.Parameters.AddWithValue("@PNum", Result[i + 2].ToString());
                insert.Parameters.AddWithValue("@BDate", Result[i + 3].ToString());
                insert.Parameters.AddWithValue("@BNum", Result[i + 4].ToString());
                insert.Parameters.AddWithValue("@sum", Result[i + 5]);
                insert.ExecuteNonQuery();
                
            }
        }
    }
}
