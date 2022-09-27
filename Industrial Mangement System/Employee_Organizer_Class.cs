using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Industrial_Mangement_System
{
    public class Employee_Organizer_Class
    {
        // declaration of all the necessary variables
        public string name, father_name, phone_number, Emp_Id, designation, joining_date, monthly_salary, daily_salary, details, address;
        
        public Employee_Organizer_Class(string emp_n, string emp_fN, string emp_des, string emp_phoneNumber, string emp_id, string emp_det, string emp_add, string emp_MSalary, string emp_DSalary, string emp_JDate)
        {
            name = emp_n;
            father_name = emp_fN;
            phone_number = emp_phoneNumber;
            Emp_Id = emp_id;
            designation = emp_des;
            joining_date = emp_JDate;
            monthly_salary = emp_MSalary;
            daily_salary = emp_DSalary;
            details = emp_det;
            address = emp_add;
        }
     
    }
}
