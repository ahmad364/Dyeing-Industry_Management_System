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
    public partial class sumary_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        sumary_Form Sumary_Form;

        MenuForm menuForm;

        DateTime start_date, end_date;

        // required variables
        double total_daily_expence = 0, total_industerial_expence = 0, total_employees_salaries = 0;
        public sumary_Form(string name,string designation,string ph_number,DateTime st_date,DateTime en_date,MenuForm obj)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            start_date = st_date;
            end_date = en_date;
            menuForm = obj;

            // initalize data basic data of summary 
            manger_name_label.Text = name;
            manger_designation_label.Text = designation;
            manager_ph_number.Text = ph_number;
            start_date_Label.Text = st_date.ToString("dd  MMMM  yyyy");
            end_date_label.Text = en_date.ToString("dd  MMMM  yyyy");
            today_date_label.Text = DateTime.Today.ToString("dd    MMMM    yyyy");
        }
        public void set_object_of_this_form(sumary_Form obj)
        {
            Sumary_Form = obj;
        }
        public void set_Delivered_orders_remaining_rupees()
        {

        }
        public void populpate_data()
        {
            try
            {
                Connect.Open();

                // calculate manager recieved rupees from owner

                comands.CommandText = "select sum(Rupees) from ManagerIncome where Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                if ((comands.ExecuteScalar().ToString()) != "")
                    manger_rece_rupees.Text = comands.ExecuteScalar().ToString();
                else
                    manger_rece_rupees.Text = "0.00";

                // calculate manger daily expence

                comands.CommandText = "select sum(Expence_Rupees) from ManagerDailyExpence where Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                if ((comands.ExecuteScalar().ToString()) != "")
                    manger_daily_ecpence.Text = comands.ExecuteScalar().ToString();
                else
                    manger_daily_ecpence.Text = "0.00";

                // calculate industerial expence

                comands.CommandText = "select sum(Product_Rupees) from Purchase where Purchasing_Date2 between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                if ((comands.ExecuteScalar().ToString()) != "")
                    induterial_expence.Text = comands.ExecuteScalar().ToString();
                else
                    induterial_expence.Text = "0.00";

                // counting the pending orders with in the dates
                // this is for escaping from null exception
                comands.CommandText = "select case when exists(select top 1* from OrderTable where Delivered='No' and Delivered_All_Order_Items='No' and Order_Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "')then cast (1 as bit) else cast(0 as bit) end";
                if (comands.ExecuteScalar().Equals(true))
                {
                    // just for practice
                    //  comands.CommandText = "select ItemTable.Order_Id from OrderTable inner join ItemTable on ItemTable.Order_Id=OrderTable.Order_Id where ItemTable.Delivered='No' and OrderTable.Order_Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "' ";
                    comands.CommandText = "select count(Order_Id) from OrderTable where Delivered='No' and Delivered_All_Order_Items='No'and Order_Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                    pending_orders.Text = comands.ExecuteScalar().ToString();
                }
                else
                    pending_orders.Text = "0.00";

                // calaculating total delivered orders according to the dates

                comands.CommandText = "select case when exists(select top 1* from OrderTable where Delivered='Yes' and Order_Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "')then cast (1 as bit) else cast(0 as bit) end";
                if (comands.ExecuteScalar().Equals(true))
                {
                    comands.CommandText = "select count(Order_Id) from OrderTable where Delivered='Yes' and Order_Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                    deliverd_orders.Text = comands.ExecuteScalar().ToString();
                }
                else
                    deliverd_orders.Text = "0.00";

                // calculating pending rupees of orders

                comands.CommandText = "select case when exists(select top 1* from OrderTable where Delivered='No' and Delivered_All_Order_Items='Yes')then cast (1 as bit) else cast(0 as bit) end";
                if (comands.ExecuteScalar().Equals(true))
                {
                    comands.CommandText = "select sum(Order_NetPay) from OrderTable where Delivered='No' and Delivered_All_Order_Items='Yes'";
                    pending_order_rupees.Text = comands.ExecuteScalar().ToString();
                }
                else
                    pending_order_rupees.Text = "0.00";
                
                // counting number of employees

                comands.CommandText = "select count(Employee_Id) from Employee";
                if ((comands.ExecuteScalar().ToString()) != "")
                    total_employees.Text = comands.ExecuteScalar().ToString();

                // calculating the employe's sealaries according to the dates

                comands.CommandText = "select sum(Net_Pay) from Employee_Attendance where Attendance_Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                if ((comands.ExecuteScalar().ToString()) != "")
                    total_employee_salaries.Text = (Math.Ceiling(float.Parse(comands.ExecuteScalar().ToString()))).ToString();

                // calculating the employee's advance rupees

                comands.CommandText = "select sum(Employee_Advance_Rupees)-sum(Employee_Paid_Advance_Rupees) from Employee";
                if ((comands.ExecuteScalar().ToString()) != "")
                    total_employee_advance_rupees.Text = comands.ExecuteScalar().ToString();

                // calculating the toal of of borrowed rupees of shopers

                comands.CommandText = "select sum(Total_Borrow_Rupees)-sum(Return_Borrow_Rupees) from Shoper";
                if ((comands.ExecuteScalar().ToString()) != "")
                    total_borrow_rupees.Text = comands.ExecuteScalar().ToString();

                // calculating total stored thaan

                comands.CommandText = "select sum(Total_Thans)-sum(Delivered_Thans) from ItemTable";
                if ((comands.ExecuteScalar().ToString()) != "")
                    total_store_thaan.Text = comands.ExecuteScalar().ToString();
                else
                    total_store_thaan.Text = "0.00";

                // calculting total receiving rupees from clients with in date

                comands.CommandText = "select sum(Recieved_Rupees) from RecieveOrderRupees where Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                if ((comands.ExecuteScalar().ToString()) != "")
                    receiving_rupees.Text = comands.ExecuteScalar().ToString();
                else
                    receiving_rupees.Text = "0.00";

                // calculating total earning from orders with in the dates

                comands.CommandText = "select sum(Order_Total_Rupees) from OrderTable where Order_Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                if ((comands.ExecuteScalar().ToString()) != "")
                    total_earning.Text = (Math.Ceiling(float.Parse(comands.ExecuteScalar().ToString()))).ToString();
                else
                    total_earning.Text = "0.00";

                // calculating total expences

                total_daily_expence = double.Parse(manger_daily_ecpence.Text);
                total_industerial_expence = double.Parse(induterial_expence.Text);
                total_employees_salaries = double.Parse(total_employee_salaries.Text);
                total_expence.Text = (Math.Ceiling(total_daily_expence+total_industerial_expence+total_employees_salaries)).ToString();

                // calculating profit or loss

                if((double.Parse(total_earning.Text))>(double.Parse(total_expence.Text)))
                {
                    total_profit.Text = (Math.Ceiling((double.Parse(total_earning.Text))-(double.Parse(total_expence.Text)))).ToString();
                    total_loss.Text = "0.00";
                }
                else if ((double.Parse(total_earning.Text)) < (double.Parse(total_expence.Text)))

                {
                    total_loss.Text = (Math.Ceiling((double.Parse(total_expence.Text)) - (double.Parse(total_earning.Text)))).ToString();
                    total_profit.Text = "0.00";
                }
                else
                {
                    total_loss.Text = "0.00";
                    total_profit.Text = "0.00";
                }

                Connect.Close();
            }catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void sumary_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            populpate_data();

            menuForm.tableLayoutPanel2.Visible = false;
        }
        private void check_manger_rece_rupees_Click(object sender, EventArgs e)
        {
            manager_income_details_Form obj = new manager_income_details_Form();
            obj.set_object_of_menu_form(menuForm);
            obj.set_object_of_this_form(obj);
            obj.set_object_of_sumary_form(Sumary_Form);
            obj.setFalg(true);
            obj.populate_income_data_according_to_the_summary(start_date, end_date);
            obj.ShowDialog();
        }

        private void check_manger_dail_ecpence_Click(object sender, EventArgs e)
        {
            daily_expence_Form obj = new daily_expence_Form();
            obj.set_object_of_menu_form(menuForm);
            obj.set_object_of_this_form(obj);
            obj.set_object_of_summary_form(Sumary_Form);
            obj.setFalg(true);
            obj.calculate_total_of_expence_rupees_according_to_the_summary(start_date,end_date);
            obj.populate_data_according_to_the_summary(start_date,end_date);
            obj.ShowDialog();
        }

        private void check_indsterial_expence_Click(object sender, EventArgs e)
        {
            Check_Induterial_Expence_Form obj = new Check_Induterial_Expence_Form(this);
            obj.set_dates(start_date,end_date);
            obj.ShowDialog();
        }

        private void check_pending_orders_Click(object sender, EventArgs e)
        {
            employee_list_form obj = new employee_list_form("call from summary");
            obj.set_dates(start_date,end_date);
            obj.set_object_of_menu_form(menuForm);
            obj.set_object_of_summary_form(Sumary_Form);
            obj.set_obj_of_form(obj);
            obj.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        private void total_borrow_rupees_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {
            tableLayoutPanel6.Location = new Point
                 (
                 panel2.ClientSize.Width / 2 - tableLayoutPanel6.Size.Width / 2,
                 panel2.ClientSize.Height / 2 - tableLayoutPanel6.Size.Height / 2
                 );
            tableLayoutPanel6.Anchor = AnchorStyles.None;
        }


        private void check_delivered_orders_Click(object sender, EventArgs e)
        {
            Delivered_Order_List_Form obj = new Delivered_Order_List_Form();
            obj.set_object_of_menu_form(menuForm);
            obj.set_object_of_sumary_from(Sumary_Form);
            obj.setForm_Object(obj);
            obj.set_dates(start_date,end_date);
            obj.set_call(true);
            obj.populate_orders_according_to_the_summary_form();
            obj.ShowDialog();
        }

        private void check_pending_prder_rupees_Click(object sender, EventArgs e)
        {
            employee_list_form obj=new employee_list_form("call for order's pending rupees");
            obj.set_object_of_menu_form(menuForm);
            obj.set_dates(start_date, end_date);
            obj.set_object_of_summary_form(Sumary_Form);
            obj.set_obj_of_form(obj);
            obj.ShowDialog();
        }
    }
}
