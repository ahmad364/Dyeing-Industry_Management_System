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
    public partial class Edit_Order_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataAdapter dataAdapter;

        DataTable items_table = new DataTable();

        employee_list_form Employee_List_Form;

        order_organizer_Class order_Organizer;

        public Order_Form order_Form;

        Edit_Order_Form Edit_Order_form;

        private float items_pay = 0;
        string imgLoc = "";
        public Edit_Order_Form(order_organizer_Class obj1,employee_list_form obj2)
        {

            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;

            order_Organizer = obj1;
            Employee_List_Form = obj2;

        }
        public void set_edit_order_form_object(Edit_Order_Form obj)
        {
            Edit_Order_form = obj;
        }
        public void set_order_form_object(Order_Form obj)
        {
            order_Form = obj;
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
                this.Close();
                order_Form = new Order_Form(order_Organizer, Employee_List_Form);
                order_Form.set_order_form_page_object(order_Form);
                order_Form.ShowDialog();       
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
        public void clear_grid_view()
        {
            ((DataTable)show_orderItems_dataGridView.DataSource).Rows.Clear();
            show_orderItems_dataGridView.Refresh();
        }
        public void Populate_ordered_items()
        {

            try
            {

                Connect.Open();
                dataAdapter = new SqlDataAdapter("select Item_Name as 'Item Name',Total_Width as 'Width', Total_Height as 'Height', Height_Price as 'Item Rate', Total_Thans as 'Total Thaan',Measure_In as 'Measured In' ,Item_Size as 'Item Size',Client_Berhoti as 'Client Berhoti',Your_Berhoti as 'Your Berhoti',Item_Pay as 'Item Pay' from ItemTable where Order_Id='" + order_Organizer.order_id + "'", Connect);
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
        public void populate_order_data()
        {
            c_name_textBox.Text = order_Organizer.c_name;
            c_cnic_textBox.Text = order_Organizer.c_cnic;
            address_textBox.Text = order_Organizer.c_address;
            PhNumber_textBox.Text = order_Organizer.c_phoNumber;
            order_dateTimePicker.Value = Convert.ToDateTime(order_Organizer.order_date);
            order_number_textBox.Text = order_Organizer.order_no.ToString();
            order_status_textBox.Text = order_Organizer.order_status;
            total_item_pay_textBox.Text = order_Organizer.total_item_pay.ToString();
            order_rece_rupees_textBox.Text = order_Organizer.rece_rupees.ToString();
            net_pay_textBox.Text = order_Organizer.netPay.ToString();
            items_pay = order_Organizer.total_item_pay;
        }
        private void Edit_Order_Form_Load(object sender, EventArgs e)
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

            // populate order data
            populate_order_data();
            Populate_ordered_items();

        }

        private void button5_Click(object sender, EventArgs e)
        {

                this.Close();
                order_Form = new Order_Form(order_Organizer, Employee_List_Form);
                order_Form.set_order_form_page_object(order_Form);
                order_Form.ShowDialog();
            
        }
        private void update_client_image()
        {
            try
            {
                // converting the image to binary
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);

                comands.Parameters.Clear();

                Connect.Open();
                comands.CommandText = "update OrderTable set Client_Image=@img where Order_Id='" + order_Organizer.order_id + "'";
                comands.Parameters.Add(new SqlParameter("@img", img));
                comands.ExecuteNonQuery();
                Connect.Close();

            }
            catch (Exception)
            {
                Connect.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // update order
            try
            {
                // updating client's image
                update_client_image();

                Connect.Open();
                comands.CommandText = "update OrderTable set Client_Name='"+c_name_textBox.Text+ "',Client_CNIC='"+c_cnic_textBox.Text+ "',Client_Address='"+address_textBox.Text+ "',Client_Phone_Number='"+PhNumber_textBox.Text+ "',Order_Date='"+order_dateTimePicker.Text+ "',Order_Status='"+order_status_textBox.Text+ "' where Order_Id='"+order_Organizer.order_id+"'";
                comands.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show("Order has been updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // update data in order organizer class
                order_Organizer.c_name = c_name_textBox.Text;
                order_Organizer.c_cnic = c_cnic_textBox.Text;
                order_Organizer.c_phoNumber = PhNumber_textBox.Text;
                order_Organizer.c_address = address_textBox.Text;
                order_Organizer.order_date = order_dateTimePicker.Text;
                order_Organizer.order_status = order_status_textBox.Text;
      
                order_Form.populate_items_according_to_the_calls();
            }
            catch (Exception exc) 
            {
                Connect.Close(); 
                MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void set_TotalPay()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select sum(Item_Pay) from ItemTable where Order_Id='"+order_Organizer.order_id+"' ";
                total_item_pay_textBox.Text = comands.ExecuteScalar().ToString();
                items_pay = float.Parse(comands.ExecuteScalar().ToString());
                Connect.Close();
                net_pay_textBox.Text = Convert.ToString(items_pay-Convert.ToInt64(order_rece_rupees_textBox.Text));
                // updating data into order organizer class which holds order data
                order_Organizer.total_item_pay = float.Parse(total_item_pay_textBox.Text);
                order_Organizer.netPay = float.Parse(net_pay_textBox.Text);
                
            }
            catch (Exception exc) {
                Connect.Close();
                MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
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
                Ordered_Items_Edit_Form obj = new Ordered_Items_Edit_Form(order_Organizer, Edit_Order_form);
                obj.ShowDialog();
            }
            else
                MessageBox.Show("You can't update order's items now because you have delivered them","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Order_Raceed_Pic_Form obj = new Order_Raceed_Pic_Form();
            obj.set_order_id(order_Organizer.order_id);
            obj.update_button.Visible = true;
            obj.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Recieve_Order_Rupees_Form obj = new Recieve_Order_Rupees_Form(order_Organizer,float.Parse(total_item_pay_textBox.Text),float.Parse(order_rece_rupees_textBox.Text));
            obj.set_object_of_required_form(obj,Employee_List_Form,Edit_Order_form);
            obj.ShowDialog();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files(*.*)|*.*";
            dlg.Title = "Select Client Image";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBox1.ImageLocation = imgLoc;
                pictureBox1.BackColor = Color.White;
            }
        }
    }
}
