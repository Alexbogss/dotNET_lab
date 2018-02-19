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
using System.Drawing;

namespace lab3
{
    public partial class Form1 : Form
    {
        bool start = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            
            if (LocalRB.Checked && !start)
            {
                start = true;
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
                
                Console.WriteLine("\n _______________________ \n");

                var res = PaymentProcessor.ProcessPayments(bills, payments, false);
                Repository.WriteXML(res);

                Console.WriteLine("\n _______________________ \n");

                Console.WriteLine("Cчета: ");
                for (int i = 0; i < bills.Count; i++)
                    Console.WriteLine(bills[i]);
                Console.WriteLine("Платежи: ");
                for (int i = 0; i < payments.Count; i++)
                    Console.WriteLine(payments[i]);

                dataGridView1.Columns.Add("client", "Client");
                dataGridView1.Columns.Add("PDate", "PayDate");
                dataGridView1.Columns.Add("PNum", "PayNum");
                dataGridView1.Columns.Add("BDate", "BillDate");
                dataGridView1.Columns.Add("BNum", "BillNum");
                dataGridView1.Columns.Add("PaySum", "Sum");

                for (int i = 0; i < res.Count; i+=6)
                {
                    int rc = dataGridView1.Rows.Add();
                    Console.WriteLine(rc);
                    dataGridView1.Rows[rc].Cells[0].Value = res[i];
                    dataGridView1.Rows[rc].Cells[1].Value = res[i + 1].ToString();
                    dataGridView1.Rows[rc].Cells[2].Value = res[i + 2];
                    dataGridView1.Rows[rc].Cells[3].Value = res[i + 3];
                    dataGridView1.Rows[rc].Cells[4].Value = res[i + 4];
                    dataGridView1.Rows[rc].Cells[5].Value = res[i + 5];
                }
            }

