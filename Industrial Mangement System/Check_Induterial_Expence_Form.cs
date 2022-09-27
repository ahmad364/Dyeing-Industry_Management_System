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
    public partial class Check_Induterial_Expence_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataReader DataReader;

        sumary_Form Sumary_Form;

        DateTime start_date, end_date;
        public Check_Induterial_Expence_Form(sumary_Form obj)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            Sumary_Form = obj;
        }
        public void set_dates(DateTime date1, DateTime date2)
        {
            start_date = date1;
            end_date = date2;
        }
        private void populate_data_to_comobox()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Purchasing_Date from Purchase where Purchasing_Date2 between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                DataReader = comands.ExecuteReader();
                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        purchasing_date_comboBox.Items.Add(DataReader[0].ToString());
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
        private void set_purchasing_raceed_image()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Purchasing_Raceed_Image from Purchase where Purchasing_Date='" + purchasing_date_comboBox.Text + "'";
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

            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }

        private void purchasing_date_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_purchasing_raceed_image();
            try
            {
                Connect.Open();
                comands.CommandText = "select Product_Name,Product_Quantity,Product_Rupees,Purchasing_Pay,Purchasing_Date,Shopper_Name,Shopper_Father_Name,Shopper_CNIC from Purchase where Purchasing_Date='" + purchasing_date_comboBox.Text + "'";
                SqlDataReader reader = comands.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    item_name_textBox.Text = reader[0].ToString();
                    item_quantity_textBox.Text = reader[1].ToString();
                    item_rupees_textBox.Text = reader[2].ToString();
                    pay_item_rupees_label.Text = reader[3].ToString();
                    date_label.Text = (reader[4]).ToString();
                    name_textBox.Text = reader[5].ToString();
                    father_name_textBox.Text = reader[6].ToString();
                    cnic_textBox.Text = reader[7].ToString();
                }

                Connect.Close();
            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void purchase_button_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to delete the record of Purchased Items?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Connect.Open();
                    comands.CommandText = "delete from Purchase where Purchasing_Date2 between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    raceed_image_pictureBox.Image = null;

                    purchasing_date_comboBox.Text = "";
                    item_name_textBox.Clear();
                    item_quantity_textBox.Clear();
                    item_rupees_textBox.Clear();
                    name_textBox.Clear();
                    father_name_textBox.Clear();
                    cnic_textBox.Clear();
                    pay_item_rupees_label.Text = "0.00";
                    date_label.Text = DateTime.Now.ToString("dd   MMMM   yyyy   hh:mm:ss:tt");

                    Sumary_Form.populpate_data();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Check_Induterial_Expence_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            date_label.Text = DateTime.Now.ToString("dd   MMMM   yyyy   hh:mm:ss:tt");

            populate_data_to_comobox();
        }
    }
}
