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
    public partial class Salary_Generator_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Employee_Organizer_Class employee_Organizer;
        public Salary_Generator_Form(Employee_Organizer_Class obj)
        {
            InitializeComponent();


            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;

            employee_Organizer = obj;


            try
            {
                Connect.Open();
                //   assigning emp image to picture box
                string sql = "Select Employee_Image from Employee where Employee_Id='" + employee_Organizer.Emp_Id + "'";
                comands = new SqlCommand(sql, Connect);
                SqlDataReader reader = comands.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    byte[] img = (byte[])(reader[0]);
                    if (img == null)
                        employee_pic.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        employee_pic.Image = Image.FromStream(ms);
                    }
                }
                Connect.Close();
            }
            catch(Exception)
            {
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Location = new Point
                 (
                 this.ClientSize.Width / 2 - panel1.Size.Width / 2,
                 this.ClientSize.Height / 2 - panel1.Size.Height / 2
                 );
            panel1.Anchor = AnchorStyles.None;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Employee_salary_form obj = new Employee_salary_form(employee_Organizer,minimum_dateTimePicker.Value,maximum_dateTimePicker.Value);
            obj.ShowDialog();
        }

        private void Salary_Generator_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            employee_name_textBox.Text = employee_Organizer.name;
            employee_idCard_textBox.Text = employee_Organizer.Emp_Id;
            employee_designation_textBox.Text = employee_Organizer.designation;
            try
            {
                Connect.Open();
                comands.CommandText = "select min(Attendance_Date) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "'";
                minimum_dateTimePicker.MinDate = Convert.ToDateTime(comands.ExecuteScalar());
                maximum_dateTimePicker.MinDate = Convert.ToDateTime(comands.ExecuteScalar());
                // stting value of maximum datetime picker
                minimum_dateTimePicker.Value = Convert.ToDateTime(comands.ExecuteScalar());

                comands.CommandText = "select max(Attendance_Date) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "'";
                minimum_dateTimePicker.MaxDate = Convert.ToDateTime(comands.ExecuteScalar());
                maximum_dateTimePicker.MaxDate = Convert.ToDateTime(comands.ExecuteScalar());
                // stting value of maximum datetime picker
                maximum_dateTimePicker.Value = Convert.ToDateTime(comands.ExecuteScalar());
                Connect.Close();
            }catch(Exception)
            {
                MessageBox.Show("Employee don't have record of working days","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.Close();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
