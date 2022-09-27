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
    public partial class Shopers_UserControl : UserControl
    {
        SqlConnection Connect;
        SqlCommand comands = new SqlCommand();

        Shoper_List_Form Shoper_List_form;
        Shoper_Data_Holder_Class Shoper_Data_Holder;
        public Shopers_UserControl(Shoper_List_Form obj1,Shoper_Data_Holder_Class obj2)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);
            comands.Connection = Connect;

            Shoper_List_form = obj1;
            Shoper_Data_Holder = obj2;

            // assigning data
            employee_name.Text = Shoper_Data_Holder.name;
            employee_designation.Text = Shoper_Data_Holder.designation;
            employee_idCard.Text = Shoper_Data_Holder.cnic;
        }

        private void Shopers_UserControl_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
        }

        private void Shopers_UserControl_MouseLeave(object sender, EventArgs e)
        {
           
            this.BackColor = Color.White;
        }

        private void Shopers_UserControl_Load(object sender, EventArgs e)
        {
            Connect.Open();

            try
            {

                //   assigning emp image to picture box
                string sql = "Select Image from Shoper where CNIC='" + Shoper_Data_Holder.cnic + "'";
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
            }
            catch (Exception )
            {
                Connect.Close();
            }

            int x = SystemInformation.WorkingArea.Width;
            int y = 120;
            this.Size = new Size(x - 24, y);
        }

        private void Shopers_UserControl_Click(object sender, EventArgs e)
        {
            Shoper_Details_Form obj = new Shoper_Details_Form(Shoper_Data_Holder,Shoper_List_form);
            obj.set_object_of_this_form(obj);
            obj.ShowDialog();
        }
    }
}
