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
    public partial class Order_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataAdapter dataAdapter;

        DataTable items_table = new DataTable();

        Delivered_Order_List_Form Delivered_Order_List_form;
        employee_list_form employee_List_Form;
        order_organizer_Class order_Organizer;
        Order_Form order_form_page;
        bool Delivered_order;
        public Order_Form(order_organizer_Class obj1,employee_list_form obj2)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            order_Organizer = obj1;
            employee_List_Form = obj2;
        }
        public void set_Deliver_or_not(bool check)
        {
            Delivered_order = check;
        }
        public void set_order_form_page_object(Order_Form obj)
        {
            order_form_page = obj;
        }
        public void set_object_of_Delivered_List_Form(Delivered_Order_List_Form obj)
        {
            Delivered_Order_List_form = obj;
        }
        public void populate_items_according_to_the_calls()
        {
            if (Delivered_order == false)
                employee_List_Form.populate_order_items_according_to_the_call();
            else
                Delivered_Order_List_form.populate_order_items();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        private void Order_Form_Load(object sender, EventArgs e)
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

            Client_name_textBox.Text = order_Organizer.c_name;
            Cnic_Number_textBox.Text = order_Organizer.c_cnic;
            Address_textBox.Text = order_Organizer.c_address;
            Phone_Number_textBox.Text = order_Organizer.c_phoNumber;
            order_date_textBox.Text = Convert.ToDateTime(order_Organizer.order_date).ToString("dd        MMMM        yyyy");
            orderStatus_textBox.Text = order_Organizer.order_status;
            orderNumber_textBox.Text = order_Organizer.order_no.ToString();
            ReceRupees_textBox.Text = order_Organizer.rece_rupees.ToString();
            TotalPay_textBox.Text = order_Organizer.total_item_pay.ToString();
            NetPay_textBox.Text = order_Organizer.netPay.ToString();

            populate_ordered_items();
        }
        public void clear_gridview()
        {

            ((DataTable)show_orderItems_dataGridView.DataSource).Rows.Clear();
            show_orderItems_dataGridView.Refresh();
        }
        public void populate_ordered_items()
        {
            try
            {

                Connect.Open();
                dataAdapter = new SqlDataAdapter("select Item_Name as 'Item Name',Total_Width as 'Width', Total_Height as 'Height', Height_Price as 'Item Rate', Total_Thans as 'Total Thaan', Measure_In as 'Measured In',Item_Size as 'Item Size',Client_Berhoti as 'Client Berhoti',Your_Berhoti as 'Your Berhoti',Delivered as Delivered,Item_Pay as 'Item Pay' from ItemTable where Order_Id='" + order_Organizer.order_id + "'", Connect);
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
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Edit_Order_Form obj = new Edit_Order_Form(order_Organizer,employee_List_Form);
            obj.set_edit_order_form_object(obj);
            obj.set_order_form_object(order_form_page);
            obj.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Deliver_Order_Form obj = new Deliver_Order_Form(order_Organizer,order_form_page);
            obj.set_object_of_deliver_form(obj);
            obj.set_Employee_list_form(employee_List_Form);
            obj.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to delete this Order?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Connect.Open();
                    comands.CommandText = "delete from OrderTable where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from ItemTable where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from DeliveryTable where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from Order_Raceed_Image_Table where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from RecieveOrderRupees where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    MessageBox.Show("Order has been successfully deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    populate_items_according_to_the_calls();
                    this.Close();
                }
                catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message);
                }
            }
        }

        public void set_receieved_rupees(string rup)
        {
            try
            {
                ReceRupees_textBox.Text = rup;
            }catch(Exception)
            { 
            }
        }

        public void ReceRupees_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NetPay_textBox.Text = Convert.ToString(float.Parse(TotalPay_textBox.Text) - Convert.ToInt64(ReceRupees_textBox.Text));
            }
            catch (Exception)
            {
                ReceRupees_textBox.Text = "0";
            }
        }

        private void check_deliveries_label_Click(object sender, EventArgs e)
        {
            Ordered_Items_Delivery_Details_Form obj = new Ordered_Items_Delivery_Details_Form(order_Organizer);
            obj.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to delete this Order?","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Connect.Open();
                    comands.CommandText = "delete from OrderTable where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from ItemTable where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from DeliveryTable where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from Order_Raceed_Image_Table where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from RecieveOrderRupees where Order_Id='" + order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    MessageBox.Show("Order has been successfully deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Delivered_Order_List_form.populate_items_according_to_the_call();
                    this.Close();
                }
                catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            panel4.Location = new Point
           (
           panel3.ClientSize.Width / 2 - panel4.Size.Width / 2,
           panel3.ClientSize.Height / 2 - panel4.Size.Height / 2
           );
            panel4.Anchor = AnchorStyles.None;

            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Order_Raceed_Pic_Form obj = new Order_Raceed_Pic_Form();
            obj.set_order_id(order_Organizer.order_id);
            obj.update_button.Visible = true;
            obj.ShowDialog();

        }

        private void order_rece_rupees_button_Click(object sender, EventArgs e)
        {
            Recieve_Order_Rupees_Form obj = new Recieve_Order_Rupees_Form(order_Organizer, order_Organizer.total_item_pay,order_Organizer.rece_rupees);
            obj.set_called_flag(true);
            obj.ShowDialog();

        }

    }
}