            if(TCPRB.Checked && !start)
            {
                string ip = System.Configuration.ConfigurationManager.ConnectionStrings["TestIP"].ConnectionString.ToString();
                int port = Int32.Parse(ConfigurationManager.AppSettings["port"]);
                if (ButHash.Checked)
                {
                    progressBar1.Visible = true;
                    var bills = new List<Bill>();
                    var payments = new List<Payment>();
                    TcpClient client = new TcpClient(ip, port);
                    NetworkStream stream = client.GetStream();
                    Byte[] test = System.Text.Encoding.UTF32.GetBytes("0");
                    stream.Write(test, 0, test.Length);

                    Byte[] data = System.Text.Encoding.UTF32.GetBytes("1");
                    stream.Write(data, 0, data.Length);
                    bills = Client.RecieveBill(stream);

                    /*var hp = new System.Security.Cryptography.SHA1Managed();
                    data = hp.ComputeHash(data);
                    test = hp.ComputeHash(test);
                    Client.hashEqual(data, test);*/

                    if (Client.checkhash == 1)
                        progressBar1.BackColor = System.Drawing.Color.LimeGreen;
                    else
                        progressBar1.BackColor = System.Drawing.Color.Red;

                    data = System.Text.Encoding.UTF32.GetBytes("2");
                    stream.Write(data, 0, data.Length);
                    payments = Client.RecievePayment(stream);
                    progressBar2.Visible = true;
                    if (Client.checkhash == 1)
                        progressBar2.BackColor = System.Drawing.Color.LimeGreen;
                    else
                        progressBar2.BackColor = System.Drawing.Color.Red;

                    var res = PaymentProcessor.ProcessPayments(bills, payments, false);
                    data = System.Text.Encoding.UTF32.GetBytes("3");
                    stream.Write(data, 0, data.Length);
                    Client.SendResult(res, stream);

                    dataGridView1.Columns.Add("client", "Client");
                    dataGridView1.Columns.Add("PDate", "PayDate");
                    dataGridView1.Columns.Add("PNum", "PayNum");
                    dataGridView1.Columns.Add("BDate", "BillDate");
                    dataGridView1.Columns.Add("BNum", "BillNum");
                    dataGridView1.Columns.Add("PaySum", "Sum");

                    for (int i = 0; i < res.Count; i += 6)
                    {
                        int rc = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rc].Cells[0].Value = res[i];
                        dataGridView1.Rows[rc].Cells[1].Value = res[i + 1].ToString();
                        dataGridView1.Rows[rc].Cells[2].Value = res[i + 2];
                        dataGridView1.Rows[rc].Cells[3].Value = res[i + 3];
                        dataGridView1.Rows[rc].Cells[4].Value = res[i + 4];
                        dataGridView1.Rows[rc].Cells[5].Value = res[i + 5];
                    }
                }
                else
                {
                    Client.checkhash = -1;
                    var bills = new List<Bill>();
                    var payments = new List<Payment>();
                    TcpClient client = new TcpClient(ip, port);
                    NetworkStream stream = client.GetStream();
                    Byte[] test = System.Text.Encoding.UTF32.GetBytes("0");
                    stream.Write(test, 0, test.Length);

                    Byte[] data = System.Text.Encoding.UTF32.GetBytes("1");
                    stream.Write(data, 0, data.Length);
                    bills = Client.RecieveBill(stream);

                    data = System.Text.Encoding.UTF32.GetBytes("2");
                    stream.Write(data, 0, data.Length);
                    payments = Client.RecievePayment(stream);

                    var res = PaymentProcessor.ProcessPayments(bills, payments, false);
                    data = System.Text.Encoding.UTF32.GetBytes("3");
                    stream.Write(data, 0, data.Length);
                    Client.SendResult(res, stream);

                    dataGridView1.Columns.Add("client", "Client");
                    dataGridView1.Columns.Add("PDate", "PayDate");
                    dataGridView1.Columns.Add("PNum", "PayNum");
                    dataGridView1.Columns.Add("BDate", "BillDate");
                    dataGridView1.Columns.Add("BNum", "BillNum");
                    dataGridView1.Columns.Add("PaySum", "Sum");

                    for (int i = 0; i < res.Count; i += 6)
                    {
                        int rc = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rc].Cells[0].Value = res[i];
                        dataGridView1.Rows[rc].Cells[1].Value = res[i + 1].ToString();
                        dataGridView1.Rows[rc].Cells[2].Value = res[i + 2];
                        dataGridView1.Rows[rc].Cells[3].Value = res[i + 3];
                        dataGridView1.Rows[rc].Cells[4].Value = res[i + 4];
                        dataGridView1.Rows[rc].Cells[5].Value = res[i + 5];
                    }
                }
            }

            if(SQLRB.Checked && !start)
            {
                using (var cnn = new SqlConnection(@"Data Source = localhost\Alexbogs; Database = Lab5Db; Integrated Security=SSPI; User ID = ALEXBOGS\Alexbogs"))
                {
                    cnn.Open();
                    var bills = SQLClient.ReadBill(cnn);
                    var payments = SQLClient.ReadPay(cnn);
                    var res = PaymentProcessor.ProcessPayments(bills, payments, false);
                    SQLClient.SQLWrite(res, cnn);

                    dataGridView1.Columns.Add("client", "Client");
                    dataGridView1.Columns.Add("PDate", "PayDate");
                    dataGridView1.Columns.Add("PNum", "PayNum");
                    dataGridView1.Columns.Add("BDate", "BillDate");
                    dataGridView1.Columns.Add("BNum", "BillNum");
                    dataGridView1.Columns.Add("PaySum", "Sum");

                    for (int i = 0; i < res.Count; i += 6)
                    {
                        int rc = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rc].Cells[0].Value = res[i];
                        dataGridView1.Rows[rc].Cells[1].Value = res[i + 1].ToString();
                        dataGridView1.Rows[rc].Cells[2].Value = res[i + 2];
                        dataGridView1.Rows[rc].Cells[3].Value = res[i + 3];
                        dataGridView1.Rows[rc].Cells[4].Value = res[i + 4];
                        dataGridView1.Rows[rc].Cells[5].Value = res[i + 5];
                    }
                }
            }
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {


        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (TCPRB.Checked)
                ButHash.Enabled = true;
            else
            {
                ButHash.Enabled = false;
                ButHash.Checked = false;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }


    }
}
