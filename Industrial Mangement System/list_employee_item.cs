using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Industrial_Mangement_System
{
    public partial class list_employee_item : UserControl
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        public string check_option;

        employee_list_form emp_list_form;

        Employee_Organizer_Class employee_Organizer;
        public list_employee_item(Employee_Organizer_Class obj1 ,string option, employee_list_form obj2)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            employee_Organizer = obj1;
            check_option = option;
            emp_list_form = obj2;

            employee_name.Text = employee_Organizer.name;
            employee_designation.Text = employee_Organizer.designation;
            employee_idCard.Text = employee_Organizer.Emp_Id;

        }

        private void setSize(object sender, EventArgs e)
        {
            Connect.Open();

            try
            {

                //   assigning emp image to picture box
                string sql = "Select Employee_Image from Employee where Employee_Id='" + employee_Organizer.Emp_Id + "'";
                comands = new SqlCommand(sql, Connect);
                SqlDataReader reader = comands.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    byte[] img = (byte[])(reader[0]);
                    if (img == null)
                        employee_pic.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        employee_pic.Image = Image.FromStream(ms);
                    }
                }
                Connect.Close();
            }catch(Exception)
            {
                Connect.Close();
            }

            int x = SystemInformation.WorkingArea.Width;
                int y = 120;
                this.Size = new Size(x-23, y);

        }


        private void list_employee_item_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
        }

        private void list_employee_item_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void list_employee_item_Click(object sender, EventArgs e)
        {
            if (check_option == "employee details")
            {
              
                Employee_form obj = new Employee_form(employee_Organizer,emp_list_form);
                obj.ShowDialog();
            }
            else   
            {
                Salary_Generator_Form obj = new Salary_Generator_Form(employee_Organizer);
                obj.ShowDialog();
            }
        }

    }
}
