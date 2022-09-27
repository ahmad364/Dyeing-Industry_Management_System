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
    public partial class Ordered_Items_Delivery_Details_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataReader DataReader;

        order_organizer_Class Order_Organizer;
        public Ordered_Items_Delivery_Details_Form(order_organizer_Class obj)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;

            Order_Organizer = obj;
        }

        private void Ordered_Items_Delivery_Details_Form_Load(object sender, EventArgs e)
        {
            delivery_date_label.Text = DateTime.Today.ToString("dd    MMMM    yyyy");
            populate_delivery_number_to_comobox();
        }
        private void populate_delivery_number_to_comobox()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Delivery_Number from DeliveryTable where Order_Id='"+Order_Organizer.order_id+"'";
                DataReader = comands.ExecuteReader();
                if(DataReader.HasRows)
                {
                    while(DataReader.Read())
                    {
                        delivery_number_comboBox.Items.Add(DataReader[0]);
                    }
                }

                Connect.Close();
            }
            catch (Exception) 
            {
                Connect.Close();
            }
        }

        private void set_delivery_raceed_image()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "Select Raceed_Image from DeliveryTable where Order_Id='" + Order_Organizer.order_id + "' and Delivery_Number='"+(delivery_number_comboBox.SelectedIndex +1)+"'";
                DataReader = comands.ExecuteReader();
                DataReader.Read();
                if (DataReader.HasRows)
                {
                    byte[] img = (byte[])(DataReader[0]);
                    if (img == null)
                        raceed_image_pictureBox.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        raceed_image_pictureBox.Image = Image.FromStream(ms);
                    }
                }

                Connect.Close();

            }catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }
        private void delivery_number_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                set_delivery_raceed_image();
                Connect.Open();
                comands.CommandText = "select Total_Width from ItemTable where Order_Id='" + Order_Organizer.order_id + "'";
                ittem_width_textBox.Text = comands.ExecuteScalar().ToString();
                comands.CommandText = "select Item_Name,Delivered_Height,Delivered_Thaan,Measured_In,Delivered_Size,Delivery_Pay,Delivery_Date from DeliveryTable where Order_Id='"+Order_Organizer.order_id+"' and Delivery_Number='"+Convert.ToString(delivery_number_comboBox.Text)+"'";
                SqlDataReader reader = comands.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                        item_name_textBox.Text = reader[0].ToString();
                        delivered_height_textBox.Text = reader[1].ToString();
                        delivered_thaan_textBox.Text = reader[2].ToString();
                        measured_in_textBox.Text = reader[3].ToString();
                        delivery_size_textBox.Text = reader[4].ToString();
                        delivery_pay_label.Text = reader[5].ToString();
                        delivery_date_label.Text =(reader[6]).ToString();
                    
                }
             
                Connect.Close();
            }
            catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
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

        private void cancel_button_Click(object sender, EventArgs e)
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
    }
}
