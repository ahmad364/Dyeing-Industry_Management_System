using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Mangement_System
{
 public class order_organizer_Class
    {
        public string c_name, c_cnic, c_phoNumber, c_address, order_status, order_date, order_id;
        public float rece_rupees, total_item_pay, netPay;
        public int order_no;
        public order_organizer_Class(string c_n, string cnic, string c_add, string c_phNum, int ord_no, string o_date, string o_status,string o_id, float rec_rup, float total_orderPay, float NetPay)
        {
            c_name = c_n;
            c_cnic = cnic;
            c_phoNumber = c_phNum;
            c_address = c_add;
            order_id = o_id;
            order_status = o_status;
            order_date = o_date;
            order_no = ord_no;
            rece_rupees = rec_rup;
            total_item_pay = total_orderPay;
            netPay = NetPay;
        }
    }
}
