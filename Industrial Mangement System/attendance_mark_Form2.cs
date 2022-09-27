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
    public partial class attendance_mark_Form2 : Form
    {
        // conect to database

        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        public string nam, father_name, designation, cnic, monthly_salary, daily_salary, hourly_salary, attendance, cehcked_overtime;
        public float today_payment = 0;
        public string select_date;

        attenadanceMark_form attenadanceMark_Form;
        public attendance_mark_Form2(string n, string f_n, string cn, string des, string m_s, string d_s, string h_s, string date)
        {
            InitializeComponent();


            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;

            // show employee image
            try
            {
                Connect.Open();
                //    employee_pic.BackColor = Color.White;
                string sql = "Select Employee_Image from Employee where Employee_Id='" + cn + "'";
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

            nam = n;
            father_name = f_n;
            designation = des;
            cnic = cn;
            monthly_salary = m_s;
            daily_salary = d_s;
            hourly_salary = h_s;
            select_date = date;

            // populate employee data

            name_textBox.Text = nam;
            father_name_textBox.Text = father_name;
            cnic_textBox.Text = cnic;
            designation_textBox.Text = designation;
            monthly_salary_textBox.Text = monthly_salary;
            daily_salay_textBox.Text = daily_salary;
            hourly_salary_textBox.Text = hourly_salary;

        }

        public void set_objeect_of_attendanceMark_form(attenadanceMark_form obj)
        {
            attenadanceMark_Form = obj;
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
            attenadanceMark_Form.mark_attendance();
        }

    
       
        private void attendance_mark_Form2_Load(object sender, EventArgs e)
        {
           
            check_visibility_of_updateButton();
        }

        private void check_visibility_of_updateButton()
        {
            Connect.Open();
            comands.CommandText = "select case when exists(select top 1* from Employee_Attendance where Attendance_Date='" + select_date + "' and Employee_Id='" + cnic + "')then cast (1 as bit) else cast(0 as bit) end";

            if (comands.ExecuteScalar().Equals(true))
            {
                comands.CommandText = "select Overtime_Hours from Employee_Attendance where (Employee_Id='" + cnic + "' and Attendance_Date='" + select_date + "')";
                Overtime_Hours_textBox.Text = comands.ExecuteScalar().ToString();
                comands.CommandText = "select Bonus from Employee_Attendance where (Employee_Id='" + cnic + "' and Attendance_Date='" + select_date + "')";
                Bonus_textBox.Text = comands.ExecuteScalar().ToString();
                comands.CommandText = "select Net_Pay from Employee_Attendance where (Employee_Id='" + cnic + "' and Attendance_Date='" + select_date + "')";
                today_payment_textBox.Text = comands.ExecuteScalar().ToString();
                comands.CommandText = "select Overtime from Employee_Attendance where (Employee_Id='" + cnic + "' and Attendance_Date='" + select_date + "')";
                cehcked_overtime = comands.ExecuteScalar().ToString();
                comands.CommandText = "select Attendance from Employee_Attendance where (Employee_Id='"+cnic+"' and Attendance_Date='"+select_date+"') ";
                attendance = comands.ExecuteScalar().ToString();
                // marked attendace according to the record
                if (comands.ExecuteScalar().Equals("Present"))
                {
                    present.Checked = true;
                    today_payment = float.Parse(daily_salary);
                }
                else if (comands.ExecuteScalar().Equals("Absent"))
                {
                    absent.Checked = true;
                    today_payment = 0;
                }
                else if (comands.ExecuteScalar().Equals("Half Day"))
                {
                    halfDay.Checked = true;
                    today_payment = float.Parse(daily_salary)/2;

                }
                else if (comands.ExecuteScalar().Equals("Double"))
                {
                    duble.Checked = true;
                    today_payment = float.Parse(daily_salary)*2;

                }
                else if (comands.ExecuteScalar().Equals("Leave"))
                {
                    paidLeave.Checked = true;
                    today_payment = float.Parse(daily_salary);
                }
                else if(comands.ExecuteScalar().Equals("Overtime"))
                {
                    overtime.Checked = true;
                }
                submit_button.Visible = false;
                update_aattendance_button.Visible = true;
            }
            else
            {
                update_aattendance_button.Visible = false;
            }
            Connect.Close();
        }
    
        
        private void insert_data_into_Employee_attendance_table()
        {
            if (present.Checked == false && absent.Checked == false && halfDay.Checked == false && duble.Checked == false && overtime.Checked == false && paidLeave.Checked == false)
                MessageBox.Show("Select at least one attendance's option","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            else
            {
                // set the vale of overtime that is yes or not
                if (Convert.ToInt32(Overtime_Hours_textBox.Text) > 0)
                    cehcked_overtime = "Yes";
                else
                    cehcked_overtime = "No";

                try
                {
                    Connect.Open();
                    comands.CommandText = "insert into Employee_Attendance(Attendance_Id,Attendance_Date,Attendance,Employee_Id,Overtime,Overtime_Hours,Overtime_pay,Bonus,Net_Pay,Attendance_Pay,Hourly_Pay) values('" + select_date + "_" + cnic + "','" + select_date + "','" + attendance + "','" + cnic + "','" + cehcked_overtime + "','" + Convert.ToInt32(Overtime_Hours_textBox.Text) + "','" + float.Parse(Overtime_Hours_textBox.Text) * float.Parse(hourly_salary) + "','" + Convert.ToInt32(Bonus_textBox.Text) + "','" + float.Parse(today_payment_textBox.Text) + "','" + today_payment + "','"+float.Parse(hourly_salary)+"')";
                    comands.ExecuteNonQuery();

                    Connect.Close();

                    submit_button.Visible = false;
                    update_aattendance_button.Visible = true;
                }
                catch (Exception exception)
                {

                    Connect.Close();
                    MessageBox.Show(exception.Message);
                }
            }
        }
        private void submit_button_Click(object sender, EventArgs e)
        {
            insert_data_into_Employee_attendance_table();
        }

        private void halfDay_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void absent_Click(object sender, EventArgs e)
        {
            try
            {
                attendance = "Absent";
                today_payment = 0;
                today_payment_textBox.Text = Convert.ToString((Convert.ToInt32(Overtime_Hours_textBox.Text) * float.Parse(hourly_salary)) + 0 + Convert.ToInt32(Bonus_textBox.Text));
            }
            catch (Exception )
            {

            }
        }

        private void halfDay_Click(object sender, EventArgs e)
        {
            try
            {
                attendance = "Half Day";
                today_payment = float.Parse(daily_salary) / 2;
                today_payment_textBox.Text = Convert.ToString((Convert.ToInt32(Overtime_Hours_textBox.Text) * float.Parse(hourly_salary)) + float.Parse(daily_salary) / 2 + Convert.ToInt32(Bonus_textBox.Text));
            }
            catch (Exception ) 
            {
            }
        }

        private void duble_Click(object sender, EventArgs e)
        {
            try
            {
                attendance = "Double";
                today_payment = float.Parse(daily_salary) * 2;
                today_payment_textBox.Text = Convert.ToString((int.Parse(Overtime_Hours_textBox.Text) * float.Parse(hourly_salary)) + float.Parse(daily_salary) * 2 + Convert.ToInt32(Bonus_textBox.Text));
            } catch (Exception)
            {

            }
        }

        private void paidLeave_Click(object sender, EventArgs e)
        {
            try
            {
                attendance = "Leave";
                today_payment = float.Parse(daily_salary);
                today_payment_textBox.Text = Convert.ToString((Convert.ToInt32(Overtime_Hours_textBox.Text) * float.Parse(hourly_salary)) + float.Parse(daily_salary)+Convert.ToInt32(Bonus_textBox.Text));
            }
            catch { }
        }

        private void update_aattendance_button_Click(object sender, EventArgs e)
        {
            // set the vale of overtime that is yes or not
            if (Convert.ToInt32(Overtime_Hours_textBox.Text) > 0)
                cehcked_overtime = "Yes";
            else
                cehcked_overtime = "No";
            try
            {
                Connect.Open();
                comands.CommandText = "update Employee_Attendance set Attendance= '" + attendance + "',Overtime='" + cehcked_overtime + "',Overtime_Hours='" + Convert.ToInt32(Overtime_Hours_textBox.Text) + "',Overtime_pay='" + Convert.ToInt32(Overtime_Hours_textBox.Text) * float.Parse(hourly_salary) + "',Bonus='" + Convert.ToInt32(Bonus_textBox.Text) + "',Net_Pay='" + float.Parse(today_payment_textBox.Text) + "',Attendance_Pay='"+today_payment+"' where (Employee_Id='" + cnic + "' and Attendance_Date='"+select_date+"')";
                comands.ExecuteNonQuery();

                Connect.Close();

                MessageBox.Show("Employee's attendance has been updated","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception )
            {
                Connect.Close();
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
            attenadanceMark_Form.mark_attendance();

        }

        private void Overtime_Hours_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                today_payment_textBox.Text = Convert.ToString((Convert.ToInt32(Overtime_Hours_textBox.Text) * float.Parse(hourly_salary)) + today_payment + Convert.ToInt32(Bonus_textBox.Text));
            }
            catch (Exception )
            {
                Overtime_Hours_textBox.Text = "0";
                today_payment_textBox.Text = Convert.ToString(Convert.ToInt32(Bonus_textBox.Text) + today_payment);

            }
        }

        private void Bonus_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                today_payment_textBox.Text = Convert.ToString((Convert.ToInt32(Overtime_Hours_textBox.Text) * float.Parse(hourly_salary)) + today_payment + float.Parse(Bonus_textBox.Text));
            }
            catch (Exception )
            {
                Bonus_textBox.Text = "0";
               today_payment_textBox.Text = Convert.ToString((Convert.ToInt32(Overtime_Hours_textBox.Text) * Convert.ToDouble(hourly_salary))+ today_payment);
            }
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void overtime_Click(object sender, EventArgs e)
        { 
            try{
                attendance = "Overtime";
                today_payment = 0;
                today_payment_textBox.Text = Convert.ToString((Convert.ToInt32(Overtime_Hours_textBox.Text)* float.Parse(hourly_salary)) + Convert.ToInt32(Bonus_textBox.Text));
            }
            catch (Exception) 
            { 
            }
        }

        private void present_Click(object sender, EventArgs e)
        {
            try
            {
                attendance = "Present";
                today_payment = float.Parse(daily_salary);
                today_payment_textBox.Text = Convert.ToString((Convert.ToInt32(Overtime_Hours_textBox.Text) * float.Parse(hourly_salary)) + float.Parse(daily_salary) + Convert.ToInt32(Bonus_textBox.Text));
            }
            catch (Exception ) { }
        }


        private void panel7_Paint_1(object sender, PaintEventArgs e)
        {
            panel7.Location = new Point
                  (
                  panel2.ClientSize.Width / 2 - panel7.Size.Width / 2,
                  panel2.ClientSize.Height / 2 - panel7.Size.Height / 2
                  );
            panel7.Anchor = AnchorStyles.None;
        }
    }
}
