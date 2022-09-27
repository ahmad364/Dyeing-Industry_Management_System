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
    public partial class Recieved_Order_Form : Form
    {
        SqlConnection Connect;
        SqlCommand comands = new SqlCommand();

        SqlDataAdapter dataAdapter;

        DataTable items_table = new DataTable();

        Recieved_Order_Form recieved_Order;

        Add_Orderd_Items add_Orderd_Item_obj;

        MenuForm menuForm;

        int order_number;
        float total_Pay = 0;
        public bool order_save = false;
        bool image_save = false;
        string imgLoc = "";
        Image image;
        public Recieved_Order_Form()
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            image = pictureBox1.Image;


        }
        public void set_object_of_menu_form(MenuForm obj)
        {
            menuForm = obj;
        }
        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // this condition tells that user order items or order raceed image saved or not

                if (order_save == false)
                {
                    try
                    {
                        // In case, User add order ites or order raceed image and he does not save the order then you would have to delete bothe from the database

                        Connect.Open();
                        comands.CommandText = "delete from ItemTable where Order_Id='" + get_order_id() + "'";
                        comands.ExecuteNonQuery();
                        comands.CommandText = "delete from Order_Raceed_Image_Table where Order_Id='" + get_order_id() + "'";
                        comands.ExecuteNonQuery();
                        Connect.Close();
                    }
                    catch (Exception exc)
                    {
                        Connect.Close();
                        MessageBox.Show(exc.Message);
                    }
                }

                System.Windows.Forms.Application.Exit();
            }
        }
        public void set_object_of_this_form(Recieved_Order_Form obj)
        {
            recieved_Order = obj;
        }
        public void set_image_save(bool f)
        {
            image_save = f;
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (order_save == false)
            {
                try
                {
                    // In case, User add order ites or order raceed image and he does not save the order then you would have to delete bothe from the database

                    Connect.Open();
                    comands.CommandText = "delete from ItemTable where Order_Id='" + get_order_id() + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from Order_Raceed_Image_Table where Order_Id='" + get_order_id() + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                }
                catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message);
                }
            }
            this.Close();
        }

        private void Recieved_Order_Form_Load(object sender, EventArgs e)
        {

            add_Orderd_Item_obj = new Add_Orderd_Items(recieved_Order);
            comands.Connection = Connect;

            
            Populate_ordered_items();

            orderNumber_textBox.Text = Convert.ToString(get_Order_Number() + 1);
            order_dateTimePicker.Value = DateTime.Today.Date;

            menuForm.tableLayoutPanel2.Visible = false;
        }
        private int get_Order_Number()
        {
            
            Connect.Open();
            comands.CommandText = "select case when exists(select top 1* from OrderTable)then cast (1 as bit) else cast(0 as bit) end";
            if (comands.ExecuteScalar().Equals(true))
            {
                comands.CommandText = "select max(Order_Number) from OrderTable";
                order_number = int.Parse(comands.ExecuteScalar().ToString());
            }
            else
                order_number = 0;
            Connect.Close();

            return order_number;
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
                dataAdapter = new SqlDataAdapter("select Item_Name as 'Item Name',Total_Width as 'Width', Total_Height as 'Height', Height_Price as 'Item Rate', Total_Thans as 'Total Thaan',Measure_In as 'Measured In'  ,Item_Size as 'Item Size',Client_Berhoti as 'Client Berhoti',Item_Pay as 'Item Pay' from ItemTable where Order_Id='" + get_order_id() +"'", Connect);
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

        private void label18_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Cnic_Number_textBox.Text) || String.IsNullOrWhiteSpace(Client_name_textBox.Text) || String.IsNullOrWhiteSpace(Phone_Number_textBox.Text) || String.IsNullOrWhiteSpace(Address_textBox.Text))
                MessageBox.Show("Enter Client's information first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                Cnic_Number_textBox.ReadOnly = true;
                add_Orderd_Item_obj.ShowDialog();
            }
        }
        private void insert_client_image_into_database_table()
        {
            try
            {
                comands.Parameters.Clear();

                // convert image to binary
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                Connect.Open();
                comands.CommandText = "update OrderTable set Client_Image=@img where Order_Id='" + get_order_id() + "'";
                comands.Parameters.Add(new SqlParameter("@img", img));
                comands.ExecuteNonQuery();
                Connect.Close();
            }
            catch (Exception) { Connect.Close(); }
        }
        private void Save_button_button_Click(object sender, EventArgs e)
        {
            if (show_orderItems_dataGridView.RowCount == 0)
                MessageBox.Show("Add at least one item for saving order", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            else if (image_save == false)
                MessageBox.Show("Add Order raceed image first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            else if (String.IsNullOrWhiteSpace(Client_name_textBox.Text) || String.IsNullOrWhiteSpace(Phone_Number_textBox.Text) || String.IsNullOrWhiteSpace(Address_textBox.Text) || String.IsNullOrWhiteSpace(orderStatus_textBox.Text))
                MessageBox.Show("You are missing some information of Order kindly fill it","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            else
            {
                try
                {

                    Connect.Open();

                    comands.CommandText = "insert into OrderTable(Order_Id,Order_Date,Order_Status,Order_Number,Order_Items,Order_recieved_Rupees,Order_Total_Rupees,Client_Name,Client_CNIC,Client_Address,Client_Phone_Number,Order_NetPay,Delivered,Delivered_All_Order_Items) values('" + get_order_id() + "','" + order_dateTimePicker.Text + "','" + orderStatus_textBox.Text + "','" + Convert.ToInt32(orderNumber_textBox.Text) + "','" + (Convert.ToInt32(add_Orderd_Item_obj.item_no_textBox.Text) - 1) + "','" + float.Parse(ReceRupees_textBox.Text) + "','" + float.Parse(TotalPay_textBox.Text) + "','" + Client_name_textBox.Text + "','" + Cnic_Number_textBox.Text + "','" + Address_textBox.Text + "','" + Phone_Number_textBox.Text + "','" + float.Parse(NetPay_textBox.Text) + "','No','No')";
                    comands.ExecuteNonQuery();

                    // inserting data into order recieved rupees table
                    if (ReceRupees_textBox.Text != "0")
                    {
                        comands.CommandText = "insert into RecieveOrderRupees(Date,Details,Recieved_Rupees,Total_Order_Rupees,Remaining_Rupees,Order_Id) values('" + order_dateTimePicker.Text + "','Order deny ka time pessy diya','" + Convert.ToInt64(ReceRupees_textBox.Text) + "','" + float.Parse(TotalPay_textBox.Text) + "','" + Convert.ToInt64(NetPay_textBox.Text) + "','" + get_order_id() + "')";
                        comands.ExecuteNonQuery();
                    }
                    Connect.Close();

                    insert_client_image_into_database_table();

                    MessageBox.Show("Order has been saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    order_save = true;
                    image_save = false;

                    // clear texbox for new order
                    ((DataTable)show_orderItems_dataGridView.DataSource).Rows.Clear();
                    show_orderItems_dataGridView.Refresh();
                    Client_name_textBox.Clear();
                    Phone_Number_textBox.Clear();
                    Address_textBox.Clear();
                    Cnic_Number_textBox.Clear();
                    orderNumber_textBox.Text = Convert.ToString(Convert.ToInt64(orderNumber_textBox.Text) + 1);
                    ReceRupees_textBox.Text = "0";
                    NetPay_textBox.Text = "0";
                    TotalPay_textBox.Text = "0";
                    imgLoc = "";
                    pictureBox1.Image = image;
                    orderStatus_textBox.Clear();
                    Cnic_Number_textBox.ReadOnly = false;

                }
                catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }


        }
        public string get_order_id()
        {
            string id = "";

            try
            {
                // order id for the the primary key of orderTable in dataBase

                id = orderNumber_textBox.Text + "_" + Cnic_Number_textBox.Text + "_" + DateTime.Today.Date;
            }
            catch (Exception) { }

            return id;
        }
        
        public void set_TotalPay(float item_pay)
        {
            total_Pay = float.Parse(TotalPay_textBox.Text);
            total_Pay = total_Pay + item_pay;
            TotalPay_textBox.Text = total_Pay.ToString();
            NetPay_textBox.Text = Convert.ToString(item_pay + float.Parse(NetPay_textBox.Text));
        }

        private void ReceRupees_textBox_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (float.Parse(ReceRupees_textBox.Text) > float.Parse(TotalPay_textBox.Text))
                {
                    MessageBox.Show("Receieve rupees must be less than or equal to total item pay rupees","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    ReceRupees_textBox.Text = "0";
                }
                else
                {
                    NetPay_textBox.Text = Convert.ToString(float.Parse(TotalPay_textBox.Text) - float.Parse(ReceRupees_textBox.Text));
                }

            }
            catch (Exception)
            {
                ReceRupees_textBox.Text = "0";
            }

        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            if (order_save == false)
            {
                try
                {
                    // In case, User add order ites or order raceed image and he does not save the order then you would have to delete bothe from the database

                    Connect.Open();
                    comands.CommandText = "delete from ItemTable where Order_Id='"+get_order_id()+"'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from Order_Raceed_Image_Table where Order_Id='"+get_order_id()+"'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                }
                catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message);
                }
            }
            this.Close();
        }


        private void label2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Cnic_Number_textBox.Text) || String.IsNullOrWhiteSpace(Client_name_textBox.Text) || String.IsNullOrWhiteSpace(Phone_Number_textBox.Text) || String.IsNullOrWhiteSpace(Address_textBox.Text))
            {     
                MessageBox.Show("Enter Client's information first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                Cnic_Number_textBox.ReadOnly = true;

                Order_Raceed_Pic_Form obj = new Order_Raceed_Pic_Form();
                obj.set_object_of_received_order_fom(recieved_Order);
                obj.set_order_id(get_order_id());
                obj.ShowDialog();
            }
            
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
