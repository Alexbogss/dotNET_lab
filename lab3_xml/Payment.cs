using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Payment
    {
        public string PayName;
        public DateTime PayDate;
        public string PayNum;
        public double PaySum;

        public override string ToString()
        {
            return string.Format("{0, 16} {1} {2,10} {3,10}", PayName, PayDate.ToShortDateString(),PayNum,PaySum);
        }
    }
}
