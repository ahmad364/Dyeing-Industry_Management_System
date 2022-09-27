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
 
    public partial class Employee_form : Form
    {

        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        public  employee_list_form emp_list_form;


        Employee_Organizer_Class employee_Organizer;
        public Employee_form(Employee_Organizer_Class obj,employee_list_form obj1 )
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            employee_Organizer = obj;
            emp_list_form = obj1;
            // set data to textbox 
            name_textbox.Text = employee_Organizer.name;
            fatherName_textBox.Text = employee_Organizer.father_name;
            CnicNumber_textBox.Text = employee_Organizer.Emp_Id;
            phNumber_textBox.Text = employee_Organizer.phone_number;
            details_textBox.Text = employee_Organizer.details;
            Designation_textBox.Text = employee_Organizer.designation;
            joining_date_textBox.Text = Convert.ToDateTime(employee_Organizer.joining_date).ToString("dd      MMMM      yyyy");
            MonthlySalary_textBox.Text = employee_Organizer.monthly_salary;
            dailySalary_textBox.Text = employee_Organizer.daily_salary;
            adress_textBox.Text = employee_Organizer.address;

            // get data base connection to write queries 

            comands.Connection = Connect;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Employee_edit_form obj = new Employee_edit_form(employee_Organizer,emp_list_form);
            obj.ShowDialog();

        }

        private void name_textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Employee_Addvance_Rupees obj = new Employee_Addvance_Rupees(employee_Organizer.name, employee_Organizer.father_name, employee_Organizer.phone_number, employee_Organizer.Emp_Id);
            obj.set_object_of_employee_form(this);
            obj.ShowDialog();
        }

        private void Employee_form_Load(object sender, EventArgs e)
        {
            // show employee image
            try
            {
                Connect.Open();
            //    employee_pic.BackColor = Color.White;
                string sql = "Select Employee_Image from Employee where Employee_Id='" + employee_Organizer.Emp_Id + "'";
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
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to delete this Employee?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Connect.Open();
                    comands.CommandText = "delete from Employee where Employee_Id='" + employee_Organizer.Emp_Id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "delete from EmployeeAdvanceRupees where Employee_CNIC='" + employee_Organizer.Emp_Id + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    MessageBox.Show(employee_Organizer.name + " has been deleted from the record", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    emp_list_form.populateItems();
                    this.Close();

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);

                    Connect.Close();
                }
            }
        }

    }
}
