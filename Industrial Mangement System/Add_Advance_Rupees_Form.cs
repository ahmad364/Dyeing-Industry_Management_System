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
    public partial class Add_Advance_Rupees_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Employee_Addvance_Rupees Employee_Addvance;
        string emp_cnic;
        public Add_Advance_Rupees_Form(Employee_Addvance_Rupees obj,string cnic)
        {

            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;

            Employee_Addvance = obj;
            emp_cnic = cnic;
        }

        private void Add_Advance_Rupees_Form_Load(object sender, EventArgs e)
        {
            dateTimePicker.Text = DateTime.Today.Date.ToString();
        }

        private void rupees_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int i = Convert.ToInt32(rupees_textBox.Text);
            }
            catch(Exception)
            {
                rupees_textBox.Text = "0";
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            
                // adding advance rupees into total of advance rupees
                try
                {
  
                if (Convert.ToInt32(rupees_textBox.Text) <= 0)
                {
                    MessageBox.Show("You did't not enter advance rupees", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Connect.Open();
                    comands.CommandText = "update Employee set Employee_Advance_Rupees=Employee_Advance_Rupees+'" + Convert.ToInt64(rupees_textBox.Text) + "' where Employee_Id='" + emp_cnic + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "insert into EmployeeAdvanceRupees(Employee_CNIC,Date,Details,Advance_Rupees,Total_Advance_Rupees) values('" + emp_cnic + "','" + dateTimePicker.Text + "','" + details_textBox.Text + "','" + Convert.ToInt64(rupees_textBox.Text) + "','" + (Convert.ToInt64(Employee_Addvance.total_advance_rupees_label.Text) - Convert.ToInt64(Employee_Addvance.paid_advance_rupees_label.Text)) + "')";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    MessageBox.Show("Employee's advance rupees successfully added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // updating data in employee advance rupees form and this form
                    details_textBox.Clear();
                    Employee_Addvance.total_advance_rupees_label.Text = ((Convert.ToInt64(Employee_Addvance.total_advance_rupees_label.Text)) + Convert.ToInt64(rupees_textBox.Text)).ToString();
                    rupees_textBox.Text = "0";
                    Employee_Addvance.populate_advance_rupees_data_to_gridview();

                }
                }
                catch (Exception exception)
                {
                    Connect.Close();
                    MessageBox.Show(exception.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
           
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
