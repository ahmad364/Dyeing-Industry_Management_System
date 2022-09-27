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
    public partial class employee_attendance_control : UserControl
    {
        SqlConnection Connect;
        SqlCommand comands = new SqlCommand();

        public string nam, father_name, designation, cnic, monthly_salary, daily_salary, hourly_salary;
        public string selected_date;
        private string joining_date;
        attenadanceMark_form attenadanceMark_Form;
        public employee_attendance_control(string n, string f_n, string des, string cn, string m_s, string d_s, string h_s, string date)
        {

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            nam = n;
            father_name = f_n;
            designation = des;
            cnic = cn;
            monthly_salary = m_s;
            daily_salary = d_s;
            hourly_salary = h_s;
            selected_date = date;
            InitializeComponent();
        }

        public void set_joiningDae(string date)
        {
            joining_date = date;
        }
        #region Properties

        private string emp_name;
        private string emp_designation;
        private string emp_id_card;
        private Image emp_image;
        public string name
        {
            get { return emp_name; }
            set { emp_name = value; employee_name.Text = value; }

        }

        public string Designation
        {
            get { return emp_designation; }
            set { emp_designation = value; employee_designation.Text = value; }
        }

        public string IdCard
        {
            get { return emp_id_card; }
            set { emp_id_card = value; employee_idCard.Text = value; }
        }

        public Image Empl_image
        {
            get { return emp_image; }
            set { emp_image = value; }
        }
        #endregion
        private void setSize_image(object sender, EventArgs e)
        {
            Connect.Open();

            try
            {

                //   assigning emp image to picture box
                string sql = "Select Employee_Image from Employee where Employee_Id='" + cnic+ "'";
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
            catch (Exception)
            {
                Connect.Close();
            }


            employee_name.Text = nam;
            employee_designation.Text = designation;
            employee_idCard.Text = cnic;

            int x = SystemInformation.WorkingArea.Width;
            int y = 120;
            this.Size = new Size(x - 26, y);
        }

        public void set_object_of_attendanceMark_form(attenadanceMark_form obj)
        {
            attenadanceMark_Form = obj;
        }
     
        public void marked_attendance()
        {
            pictureBox1.Visible = true;
        }
        public void no_marked_attendance()
        {
            pictureBox1.Visible = false;

        }
    private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void employee_attendance_control_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro;
        }

        private void employee_attendance_control_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void employee_attendance_control_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(selected_date) < Convert.ToDateTime(joining_date))
                MessageBox.Show("You can't take attendace of this employee for this date because the joining date of this employee is greater than your selected date","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            else
            {
                attendance_mark_Form2 obj = new attendance_mark_Form2(nam, father_name, cnic, designation, monthly_salary, daily_salary, hourly_salary, selected_date);
                obj.set_objeect_of_attendanceMark_form(attenadanceMark_Form);
                obj.ShowDialog();
            }
        }
    }
}
