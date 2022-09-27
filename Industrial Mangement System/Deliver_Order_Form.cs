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
    public partial class Deliver_Order_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataAdapter dataAdapter;

        DataTable items_table = new DataTable();

        public  employee_list_form Employee_List_Form;
        public  Order_Form Order_form;
        public  Deliver_Order_Form Deliver_Order_form;

        order_organizer_Class order_Organizer;

        public Deliver_Order_Form(order_organizer_Class obj1,Order_Form obj2)
        {
            InitializeComponent();


            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            order_Organizer = obj1;
            Order_form = obj2;

        }
        private void Deliver_Order_Form_Load(object sender, EventArgs e)
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

            // asssign value to textboxes
            name_textBox.Text = order_Organizer.c_name;
            CNIC_textBox.Text = order_Organizer.c_cnic;
            PhNumber_textBox.Text = order_Organizer.c_phoNumber;
            address_textBox.Text = order_Organizer.c_address;
            order_date_textBox.Text= Convert.ToDateTime(order_Organizer.order_date).ToString("dd        MMMM        yyyy");
            orderNumber_textBox.Text = order_Organizer.order_no.ToString();
            orderStatus_textBox.Text = order_Organizer.order_status;
            ReceRupees_textBox.Text = order_Organizer.rece_rupees.ToString();
            TotalPay_textBox.Text = order_Organizer.total_item_pay.ToString();
            NetPay_textBox.Text = order_Organizer.netPay.ToString();

            populate_ordered_items();
        }
        public void clear_grid_view()
        {
            ((DataTable)show_orderItems_dataGridView.DataSource).Rows.Clear();
            show_orderItems_dataGridView.Refresh();
        }
        public void set_Employee_list_form(employee_list_form obj)
        {
            Employee_List_Form = obj;
        }
        public void set_object_of_deliver_form(Deliver_Order_Form obj)
        {
            Deliver_Order_form = obj;
        }
        public void populate_ordered_items()
        {
            try
            {

                Connect.Open();
                dataAdapter = new SqlDataAdapter("select Item_Name as 'Item Name',Total_Width as 'Width', Total_Height as 'Height', Height_Price as 'Item Rate', Total_Thans as 'Total Thaan',Measure_In as 'Measured In', Item_Size as 'Item Size',Client_Berhoti as 'Client Berhoti',Your_Berhoti as 'Your Berhoti',Delivered,Item_Pay as 'Item Pay' from ItemTable where Order_Id='" + order_Organizer.order_id + "'", Connect);
                dataAdapter.Fill(items_table);
                show_orderItems_dataGridView.DataSource = items_table;
                Connect.Close();

            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
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

                    Order_form.ReceRupees_textBox.Text = ReceRupees_textBox.Text;
                    Order_form.clear_gridview();
                    Order_form.populate_ordered_items();
                    this.Close();

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

            panel7.Location = new Point
                 (
                 panel2.ClientSize.Width / 2 - panel7.Size.Width / 2,
                 panel2.ClientSize.Height / 2 - panel7.Size.Height / 2
                 );
            panel7.Anchor = AnchorStyles.None;
        }



        private void label18_Click(object sender, EventArgs e)
        {
            string option = "";
            Connect.Open();
            comands.CommandText = "select Delivered_All_Order_Items from OrderTable where Order_Id='" + order_Organizer.order_id + "'";
            option = comands.ExecuteScalar().ToString();
            Connect.Close();
            if (option == "No")
            {
                Delivered_Items_Form obj = new Delivered_Items_Form(order_Organizer, Deliver_Order_form);
                obj.set_object_of_this_form(obj);
                obj.ShowDialog();
            }
            else
                MessageBox.Show("You have delivered all order's items","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order has been saved successfulyy","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

    
        public void set_rece_rupees(float rup)
        {
            ReceRupees_textBox.Text = rup.ToString();
            NetPay_textBox.Text = Convert.ToString(float.Parse(TotalPay_textBox.Text) - rup);
        }

        private void button5_Click(object sender, EventArgs e)
        {

                    Order_form.ReceRupees_textBox.Text = ReceRupees_textBox.Text;
                    Order_form.clear_gridview();
                    Order_form.populate_ordered_items();
                    this.Close();
         
        }

    }
}
