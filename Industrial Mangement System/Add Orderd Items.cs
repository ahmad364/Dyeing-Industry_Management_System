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
    public partial class Add_Orderd_Items : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Recieved_Order_Form Recieved_Order_page;

        string order_id;
        int item_number = 0;

        public Add_Orderd_Items(Recieved_Order_Form obj)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            Recieved_Order_page = obj;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int get_total_items()
        {
            return item_number;
        }

        private void Add_Orderd_Items_Load(object sender, EventArgs e)
        { 
            order_id = Recieved_Order_page.get_order_id();
            comands.Connection = Connect;
            // get items number
            Connect.Open();
            comands.CommandText = "select count(Item_Id) from ItemTable where Order_id='" + order_id + "'";
            item_number = Convert.ToInt32(comands.ExecuteScalar());
            item_number++;
            Connect.Close();
            item_no_textBox.Text = Convert.ToString(item_number);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(item_name_textBox.Text) || Convert.ToInt32(increasing_size_textBox.Text) < 0 || Convert.ToInt32(total_width_textBox.Text) <= 0 || Convert.ToInt32(total_height_textBox.Text) <= 0 || Convert.ToInt32(thans_textBox.Text) <= 0 || Convert.ToInt32(hieght_price_textBox.Text) <= 0)
                {
                    MessageBox.Show("Enter complete and correct information of items", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Connect.Open();
                    comands.CommandText = "insert into ItemTable(Item_Id,Item_Name,Total_Width,Total_Height,Height_Price,Total_Thans,Item_Pay,Order_Id,Item_Number,Item_Size,Measure_In,Delivered,Delivered_Width,Delivered_Height,Delivered_Thans,Client_Berhoti,Your_Berhoti) values('" + Convert.ToInt32(item_no_textBox.Text) + "_" + order_id + "','" + item_name_textBox.Text + "','" + float.Parse(total_width_textBox.Text) + "','" + float.Parse(total_height_textBox.Text) + "','" + float.Parse(hieght_price_textBox.Text) + "','" + Convert.ToInt32(thans_textBox.Text) + "','" + float.Parse(item_pay_textBox.Text) + "','" + order_id + "','" + Convert.ToInt32(item_no_textBox.Text) + "','" + Convert.ToString(double.Parse(total_width_textBox.Text) * double.Parse(total_height_textBox.Text)) + "','" + Measure_In_textBox.Text + "','No','0','0','0','" + float.Parse(increasing_size_textBox.Text) + "','0')";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    Recieved_Order_page.clear_grid_view();
                    Recieved_Order_page.Populate_ordered_items();
                    Recieved_Order_page.set_TotalPay(float.Parse(item_pay_textBox.Text));
                    Recieved_Order_page.order_save = false;

                    // clearing textbox
                    item_name_textBox.Clear();
                    total_height_textBox.Text="0";
                    total_width_textBox.Text="0";
                    hieght_price_textBox.Text="0";
                    thans_textBox.Text="0";
                    Measure_In_textBox.Clear();
                    increasing_size_textBox.Text="0";
                    item_no_textBox.Text = Convert.ToString(Convert.ToInt32(item_no_textBox.Text) + 1);
                    item_pay_textBox.Text="0";
                }

            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void total_width_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch(Exception)
            {
                total_width_textBox.Text = "0";
            }

        }

        private void total_height_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                item_pay_textBox.Text = Convert.ToString(float.Parse(total_height_textBox.Text) * float.Parse(hieght_price_textBox.Text));

            }
            catch (Exception)
            {
                total_height_textBox.Text = "0";
            }
        }

        private void hieght_price_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                item_pay_textBox.Text = Convert.ToString(float.Parse(total_height_textBox.Text) * float.Parse(hieght_price_textBox.Text));
            }
            catch (Exception )
            {
                hieght_price_textBox.Text = "0";
            }
        }

        private void increasing_size_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float size = float.Parse(increasing_size_textBox.Text);
            }
            catch(Exception )
            {
                increasing_size_textBox.Text = "0";
            }
        }

   
    }
}
