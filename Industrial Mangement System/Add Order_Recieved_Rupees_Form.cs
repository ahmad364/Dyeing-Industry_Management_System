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
    public partial class Add_Order_Recieved_Rupees_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Recieve_Order_Rupees_Form Recieve_Order_Rupees_form;
        order_organizer_Class order_Organizer_Class;
        employee_list_form employee_List_Form;
        Edit_Order_Form Edit_Order_form;

        float Total_Order_Rupees = 0, total_receieved_rupees = 0;

        public Add_Order_Recieved_Rupees_Form(order_organizer_Class obj1,Recieve_Order_Rupees_Form obj2,employee_list_form obj3,Edit_Order_Form obj4,float t_o_r, float t_r_r)
        {

            InitializeComponent();


            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            order_Organizer_Class = obj1;
            Recieve_Order_Rupees_form = obj2;
            employee_List_Form = obj3;
            Edit_Order_form = obj4;

            Total_Order_Rupees = t_o_r;
            total_receieved_rupees = t_r_r;
        }

        private void rupees_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (float.Parse(rupees_textBox.Text) > float.Parse(rem_rupees_textBox.Text))
                {
                    MessageBox.Show("Receieve rupees must be less than or equal to total item pay rupees", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rupees_textBox.Text = "0";
                }

            }
            catch (Exception)
            {
                rupees_textBox.Text = "0";
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void check_order_delivered_or_Not()
        {
            Connect.Open();
            comands.CommandText = "select Delivered_All_Order_Items from OrderTable where Order_Id='" + order_Organizer_Class.order_id + "'";
            if (comands.ExecuteScalar().ToString() == "Yes")
            {
                // check receive complete rupees of order 
                if (float.Parse(rem_rupees_textBox.Text) < 1)
                {

                    comands.CommandText = "update OrderTable set Delivered='Yes' where Order_Id='" + order_Organizer_Class.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "update OrderTable set Delivery_Date=('" + DateTime.Today.Date.ToString("dd MMMM yyyy") + "') where Order_Id='" + order_Organizer_Class.order_id + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("You have recieved total order rupees", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Edit_Order_form.order_Form.Close();
                    Edit_Order_form.Close();
                    Recieve_Order_Rupees_form.Close();
                    employee_List_Form.populate_order_items_according_to_the_call();
                    this.Close();

                }

            }

            Connect.Close();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (float.Parse(rupees_textBox.Text) > 0)
                {
                    Connect.Open();
                    comands.CommandText = "update OrderTable set Order_Recieved_Rupees=Order_Recieved_Rupees+'" + float.Parse(rupees_textBox.Text) + "',Order_NetPay=Order_NetPay-'" + float.Parse(rupees_textBox.Text) + "' where Order_Id='" + order_Organizer_Class.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "insert into RecieveOrderRupees(Date,Details,Recieved_Rupees,Total_Order_Rupees,Remaining_Rupees,Order_Id) values('" + dateTimePicker.Text + "','" + details_textBox.Text + "','" + Convert.ToInt64(rupees_textBox.Text) + "','"+Total_Order_Rupees+"','" + (Convert.ToInt64(rem_rupees_textBox.Text) - Convert.ToInt64(rupees_textBox.Text)) + "','" + order_Organizer_Class.order_id + "')";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("Recieved Rupees has been saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Recieve_Order_Rupees_form.total_rece_Rupees_label.Text = (float.Parse(Recieve_Order_Rupees_form.total_rece_Rupees_label.Text) + float.Parse(rupees_textBox.Text)).ToString();
                    order_Organizer_Class.rece_rupees = order_Organizer_Class.rece_rupees + float.Parse(rupees_textBox.Text);
                    order_Organizer_Class.netPay = order_Organizer_Class.netPay - float.Parse(rupees_textBox.Text);

                    rem_rupees_textBox.Text = (float.Parse(rem_rupees_textBox.Text) - float.Parse(rupees_textBox.Text)).ToString();
                    rupees_textBox.Text = "0";
                    details_textBox.Clear();

                    // check order was completed or not
                    check_order_delivered_or_Not();

                    // changing the data of other form because of recievening order rupees
                    Edit_Order_form.populate_order_data();
                    Recieve_Order_Rupees_form.clear_grid_view();
                    Recieve_Order_Rupees_form.populate_recieved_rupees_data_to_gridview();

                }
                else
                    MessageBox.Show("Enter Order Recieved Rupees","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Add_Order_Recieved_Rupees_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            dateTimePicker.Text = DateTime.Today.ToString("dd     MMMM     yyyy");
            rem_rupees_textBox.Text = (Total_Order_Rupees - total_receieved_rupees).ToString();
        }
    }
}
