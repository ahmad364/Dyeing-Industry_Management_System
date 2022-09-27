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
    public partial class Ordered_Items_Edit_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataReader DataReader;

        order_organizer_Class Order_Organizer;

        Edit_Order_Form Edit_Order_form;

        private int total_items = 0;
        private bool clear_textBoxes_for_adding_new_items = false;
        public Ordered_Items_Edit_Form(order_organizer_Class obj1,Edit_Order_Form obj2)
        {

            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;
            Order_Organizer = obj1;
            Edit_Order_form = obj2;
        }
        public int get_no_of_total_items()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Order_Items from OrderTable where Order_Id='"+Order_Organizer.order_id+"'";
                total_items = Convert.ToInt32(comands.ExecuteScalar().ToString());
                Connect.Close();
            }
            catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
            return total_items;
        }

        public void clear_text_Box()
        {
            // clearing textbox
            item_name_textBox.Clear();
            total_height_textBox.Clear();
            total_width_textBox.Clear();
            hieght_price_textBox.Clear();
            thans_textBox.Clear();
            item_pay_textBox.Clear();
            Measure_In_textBox.Clear();
            client_berhoti_textBox.Clear();
        }
        // button2==Add New Item
        private void button2_Click(object sender, EventArgs e)
        {
            if (clear_textBoxes_for_adding_new_items == false)
            {
                clear_text_Box();
                item_number_comboBox.Text = (get_no_of_total_items() + 1).ToString();
                clear_textBoxes_for_adding_new_items = true;
            }
            else
            {
                // update items number into orderTable
                try
                {                 
                    Connect.Open();
                    // add item into itemTable
                    comands.CommandText = "insert into ItemTable(Item_Id,Item_Name,Total_Width,Total_Height,Height_Price,Total_Thans,Item_Pay,Order_Id,Item_Number,Item_Size,Delivered,Client_Berhoti,Your_Berhoti,Measure_In,First_Height) values('" + Convert.ToInt32(item_number_comboBox.Text) + "_" + Order_Organizer.order_id + "','" + item_name_textBox.Text + "','" + float.Parse(total_width_textBox.Text) + "','" + float.Parse(total_height_textBox.Text) + "','" + float.Parse(hieght_price_textBox.Text) + "','" + Convert.ToInt32(thans_textBox.Text) + "','" + float.Parse(item_pay_textBox.Text) + "','" + Order_Organizer.order_id + "','" + Convert.ToInt32(item_number_comboBox.Text) + "','" + double.Parse(total_width_textBox.Text)*double.Parse(total_height_textBox.Text) +"','No','"+float.Parse(client_berhoti_textBox.Text)+"','0','"+Measure_In_textBox.Text+"','"+float.Parse(total_height_textBox.Text)+"')";
                    comands.ExecuteNonQuery();
                    // updating total number of items of the order into order table
                    comands.CommandText = "update OrderTable set Order_Items='" + Convert.ToInt64(item_number_comboBox.Text) + "' where Order_Id='" + Order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    // updating the order rupees in the text boxes of edit order form
                    Edit_Order_form.set_TotalPay();
                    //now udpating the order rupees in the data base
                    Connect.Open();
                    comands.CommandText = "update OrderTable set Order_Total_Rupees='"+float.Parse(Edit_Order_form.total_item_pay_textBox.Text)+"', Order_NetPay='"+float.Parse(Edit_Order_form.net_pay_textBox.Text)+"' where Order_Id='"+Order_Organizer.order_id+"'";
                    comands.ExecuteScalar();
                    Connect.Close();
                    MessageBox.Show("Item has been added successfully","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Edit_Order_form.clear_grid_view();
                    Edit_Order_form.Populate_ordered_items();
                    item_number_comboBox.Items.Clear();
                    populate_item_numbers_to_comobox();
                    clear_textBoxes_for_adding_new_items = false;

                    // update data into list forms
                    Edit_Order_form.order_Form.populate_items_according_to_the_calls();
                }
                 catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }


        private void Ordered_Items_Edit_Form_Load(object sender, EventArgs e)
        {
            populate_item_numbers_to_comobox();
        }
        public void populate_item_numbers_to_comobox()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Item_Number from ItemTable where Order_Id='" + Order_Organizer.order_id + "'";
                DataReader = comands.ExecuteReader();
                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        item_number_comboBox.Items.Add(DataReader[0]);
                    }
                }

                Connect.Close();
            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }

        private void Update_button_Click(object sender, EventArgs e)
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select case when exists(select top 1* from ItemTable where Order_id='" + Order_Organizer.order_id + "' and Item_Number='" + Convert.ToInt32(item_number_comboBox.Text) + "') then cast(1 as bit) else cast(0 as bit) end";
                if (comands.ExecuteScalar().Equals(true))
                {
                    comands.CommandText = "update ItemTable set Item_Name='" + item_name_textBox.Text + "',Total_Width='" + float.Parse(total_width_textBox.Text) + "',Total_Height='" + float.Parse(total_height_textBox.Text) + "',Height_Price='" + float.Parse(hieght_price_textBox.Text) + "',Total_Thans='" + int.Parse(thans_textBox.Text) + "',Item_Pay='" + float.Parse(item_pay_textBox.Text) + "', Item_Size='" + Double.Parse(total_width_textBox.Text) * Double.Parse(total_height_textBox.Text) + "',Measure_In='"+Measure_In_textBox.Text+"',Client_Berhoti='"+float.Parse(client_berhoti_textBox.Text)+"' where Order_Id='" + Order_Organizer.order_id + "' and Item_Number='" + Convert.ToInt32(item_number_comboBox.Text) + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    // updating the order rupees in the text boxes of edit order form
                    Edit_Order_form.set_TotalPay();
                    //now udpating the order rupees in the data base
                    Connect.Open();
                    comands.CommandText = "update OrderTable set Order_Total_Rupees='" + float.Parse(Edit_Order_form.total_item_pay_textBox.Text) + "', Order_NetPay='" + float.Parse(Edit_Order_form.net_pay_textBox.Text) + "' where Order_Id='" + Order_Organizer.order_id + "'";
                    comands.ExecuteScalar();
                    MessageBox.Show("Itam has been updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Edit_Order_form.clear_grid_view();
                    Edit_Order_form.Populate_ordered_items();

                    // update data into list forms
                    Edit_Order_form.order_Form.populate_items_according_to_the_calls();
                }
                else
                    MessageBox.Show("Please select item first for updating", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Connect.Close();

            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void total_width_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception )
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
            catch (Exception)
            {
                hieght_price_textBox.Text = "0";
            }
        }

        private void item_number_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clear_textBoxes_for_adding_new_items = false;

                Connect.Open();
                comands.CommandText = "select Item_Name,Total_Height,Total_Width,Total_Thans,Client_Berhoti,Measure_In,Height_Price,Item_Pay from ItemTable where Order_ID='"+Order_Organizer.order_id+"' and Item_Number='"+Convert.ToInt32(item_number_comboBox.Text)+"'";
                SqlDataReader reader = comands.ExecuteReader();
                reader.Read();
                if(reader.HasRows)
                {
                    item_name_textBox.Text = reader[0].ToString();
                    total_height_textBox.Text = reader[1].ToString();
                    total_width_textBox.Text = reader[2].ToString();
                    thans_textBox.Text = reader[3].ToString();
                    client_berhoti_textBox.Text = reader[4].ToString();
                    Measure_In_textBox.Text = reader[5].ToString();
                    hieght_price_textBox.Text = reader[6].ToString();
                    item_pay_textBox.Text = reader[7].ToString();
                    
                }
                Connect.Close();
            }
            catch (Exception exc){ Connect.Close(); MessageBox.Show(exc.Message); }
        }

        private void find_berhoti_button_Click(object sender, EventArgs e)
        {
            Calaculate_Items_Berhoti_Form obj = new Calaculate_Items_Berhoti_Form(Order_Organizer,Edit_Order_form,this);
            obj.ShowDialog();
        }
    }
}
