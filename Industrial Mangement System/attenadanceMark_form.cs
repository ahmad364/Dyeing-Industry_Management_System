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
using System.Threading;
namespace Industrial_Mangement_System
{
   
    public partial class attenadanceMark_form : Form
    {

        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataReader data_reader;

        List<employee_attendance_control> emp = new List<employee_attendance_control>();

        attenadanceMark_form attenadanceMark_Form;

        MenuForm menuForm;
        public attenadanceMark_form()
        {

            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;
        }
        public void set_menufrom_object(MenuForm obj)
        {
            menuForm = obj;
        }

        public void set_object_of_form(attenadanceMark_form obj)
        {
            attenadanceMark_Form = obj;
        }
       
       // will run at loading time
        private void DisplayItems(object sender, EventArgs e)
        {
            
            int n = 0;
            try
            {
                Connect.Open();
                comands.CommandText = "select min(Employee_JoiningDate) from Employee";
                selected_dateTimePicker.MinDate = Convert.ToDateTime(comands.ExecuteScalar());
                comands.CommandText = "select count(Employee_Id) from Employee";
                n = Convert.ToInt32(comands.ExecuteScalar().ToString());
                Connect.Close();
            }
            catch(Exception )
            {
                Connect.Close();
            }
            selected_dateTimePicker.Value = DateTime.Today.Date;
            selected_dateTimePicker.MaxDate = DateTime.Today;
            
            populateItems();
            mark_attendance();

            menuForm.tableLayoutPanel2.Visible = false;


        }
        public void populateItems()
        {
            Connect.Open();

            comands.CommandText = "select *from Employee order by Employee_Name";
            data_reader = comands.ExecuteReader();

            int emp_no = 0;
            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    emp.Add(new employee_attendance_control(data_reader["Employee_Name"].ToString(), data_reader["Employee_FatherName"].ToString(), data_reader["Employee_Designation"].ToString(), data_reader["Employee_Id"].ToString(), data_reader["Employee_MonthlySalary"].ToString(), data_reader["Employee_DailySalary"].ToString(),data_reader["Employee_HourlySalary"].ToString(),selected_dateTimePicker.Text));
                    emp[emp_no].set_joiningDae(data_reader["Employee_JoiningDate"].ToString());
                    emp[emp_no].set_object_of_attendanceMark_form(attenadanceMark_Form);
                    emp_no++;
                }
            }
            Connect.Close();

        }
        public void mark_attendance()
        {
            

            foreach (employee_attendance_control control in emp)
            {
                Connect.Open();
                comands.CommandText = "select case when exists(select top 1* from Employee_Attendance where Attendance_Date='" + selected_dateTimePicker.Text + "' and Employee_Id='" + control.cnic + "')then cast (1 as bit) else cast(0 as bit) end";
                if (comands.ExecuteScalar().Equals(false))
                    control.no_marked_attendance();
                else
                    control.marked_attendance();
                control.selected_date = selected_dateTimePicker.Text;
                flowLayoutPanel1.Controls.Add(control);
                Connect.Close();
            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selected_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            mark_attendance();
        }
    }
}
