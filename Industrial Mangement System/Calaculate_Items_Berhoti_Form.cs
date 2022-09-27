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
    public partial class Calaculate_Items_Berhoti_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataReader DataReader;

        order_organizer_Class order_Organizer;
        Edit_Order_Form Edit_Order_form;
        Ordered_Items_Edit_Form Ordered_Items_Edit_form;

        public Calaculate_Items_Berhoti_Form(order_organizer_Class _Organizer_Class,Edit_Order_Form edit_Order_Form,Ordered_Items_Edit_Form ordered_Items_Edit)
        {

            InitializeComponent();

            order_Organizer = _Organizer_Class;
            Edit_Order_form = edit_Order_Form;
            Ordered_Items_Edit_form = ordered_Items_Edit;

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;

        }
        private void populate_item_numbers_to_comobox()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Item_Number from ItemTable where Order_Id='" + order_Organizer.order_id + "'";
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

        private void item_number_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Connect.Open();
                comands.CommandText = "select Item_Name,Client_Berhoti from ItemTable where Order_ID='" + order_Organizer.order_id + "' and Item_Number='" + Convert.ToInt32(item_number_comboBox.Text) + "'";
                SqlDataReader reader = comands.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    item_name_textBox.Text = reader[0].ToString();
                    client_berhoti__textBox.Text = reader[1].ToString();

                }
                Connect.Close();
            }
            catch (Exception exc) { Connect.Close(); MessageBox.Show(exc.Message); }
        }

        private void Calculate_button_Click(object sender, EventArgs e)
        {
            if (new_height_textBox.Text == "0" || new_width_textBox.Text == "0")
                MessageBox.Show("New height or New width should by grater than 0", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                float size = 0, previous_size = 0, new_size = 0, p_h = 0, p_w = 0, n_h = 0, n_w = 0;

                try
                {

                    Connect.Open();

                    // getting required data

                    comands.CommandText = "select Total_Width from ItemTable where Order_Id='" + order_Organizer.order_id + "' and Item_Number='" + Convert.ToInt32(item_number_comboBox.Text) + "'";
                    p_w = float.Parse(comands.ExecuteScalar().ToString());
                    comands.CommandText = "select Total_Height from ItemTable where Order_Id='" + order_Organizer.order_id + "' and Item_Number='" + Convert.ToInt32(item_number_comboBox.Text) + "'";
                    p_h = float.Parse(comands.ExecuteScalar().ToString());
                    n_h = float.Parse(new_height_textBox.Text);
                    n_w = float.Parse(new_width_textBox.Text);

                    //calculation
                    previous_size = p_h * p_w;
                    new_size = n_w * n_h;
                    size = (new_size - previous_size) / previous_size;
                    size = size * 100;

                    your_berhoti__textBox.Text = size.ToString();

                    comands.CommandText = "update ItemTable set Total_Height='" + n_h + "', Total_Width='" + n_w + "' , Your_Berhoti='" + size + "', Item_Size='" + new_size + "' where Order_Id='" + order_Organizer.order_id + "' and Item_Number='" + Convert.ToInt32(item_number_comboBox.Text) + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    // updating order items in the edit order form 
                    Edit_Order_form.clear_grid_view();
                    Edit_Order_form.Populate_ordered_items();

                    // update data in the order items edit form
                    Ordered_Items_Edit_form.clear_text_Box();
                    Ordered_Items_Edit_form.item_number_comboBox.Items.Clear();
                    Ordered_Items_Edit_form.populate_item_numbers_to_comobox();

                }
                catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    new_height_textBox.Text = "0";
                    new_width_textBox.Text = "0";
                }
            }
   
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Calaculate_Items_Berhoti_Form_Load(object sender, EventArgs e)
        {
            populate_item_numbers_to_comobox();
            
        }
    }
}
