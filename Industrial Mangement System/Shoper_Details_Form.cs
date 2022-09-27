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
    public partial class Shoper_Details_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Shoper_Data_Holder_Class Shoper_Data_Holder;
        Shoper_List_Form Shoper_List_form;
        Shoper_Details_Form Shoper_Details_form;
        public Shoper_Details_Form(Shoper_Data_Holder_Class obj1,Shoper_List_Form obj2)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            Shoper_Data_Holder = obj1;
            Shoper_List_form = obj2;
        }
        public void set_object_of_this_form(Shoper_Details_Form obj)
        {
            Shoper_Details_form = obj;
        }
        private void cancel_button_Click(object sender, EventArgs e)
        {
            Purchased_Items_Record_Form obj = new Purchased_Items_Record_Form(Shoper_Data_Holder);
            obj.ShowDialog();
        }

        private void Shoper_Details_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            Connect.Open();

            try
            {

                //   assigning emp image to picture box
                string sql = "Select Image from Shoper where CNIC='" + Shoper_Data_Holder.cnic + "'";
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


            // assigning shoper data to textbox

            name_textBox.Text = Shoper_Data_Holder.name;
            father_name_textBox.Text = Shoper_Data_Holder.father_name;
            Cnic_textBox.Text = Shoper_Data_Holder.cnic;
            ph_number_textBox.Text = Shoper_Data_Holder.phone_number;
            date_textBox.Text = Convert.ToDateTime(Shoper_Data_Holder.date).ToString("dd      MMMM      yyyy");
            details_textBox.Text = Shoper_Data_Holder.details;
            address_textBox.Text = Shoper_Data_Holder.address;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to delete this Shopper?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Connect.Open();
                    comands.CommandText = "delete from Shoper where CNIC='" + Shoper_Data_Holder.cnic + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from Purchase where Shopper_CNIC='" + Shoper_Data_Holder.cnic + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from ReturnBorrowedRupees where Shopper_CNIC='" + Shoper_Data_Holder.cnic + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    MessageBox.Show(Shoper_Data_Holder.name + " has been deleted from the record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Shoper_List_form.populate_shopers_userControl();
                    this.Close();

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);

                    Connect.Close();
                }
            }
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            this.Close();
            Edit_Shoper_Form obj = new Edit_Shoper_Form(Shoper_Data_Holder,Shoper_List_form);
            obj.ShowDialog();
        }

        private void pay_return_button_Click(object sender, EventArgs e)
        {
            Pay_Return_Rupees_To_Shopper_Form obj = new Pay_Return_Rupees_To_Shopper_Form(Shoper_Data_Holder);
            obj.set_object_of_this_form(obj);
            obj.ShowDialog();
        }

        private void purchase_button_Click(object sender, EventArgs e)
        {
            Purcahse_Product_from_Shopper_Form obj = new Purcahse_Product_from_Shopper_Form(Shoper_Data_Holder);
            obj.ShowDialog();
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
    }
}
