using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;

namespace Industrial_Mangement_System
{
    public partial class employee_list_form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataReader data_reader;

        public employee_list_form Employee_List_Form;

        public  Delivered_Order_List_Form delivered_Order_List_form;

        public sumary_Form Sumary_Form;

        MenuForm menuForm;

        public string option;
        DateTime start_date,  end_date;

     
        public employee_list_form(string opt)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            // setting label in the tittle bar
            option = opt;
            if (option == "orders" || option=="call from summary"|| option=="call for order's pending rupees")
                label1.Text = ("Orders List");
        }
        public void set_object_of_menu_form(MenuForm obj)
        {
            menuForm = obj;
        }
        public void set_object_of_summary_form(sumary_Form obj)
        {
            Sumary_Form = obj;
        }
        public void populate_order_items_according_to_the_call()
        {
            if (option == "orders")
                populate_order_items();
            else if (option == "call from summary")
                populate_orders_according_to_the_call_from_summary();
            else
                populate_orders_which_has_pending_rupees_but_delivered();
        }
        public void set_dates(DateTime st_date, DateTime en_date)
        {
            start_date = st_date;
            end_date = en_date;
        }
        private void employee_list_form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;
            if (option == "employee details")
            {
                populateItems();
            }
            else if (option == "salary")
            {
                populateItems();
            }
            else if(option=="orders")
            {
                populate_order_items();
            }
            else if(option == "call from summary")
            {
                populate_orders_according_to_the_call_from_summary();
            }
            else
            {
                populate_orders_which_has_pending_rupees_but_delivered();
            }

            menuForm.tableLayoutPanel2.Visible = false;
        }

        public void set_obj_of_form(employee_list_form obj)
        {
            Employee_List_Form = obj;
        }
        public void populateItems()
        {
            show_employees_LaoutPannel.Controls.Clear();
            Employee_Organizer_Class employee_Organizer;
            List<list_employee_item> emp = new List<list_employee_item>();
            int emp_count = 0;

            // make the array of controls panel

            Connect.Open();

            comands.CommandText = "select *from Employee order by Employee_Name";
            data_reader = comands.ExecuteReader();

            if(data_reader.HasRows)
            {
                while(data_reader.Read())
                {
                    string JD = data_reader["Employee_JoiningDate"].ToString();
                    employee_Organizer = new Employee_Organizer_Class(data_reader["Employee_Name"].ToString(), data_reader["Employee_FatherName"].ToString(), data_reader["Employee_Designation"].ToString(), data_reader["Employee_PhoneNumber"].ToString(), data_reader["Employee_Id"].ToString(), data_reader["Employee_Details"].ToString(), data_reader["Employee_Adress"].ToString(), data_reader["Employee_MonthlySalary"].ToString(), data_reader["Employee_DailySalary"].ToString(), JD);
                    emp.Add(new list_employee_item(employee_Organizer , option,Employee_List_Form));
                    show_employees_LaoutPannel.Controls.Add(emp[emp_count]);
                    emp_count++;
                }
            }

            Connect.Close();

        }
        public void populate_order_items()
        {
            
            show_employees_LaoutPannel.Controls.Clear();

            List<order_items_UserControl> order_Items = new List<order_items_UserControl>();
            order_organizer_Class order_Organizer_Object;
            int count = 0;
                Connect.Open();
                comands.CommandText = "select *from OrderTable where Delivered='No' order by Order_date desc";
                data_reader = comands.ExecuteReader();
                if(data_reader.HasRows)
                {
                    while(data_reader.Read())
                    {
                    
                        string order_date = data_reader["Order_Date"].ToString();
                        order_Organizer_Object = new order_organizer_Class(data_reader["Client_Name"].ToString(), data_reader["Client_CNIC"].ToString(), data_reader["Client_Address"].ToString(), data_reader["Client_Phone_Number"].ToString(), Convert.ToInt32(data_reader["Order_Number"]), order_date, data_reader["Order_Status"].ToString(), data_reader["Order_Id"].ToString(), float.Parse(data_reader["Order_Recieved_Rupees"].ToString()), float.Parse(data_reader["Order_Total_Rupees"].ToString()), float.Parse(data_reader["Order_NetPay"].ToString()));
                        order_Items.Add(new order_items_UserControl(order_Organizer_Object, Employee_List_Form, delivered_Order_List_form, "Not"));
                        show_employees_LaoutPannel.Controls.Add(order_Items[count]);
                        count++;
                    
                    }
                }

            Connect.Close();
        }
        public void populate_orders_according_to_the_call_from_summary()
        {
            show_employees_LaoutPannel.Controls.Clear();
            List<order_items_UserControl> order_Items = new List<order_items_UserControl>();
            order_organizer_Class order_Organizer_Object;
            int count = 0;
            Connect.Open();
            comands.CommandText = "select *from OrderTable where Delivered='No' and Delivered_All_Order_Items='No' and Order_Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "' order by Order_date desc";
            data_reader = comands.ExecuteReader();
            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {

                    string order_date = data_reader["Order_Date"].ToString();
                    order_Organizer_Object = new order_organizer_Class(data_reader["Client_Name"].ToString(), data_reader["Client_CNIC"].ToString(), data_reader["Client_Address"].ToString(), data_reader["Client_Phone_Number"].ToString(), Convert.ToInt32(data_reader["Order_Number"]), order_date, data_reader["Order_Status"].ToString(), data_reader["Order_Id"].ToString(), float.Parse(data_reader["Order_Recieved_Rupees"].ToString()), float.Parse(data_reader["Order_Total_Rupees"].ToString()), float.Parse(data_reader["Order_NetPay"].ToString()));
                    order_Items.Add(new order_items_UserControl(order_Organizer_Object, Employee_List_Form, delivered_Order_List_form, "Not"));
                    show_employees_LaoutPannel.Controls.Add(order_Items[count]);
                    count++;

                }
            }

            Connect.Close();
        }
        public void populate_orders_which_has_pending_rupees_but_delivered()
        {
            show_employees_LaoutPannel.Controls.Clear();
            List<order_items_UserControl> order_Items = new List<order_items_UserControl>();
            order_organizer_Class order_Organizer_Object;
            int count = 0;
            Connect.Open();
            comands.CommandText = "select *from OrderTable where Delivered='No' and Delivered_All_Order_Items ='Yes' order by Order_date desc";
            data_reader = comands.ExecuteReader();
            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {

                    string order_date = data_reader["Order_Date"].ToString();
                    order_Organizer_Object = new order_organizer_Class(data_reader["Client_Name"].ToString(), data_reader["Client_CNIC"].ToString(), data_reader["Client_Address"].ToString(), data_reader["Client_Phone_Number"].ToString(), Convert.ToInt32(data_reader["Order_Number"]), order_date, data_reader["Order_Status"].ToString(), data_reader["Order_Id"].ToString(), float.Parse(data_reader["Order_Recieved_Rupees"].ToString()), float.Parse(data_reader["Order_Total_Rupees"].ToString()), float.Parse(data_reader["Order_NetPay"].ToString()));
                    order_Items.Add(new order_items_UserControl(order_Organizer_Object, Employee_List_Form, delivered_Order_List_form, "Not"));
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

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (option == "call from summary" || option == "call for order's pending rupees")
                Sumary_Form.populpate_data();
            this.Close();
        }

    }
}
