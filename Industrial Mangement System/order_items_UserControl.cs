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
    public partial class order_items_UserControl : UserControl
    {

        SqlConnection Connect;
        SqlCommand comands = new SqlCommand();

        employee_list_form employee_List_Form;
        order_organizer_Class order_Organizer;
        Delivered_Order_List_Form Delivered_Order_List_form;

        string option;
        
        public order_items_UserControl(order_organizer_Class obj1,employee_list_form obj2,Delivered_Order_List_Form obj3,string opt)
        {
            
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            order_Organizer = obj1;
            employee_List_Form = obj2;
            Delivered_Order_List_form = obj3;

            option = opt;
            client_idCard_label.Text = obj1.c_cnic;
            order_status_label.Text = obj1.order_status;
            client_name_label.Text = obj1.c_name;
            today_date_label.Text = Convert.ToDateTime(obj1.order_date).ToString("dd   MMMM   yyyy");
        }
        
        private void order_items_UserControl_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            Connect.Open();

            try
            {

                //   assigning emp image to picture box
                string sql = "Select Client_Image from OrderTable where Order_Id='" + order_Organizer.order_id + "'";
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

        private void order_items_UserControl_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
        }

        private void order_items_UserControl_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void order_items_UserControl_Click(object sender, EventArgs e)
        {
            Order_Form obj = new Order_Form(order_Organizer,employee_List_Form);
            obj.set_object_of_Delivered_List_Form(Delivered_Order_List_form);
            obj.set_order_form_page_object(obj);
            if (option== "Delivered")
            {
                obj.set_Deliver_or_not(true);
                obj.panel3.Visible = true;
            }
            else
                obj.set_Deliver_or_not(false);
            obj.ShowDialog();
        }
    }
}
