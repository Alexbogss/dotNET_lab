using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Bill
    {
        public string BillName;
        public DateTime BillDate;
        public string BillNum;
        public double BillSum;

        public override string ToString()
        {
            return string.Format("{0, 16} {1} {2,10} {3,10}", BillName, BillDate.ToShortDateString(), BillNum, BillSum);
        }
    }
}
