using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrial_Mangement_System
{
 public class Shoper_Data_Holder_Class
    {
        public string name, father_name, phone_number, cnic, designation, date, details, address;

        public Shoper_Data_Holder_Class(string sh_n, string sh_fN, string sh_des, string sh_phoneNumber, string sh_cnic, string sh_det, string sh_add, string sh_Date)
        {
            name = sh_n;
            father_name = sh_fN;
            phone_number = sh_phoneNumber;
            cnic = sh_cnic;
            designation = sh_des;
            date = sh_Date;
            details = sh_det;
            address = sh_add;
        }
    }
}
