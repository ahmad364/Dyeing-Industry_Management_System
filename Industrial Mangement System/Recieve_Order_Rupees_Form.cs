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
    public partial class Recieve_Order_Rupees_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Edit_Order_Form Edit_Order_form;
        employee_list_form employee_List_Form;
        order_organizer_Class order_Organizer_Class;
        Recieve_Order_Rupees_Form Recieve_Order_Rupees_form;

        float Total_Order_Rupees = 0, total_receieved_rupees = 0;
        bool cal_from_delieverd_order_form = false;
        public Recieve_Order_Rupees_Form(order_organizer_Class obj1,float t_o_r,float t_r_r)
        {
            InitializeComponent();


            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            order_Organizer_Class = obj1;
            Total_Order_Rupees = t_o_r;
            total_receieved_rupees = t_r_r;

            name_textBox.Text = order_Organizer_Class.c_name;
            cnic_textBox.Text = order_Organizer_Class.c_cnic;
            phone_number_textBox.Text = order_Organizer_Class.c_phoNumber;
            address_textBox.Text = order_Organizer_Class.c_address;

        }
        public void set_called_flag(bool flag)
        {
            cal_from_delieverd_order_form = flag;
        }
        public void set_object_of_required_form(Recieve_Order_Rupees_Form obj1, employee_list_form obj2, Edit_Order_Form obj3)
        {
            Recieve_Order_Rupees_form = obj1;
            employee_List_Form = obj2;
            Edit_Order_form = obj3;
        }
        public void clear_grid_view()
        {
            ((DataTable)show_returned_borrowed_details_dataGridView.DataSource).Rows.Clear();
            show_returned_borrowed_details_dataGridView.Refresh();
        }
        public void populate_recieved_rupees_data_to_gridview()
        {
            try
            {
                Connect.Open();
                DataTable table = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select Date,Details,Recieved_Rupees as 'Recieved Rupees',Total_Order_Rupees as 'Total Order Rupees',Remaining_Rupees as 'Remaining Rupees' from RecieveOrderRupees where Order_Id='"+order_Organizer_Class.order_id+"'", Connect);
                dataAdapter.Fill(table);
                show_returned_borrowed_details_dataGridView.DataSource = table;
                Connect.Close();
            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }
        private void pay_borrowed_rupees_label_Click(object sender, EventArgs e)
        {
            if (cal_from_delieverd_order_form == false)
            {
                Add_Order_Recieved_Rupees_Form obj = new Add_Order_Recieved_Rupees_Form(order_Organizer_Class, Recieve_Order_Rupees_form, employee_List_Form, Edit_Order_form, Total_Order_Rupees, float.Parse(total_rece_Rupees_label.Text));
                obj.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to Delete the record of Order Received Rupees?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Connect.Open();
                    comands.CommandText = "delete from RecieveOrderRupees where Order_Id='" + order_Organizer_Class.order_id + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    clear_grid_view();
                }
                catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Recieve_Order_Rupees_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            Connect.Open();

            try
            {

                //   assigning emp image to picture box
                string sql = "Select Client_Image from OrderTable where Order_Id='" + order_Organizer_Class.order_id + "'";
                comands = new SqlCommand(sql, Connect);
                SqlDataReader reader = comands.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    byte[] img = (byte[])(reader[0]);
                    if (img == null)
                        pictureBox1.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                Connect.Close();
            }
            catch (Exception)
            {
                Connect.Close();
            }
            date_label.Text = DateTime.Now.ToString("dd    MMMM    yyyy    hh:mm:tt");
            total_Order_rupees_label.Text = Total_Order_Rupees.ToString();
            total_rece_Rupees_label.Text = total_receieved_rupees.ToString();

            populate_recieved_rupees_data_to_gridview();
        }
    }
}
