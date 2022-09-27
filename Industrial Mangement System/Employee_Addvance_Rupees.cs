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
    public partial class Employee_Addvance_Rupees : Form
    {

        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Employee_form employee_form;

        string emp_ID;

        public Employee_Addvance_Rupees(string e_n, string f_name, string emp_phoneNumber, string e_id )
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            name_textBox.Text = e_n;
            emp_ID = e_id;
            f_name_textBox.Text = f_name;
            cnic_number_textBox.Text = e_id;
            ph_number_textBox.Text = emp_phoneNumber;

            // connect to database 
            comands.Connection = Connect;

            populate_advance_rupees_record();
        }
        public void set_object_of_employee_form(Employee_form obj)
        {
            employee_form = obj;
        }

        public void populate_advance_rupees_record()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Employee_Advance_Rupees from Employee where Employee_Id='" + emp_ID + "'";
                total_advance_rupees_label.Text = Convert.ToString(comands.ExecuteScalar());
                comands.CommandText = "select Employee_Paid_Advance_Rupees from Employee where Employee_Id='" + emp_ID + "'";
                paid_advance_rupees_label.Text = Convert.ToString(comands.ExecuteScalar());
                Connect.Close();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
                Connect.Close();
            }
            // calculate remaining advance rupees

        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void clear_grid_view()
        {
            ((DataTable)show_returned_borrowed_details_dataGridView.DataSource).Rows.Clear();
            show_returned_borrowed_details_dataGridView.Refresh();
        }
        public void populate_advance_rupees_data_to_gridview()
        {
            try
            {
                Connect.Open();
                DataTable table = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select Date,Details,Advance_Rupees as 'Advance Rupees', Total_Advance_Rupees as 'Remaining Advance Rupees' from EmployeeAdvanceRupees where Employee_CNIC='" + emp_ID + "' order by Date", Connect);
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
  
 

        private void button7_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void Employee_Addvance_Rupees_Load(object sender, EventArgs e)
        {
            // show employee image
            try
            {
                Connect.Open();
                //    employee_pic.BackColor = Color.White;
                string sql = "Select Employee_Image from Employee where Employee_Id='" + emp_ID + "'";
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
            catch (Exception )
            {
                Connect.Close();
            }

            // set today date
            date_label.Text = DateTime.Now.ToString("dd    MMMM    yyyy    hh:mm:tt");
            populate_advance_rupees_data_to_gridview();
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pay_borrowed_rupees_label_Click(object sender, EventArgs e)
        {
            Add_Advance_Rupees_Form obj = new Add_Advance_Rupees_Form(this,emp_ID);
            obj.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to Delete the record of Employee Advance Rupees?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Int64 rem_rupees = Convert.ToInt64(total_advance_rupees_label.Text) - Convert.ToInt64(paid_advance_rupees_label.Text);
                    Connect.Open();
                    comands.CommandText = "update Employee set Employee_Advance_Rupees ='" + rem_rupees + "', Employee_Paid_Advance_Rupees=0 where Employee_Id='" + emp_ID + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from EmployeeAdvanceRupees where Employee_CNIC='" +emp_ID + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    total_advance_rupees_label.Text = rem_rupees.ToString();
                    paid_advance_rupees_label.Text = "0";
                    clear_grid_view();
                    
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
