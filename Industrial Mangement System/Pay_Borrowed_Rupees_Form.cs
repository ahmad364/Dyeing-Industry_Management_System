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
    public partial class Pay_Borrowed_Rupees_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Pay_Return_Rupees_To_Shopper_Form Pay_Return_Rupees_To_Shopper_form;
        Int64 Remaining_Rupees = 0;
        Int64 total_borrowed_rupees = 0;
        Int64 returned_borrowed_rupees = 0;
        public Pay_Borrowed_Rupees_Form(Pay_Return_Rupees_To_Shopper_Form obj,Int64 rem_rupees,Int64 total_rupees,Int64 ret_bor_rupees)
        {
            InitializeComponent();


            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            Pay_Return_Rupees_To_Shopper_form = obj;
            Remaining_Rupees = rem_rupees;
            total_borrowed_rupees = total_rupees;
            returned_borrowed_rupees = ret_bor_rupees;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pay_Borrowed_Rupees_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;
            dateTimePicker.Text = DateTime.Today.ToString("dd  MMMM  yyyy");
            rem_rupees_textBox.Text = Remaining_Rupees.ToString();
        }

        private void rupees_textBox_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (Convert.ToInt64(rupees_textBox.Text)>Convert.ToInt64(rem_rupees_textBox.Text))
                {
                    MessageBox.Show("Total of Return borrow rupees must be less than or equal to the of borrow rupees", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rupees_textBox.Text = "0";
                }
            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rupees_textBox.Text = "0";
            }


        }

        private void save_button_Click(object sender, EventArgs e)
        {
            
       try
       {
                if (Convert.ToInt32(rupees_textBox.Text) == 0)
                    MessageBox.Show("You didn't enter rupees ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    Connect.Open();
                    comands.CommandText = "update Shoper set Return_Borrow_Rupees=Return_Borrow_Rupees+'" + Convert.ToInt64(rupees_textBox.Text) + "' where CNIC='" + Pay_Return_Rupees_To_Shopper_form.cnic_textBox.Text + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "insert into ReturnBorrowedRupees(Date,Details,Returned_Rupees,Total_Borrowed_Rupees,Remaining_Rupees,Shopper_CNIC) values('" + dateTimePicker.Text + "','" + details_textBox.Text + "','" + Convert.ToInt64(rupees_textBox.Text) + "','" + total_borrowed_rupees + "','" + (Convert.ToInt64(rem_rupees_textBox.Text) - Convert.ToInt64(rupees_textBox.Text)) + "','" + Pay_Return_Rupees_To_Shopper_form.cnic_textBox.Text + "')";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("Pay Return Rupees has been saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    rem_rupees_textBox.Text = (Convert.ToInt64(rem_rupees_textBox.Text) - Convert.ToInt64(rupees_textBox.Text)).ToString();
                    details_textBox.Clear();
                    rupees_textBox.Text = "0";
                    Pay_Return_Rupees_To_Shopper_form.populate_rupees();
                    Pay_Return_Rupees_To_Shopper_form.clear_grid_view();
                    Pay_Return_Rupees_To_Shopper_form.populate_income_data_to_gridview();
                }
           
       }catch(Exception exc)
       {
           Connect.Close();
           MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
       }

        }
    }
}
