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
    public partial class Delivered_Order_List_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataReader data_reader;

        MenuForm menuForm;

        employee_list_form Employee_List_Form;
        Delivered_Order_List_Form Delivered_Order_List_form;
        sumary_Form Sumary_Form;

        DateTime start_date, end_date;
        bool cal_from_summary_form = false;
        public Delivered_Order_List_Form()
        {

            InitializeComponent();


            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;

        }
        public void set_object_of_menu_form(MenuForm obj)
        {
            menuForm = obj;
        }
        public void populate_items_according_to_the_call()
        {
            if (cal_from_summary_form == false)
                populate_order_items();
            else
                populate_orders_according_to_the_summary_form();
        }
        public void set_call(bool call)
        {
            cal_from_summary_form = call;
        }
        public void set_object_of_sumary_from(sumary_Form obj)
        {
            Sumary_Form = obj;
        }
        public void set_dates(DateTime s_date,DateTime en_date)
        {
            start_date = s_date;
            end_date = en_date;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if(cal_from_summary_form==true)
            Sumary_Form.populpate_data();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
        public void setForm_Object(Delivered_Order_List_Form obj)
        {
            Delivered_Order_List_form = obj;
        }
        public void setForm_of_employee_list_form(employee_list_form obj)
        {
            Employee_List_Form = obj;
        }
        public void populate_order_items()
        {
            show_employees_LaoutPannel.Controls.Clear();

            List<order_items_UserControl> order_Items = new List<order_items_UserControl>();
            order_organizer_Class order_Organizer_Object;
            int count = 0;
            Connect.Open();
            comands.CommandText = "select *from OrderTable where Delivered='Yes' order by Order_date desc";
            data_reader = comands.ExecuteReader();
            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    
                        string order_date = data_reader["Order_Date"].ToString();
                        order_Organizer_Object = new order_organizer_Class(data_reader["Client_Name"].ToString(), data_reader["Client_CNIC"].ToString(), data_reader["Client_Address"].ToString(), data_reader["Client_Phone_Number"].ToString(), Convert.ToInt32(data_reader["Order_Number"]), order_date, data_reader["Order_Status"].ToString(), data_reader["Order_Id"].ToString(), float.Parse(data_reader["Order_Recieved_Rupees"].ToString()), float.Parse(data_reader["Order_Total_Rupees"].ToString()), float.Parse(data_reader["Order_NetPay"].ToString()));
                        order_Items.Add(new order_items_UserControl(order_Organizer_Object, Employee_List_Form,Delivered_Order_List_form,"Delivered"));
                        show_employees_LaoutPannel.Controls.Add(order_Items[count]);
                        count++;
                    
                }
            }

            Connect.Close();
        }

        public void populate_orders_according_to_the_summary_form()
        {
            show_employees_LaoutPannel.Controls.Clear();

            List<order_items_UserControl> order_Items = new List<order_items_UserControl>();
            order_organizer_Class order_Organizer_Object;
            int count = 0;
            Connect.Open();
            comands.CommandText = "select *from OrderTable where Delivered='Yes' and Order_Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "' order by Order_date  desc";
            data_reader = comands.ExecuteReader();
            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {

                    string order_date = data_reader["Order_Date"].ToString();
                    order_Organizer_Object = new order_organizer_Class(data_reader["Client_Name"].ToString(), data_reader["Client_CNIC"].ToString(), data_reader["Client_Address"].ToString(), data_reader["Client_Phone_Number"].ToString(), Convert.ToInt32(data_reader["Order_Number"]), order_date, data_reader["Order_Status"].ToString(), data_reader["Order_Id"].ToString(), float.Parse(data_reader["Order_Recieved_Rupees"].ToString()), float.Parse(data_reader["Order_Total_Rupees"].ToString()), float.Parse(data_reader["Order_NetPay"].ToString()));
                    order_Items.Add(new order_items_UserControl(order_Organizer_Object, Employee_List_Form, Delivered_Order_List_form, "Delivered"));
                    show_employees_LaoutPannel.Controls.Add(order_Items[count]);
                    count++;

                }
            }

            Connect.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        private void Delivered_Order_List_Form_Load(object sender, EventArgs e)
        {
            menuForm.tableLayoutPanel2.Visible = false;
        }
    }
}
