using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace lab4
{
    public interface DataInterface
    {
        ArrayList ReadPay();
        ArrayList ReadBill();
        void WriteXML(ArrayList Result);
    }
}
