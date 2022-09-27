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
    public partial class Pay_Return_Rupees_To_Shopper_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Shoper_Data_Holder_Class Shoper_Data_Holder;
        Pay_Return_Rupees_To_Shopper_Form Pay_Return_Rupees_To_Shopper_form;
        public Pay_Return_Rupees_To_Shopper_Form(Shoper_Data_Holder_Class obj)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            Shoper_Data_Holder = obj;
            // populate data
            name_textBox.Text = obj.name;
            cnic_textBox.Text = obj.cnic;
            phone_number_textBox.Text = obj.phone_number;
            address_textBox.Text = obj.address;
        }
        public void set_object_of_this_form(Pay_Return_Rupees_To_Shopper_Form obj)
        {
            Pay_Return_Rupees_To_Shopper_form = obj;
        }
        public void clear_grid_view()
        {
            ((DataTable)show_returned_borrowed_details_dataGridView.DataSource).Rows.Clear();
            show_returned_borrowed_details_dataGridView.Refresh();
        }
        public void populate_income_data_to_gridview()
        {
            try
            {
                Connect.Open();
                DataTable table = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select Date,Details,Returned_Rupees as 'Returned Rupees',Total_Borrowed_Rupees as 'Total Borrowed Rupees',Remaining_Rupees as 'Remaining Rupees' from ReturnBorrowedRupees where Shopper_CNIC='"+Shoper_Data_Holder.cnic+"'", Connect);
                dataAdapter.Fill(table);
                show_returned_borrowed_details_dataGridView.DataSource = table;
                Connect.Close();
            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }
        public void populate_rupees()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Total_Borrow_Rupees from Shoper where CNIC='" + Shoper_Data_Holder.cnic + "'";
                total_borrowed_rupees_label.Text = comands.ExecuteScalar().ToString();
                comands.CommandText = "select Return_Borrow_Rupees from Shoper where CNIC='" + Shoper_Data_Holder.cnic + "'";
                returned_borrowed_Rupees_label.Text = comands.ExecuteScalar().ToString();
                Connect.Close();
            }catch(Exception )
            {
                Connect.Close();
            }

        }
        private void Pay_Return_Rupees_To_Shopper_Form_Load(object sender, EventArgs e)
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


            // set today date
            date_label.Text = DateTime.Now.ToString("dd    MMMM    yyyy    hh:mm:tt");
            populate_rupees();
            populate_income_data_to_gridview();
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to Delete the record of Pay Borrowed Rupees?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Int64 rem_rupees = Convert.ToInt64(total_borrowed_rupees_label.Text) - Convert.ToInt64(returned_borrowed_Rupees_label.Text);
                    Connect.Open();
                    comands.CommandText = "update Shoper set Total_Borrow_Rupees='"+rem_rupees+"',Return_Borrow_Rupees=0 where CNIC='" + Shoper_Data_Holder.cnic + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from ReturnBorrowedRupees where Shopper_CNIC='" + Shoper_Data_Holder.cnic + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    total_borrowed_rupees_label.Text = rem_rupees.ToString();
                    returned_borrowed_Rupees_label.Text = "0";
                    clear_grid_view();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void pay_borrowed_rupees_label_Click(object sender, EventArgs e)
        {
            Pay_Borrowed_Rupees_Form obj = new Pay_Borrowed_Rupees_Form(Pay_Return_Rupees_To_Shopper_form,(Convert.ToInt64(total_borrowed_rupees_label.Text)-Convert.ToInt64(returned_borrowed_Rupees_label.Text)),Convert.ToInt64(total_borrowed_rupees_label.Text),Convert.ToInt64(returned_borrowed_Rupees_label.Text));
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
        