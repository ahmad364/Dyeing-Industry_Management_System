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
    public partial class cut_addvance_rupees_form : Form
    {

        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        string emp_id;
        int total_advance_rupees = 0, paid_advance_rupees = 0, remaining_advance_rupees = 0;
        bool flag = false;
        public cut_addvance_rupees_form(string id)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            emp_id = id;

            comands.Connection = Connect;

            try
            {
                Connect.Open();
                comands.CommandText = "select Employee_Advance_Rupees from Employee where Employee_Id='" + emp_id + "'";
                total_advance_rupees = Convert.ToInt32(comands.ExecuteScalar().ToString());
                comands.CommandText = "select Employee_Paid_Advance_Rupees from Employee where Employee_Id='" + emp_id + "'";
                paid_advance_rupees = Convert.ToInt32(comands.ExecuteScalar().ToString());
                remaining_advance_rupees = total_advance_rupees - paid_advance_rupees;
                total_advance_rupees_textBox.Text = remaining_advance_rupees.ToString();
                Connect.Close();
            }
            catch(Exception)
            {
                Connect.Close();
            }
        }

        public float get_Entered_advance_rupees()
        {
            float rupees = 0;
            try
            {
               rupees=float.Parse(entered_advance_rupees_textBox.Text);
            }
            catch(Exception)
            {

            }
            return rupees;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                    Connect.Open();
                    comands.CommandText = "update Employee set Employee_Paid_Advance_Rupees=Employee_Paid_Advance_Rupees+'" + Convert.ToInt64(entered_advance_rupees_textBox.Text) + "' where Employee_id='" + emp_id + "'";
                    comands.ExecuteNonQuery();
                    if (total_advance_rupees_textBox.Text=="0")
                    {
                        comands.CommandText = "update Employee set Employee_Paid_Advance_Rupees='" + 0 + "' where Employee_id='" + emp_id + "'";
                        comands.ExecuteNonQuery();
                        comands.CommandText = "update Employee set Employee_Advance_Rupees='" + 0 + "' where Employee_id='" + emp_id + "'";
                        comands.ExecuteNonQuery();
                        comands.CommandText = "delete from EmployeeAdvanceRupees where Employee_CNIC='" + emp_id + "'";
                        comands.ExecuteNonQuery();
                    }
                    if (Convert.ToInt32(entered_advance_rupees_textBox.Text)!=0)
                    MessageBox.Show("Advance rupees has been cut successfully from the employee salary","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    flag = true;
                    Connect.Close();
            }
            catch(Exception)
            { 
                Connect.Close(); 
            }
            
        }

        private void cut_addvance_rupees_form_Load(object sender, EventArgs e)
        {
            //entered_advance_rupees_textBox.Select();
            //entered_advance_rupees_textBox.Select(entered_advance_rupees_textBox.Text.Length, 0);
        }

        
        public bool get_flag()
        {
            return flag;
        }

        
        private void entered_advance_rupees_textBox_TextChanged_3(object sender, EventArgs e)
        {
            try
            {
                if ((Convert.ToInt32(entered_advance_rupees_textBox.Text)) > remaining_advance_rupees)
                {
                    MessageBox.Show("You can't enter more than total advance rupees", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    entered_advance_rupees_textBox.Text = "0";
                }
                else
                    total_advance_rupees_textBox.Text = Convert.ToString(remaining_advance_rupees - (Convert.ToInt32(entered_advance_rupees_textBox.Text)));
            }
            catch (Exception)
            {
                entered_advance_rupees_textBox.Text = "0";
            }
        }

    }
}
