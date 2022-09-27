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
    public partial class Employee_edit_form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Employee_Organizer_Class employee_Organizer;

        employee_list_form emp_list_form;
        Employee_form emp_form;

        private float daily_salary = 0, hourly_salary = 0;
        string imgLoc = "";
        public Employee_edit_form(Employee_Organizer_Class obj,employee_list_form obj2)
        {
            

            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            emp_list_form = obj2;
            employee_Organizer = obj;
            emp_form = new Employee_form(obj,obj2 );
            // give values to textboxes

            name_textBox.Text = employee_Organizer.name;
            F_Name_textBox.Text = employee_Organizer.father_name;
            Cnic_textBox.Text = employee_Organizer.Emp_Id;
            Ph_Number_textBox.Text = employee_Organizer.phone_number;
            Designation_textBox.Text = employee_Organizer.designation;
            Joining_dateTimePicker.Value = Convert.ToDateTime(employee_Organizer.joining_date);
            M_Salary_textBox.Text = employee_Organizer.monthly_salary;
            D_Salary_textBox.Text = employee_Organizer.daily_salary;
            Details_textBox.Text = employee_Organizer.details;
            Adress_textBox.Text = employee_Organizer.address;

            
        }

        private void update_employee_image()
        {
            try
            {
                // converting the image to binary
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);

                comands.Parameters.Clear();
                Connect.Open();
                comands.CommandText = "update Employee set Employee_Image=@img where Employee_Id='"+employee_Organizer.Emp_Id+"'";
                comands.Parameters.Add(new SqlParameter("@img",img));
                comands.ExecuteNonQuery();
                Connect.Close();

            }catch(Exception)
            {
                Connect.Close();
            }
        }
        private void Update_button_Click(object sender, EventArgs e)
        {
             daily_salary = float.Parse(M_Salary_textBox.Text) / 30;
             hourly_salary = daily_salary / 12;
            comands.Connection = Connect;
          try
            {
                // updating employee image

                update_employee_image();
                Connect.Open();
                comands.CommandText = "update Employee set Employee_Id='" + Cnic_textBox.Text + "',Employee_Name='" + name_textBox.Text + "',Employee_FatherName='" + F_Name_textBox.Text + "',Employee_PhoneNumber='" + Ph_Number_textBox.Text + "',Employee_Designation='" + Designation_textBox.Text + "',Employee_JoiningDate='" + Joining_dateTimePicker.Text + "',Employee_MonthlySalary='" + M_Salary_textBox.Text + "',Employee_DailySalary='" + daily_salary + "',Employee_HourlySalary='" + hourly_salary + "',Employee_Details='" + Details_textBox.Text + "',Employee_Adress='" + Adress_textBox.Text + "' where Employee_Id='"+employee_Organizer.Emp_Id+"'";
                comands.ExecuteNonQuery();
                Connect.Close();

                // if employee's salary is changed then employee attendance pay should be change
                if (Convert.ToInt32(M_Salary_textBox.Text) != Convert.ToInt32(employee_Organizer.monthly_salary))
                    update_attendance_pay();

                MessageBox.Show("Employee's record has been successfully update","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);

                employee_Organizer.Emp_Id = Cnic_textBox.Text;
                employee_Organizer.name = name_textBox.Text;
                employee_Organizer.father_name = F_Name_textBox.Text;
                employee_Organizer.phone_number = Ph_Number_textBox.Text;
                employee_Organizer.designation = Designation_textBox.Text;
                employee_Organizer.monthly_salary = M_Salary_textBox.Text;
                employee_Organizer.daily_salary = daily_salary.ToString();
                employee_Organizer.details = Details_textBox.Text;
                employee_Organizer.address = Adress_textBox.Text;
                employee_Organizer.joining_date = Joining_dateTimePicker.Value.ToString("dd    MMMM    yyyy");

                emp_form = new Employee_form(employee_Organizer, emp_list_form);

                // updating the emolyee items in the list form
                emp_form.emp_list_form.populateItems();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Connect.Close();
            }


        }

        private void update_attendance_pay()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "update Employee_Attendance set Attendance_Pay='" + daily_salary + "' where Employee_Id='" + employee_Organizer.Emp_Id + "' and (Attendance='Present' or Attendance='Leave')";
                comands.ExecuteNonQuery();
                comands.CommandText = "update Employee_Attendance set Attendance_Pay='" + daily_salary * 2 + "' where Employee_Id='" + employee_Organizer.Emp_Id + "' and (Attendance='Double')";
                comands.ExecuteNonQuery();
                comands.CommandText = "update Employee_Attendance set Attendance_Pay='" + daily_salary / 2 + "' where Employee_Id='" + employee_Organizer.Emp_Id + "' and (Attendance='Half Day')";
                comands.ExecuteNonQuery();
                comands.CommandText = "update Employee_Attendance set Hourly_Pay='" + hourly_salary + "' where Employee_Id='" + employee_Organizer.Emp_Id + "'";
                comands.ExecuteNonQuery();
                comands.CommandText = "update Employee_Attendance set Overtime_Pay= Overtime_Hours*Hourly_Pay where Employee_Id='" + employee_Organizer.Emp_Id + "'";
                comands.ExecuteNonQuery();
                comands.CommandText = "update Employee_Attendance set Net_Pay=Attendance_Pay+Overtime_Pay+Bonus where Employee_Id='"+employee_Organizer.Emp_Id+"'";
                comands.ExecuteNonQuery();
                Connect.Close();

            }catch(Exception exception)
            {
                Connect.Close();
                MessageBox.Show(exception.Message);
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
            emp_form.ShowDialog();

        }


        private void button5_Click(object sender, EventArgs e)
        {
            
            this.Close();
            emp_form.ShowDialog();

        }

        private void Employee_edit_form_Load(object sender, EventArgs e)
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

        private void M_Salary_textBox_TextChanged_1(object sender, EventArgs e)
        {
            float daily_salary;
            // calculating the hourly and daily salary
            try
            {
                daily_salary = float.Parse(M_Salary_textBox.Text);
                daily_salary = daily_salary / 30;
                D_Salary_textBox.Text = daily_salary.ToString();
            }
            catch (Exception)
            {
                M_Salary_textBox.Text = "0";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files(*.*)|*.*";
            dlg.Title = "Select Employee Image";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBox1.ImageLocation = imgLoc;
                pictureBox1.BackColor = Color.White;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}
