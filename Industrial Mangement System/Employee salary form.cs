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
    public partial class Employee_salary_form : Form
    {

        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataAdapter DataAdapter;

        DataTable attendance_table = new DataTable();

        DateTime start_date, end_date;

        Employee_Organizer_Class employee_Organizer;

        int payable_days = 0;
        int total_days = 0;
        public Employee_salary_form(Employee_Organizer_Class obj, DateTime date,DateTime date1)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;

            employee_Organizer = obj;
            start_date = date;
            end_date = date1;
            set_employee_image();
            assign_attendance_data_to_gridView();
            set_calculation_data();
        }

      public void  assign_attendance_data_to_gridView()
        {
       
            Connect.Open();
            DataAdapter =new SqlDataAdapter( "select Attendance_Date As Date, Attendance As Attendance, Attendance_Pay As Pay, Overtime As Overtime, Overtime_Hours As 'Overtime Hours',Overtime_Pay As 'Overtime Pay', Bonus As Bonus, Net_Pay As 'Net Pay' from Employee_Attendance where ((Employee_Id='"+employee_Organizer.Emp_Id+ "') and (Attendance_Date between '"+start_date.ToString("yyyyMMdd")+"' and '"+end_date.ToString("yyyyMMdd")+"')) order by Attendance_Date ", Connect);
            DataAdapter.Fill(attendance_table);
            show_attendance_dataGridView.DataSource = attendance_table;
            Connect.Close();

        }
        public void set_calculation_data()
        {
            try
            {

                Connect.Open();

                // calculating number of payable days and number of presents, abscents,doubles etc
                comands.CommandText = "select count(Attendance_Id) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance='Present' and (Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "')";
                present_no_label.Text = comands.ExecuteScalar().ToString();
                payable_days += Convert.ToInt32(comands.ExecuteScalar());
                comands.CommandText = "select count(Attendance_Id) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance='Absent'and (Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "')";
                absent_no_label.Text = comands.ExecuteScalar().ToString();
                comands.CommandText = "select count(Attendance_Id) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance='Half Day'and (Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "')";
                halfDay_no_label.Text = comands.ExecuteScalar().ToString();
                payable_days += Convert.ToInt32(comands.ExecuteScalar());
                comands.CommandText = "select count(Attendance_Id) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance='Double'and (Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "')";
                double_no_label.Text = comands.ExecuteScalar().ToString();
                payable_days += Convert.ToInt32(comands.ExecuteScalar());
                comands.CommandText = "select count(Attendance_Id) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Overtime='Yes'and (Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "')";
                overtime_no_label.Text = comands.ExecuteScalar().ToString();
                comands.CommandText = "select count(Attendance_Id) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Overtime='Yes' and Attendance='Absent' and (Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "')";
                payable_days += Convert.ToInt32(comands.ExecuteScalar());
                comands.CommandText = "select count(Attendance_Id) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance='Leave'and (Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "')";
                leaves_label.Text = comands.ExecuteScalar().ToString();
                payable_days += Convert.ToInt32(comands.ExecuteScalar());
                comands.CommandText = "select count(Attendance_Id) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance='Overtime' and (Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "')";
                payable_days += Convert.ToInt32(comands.ExecuteScalar());

                // get totoal payable days
                comands.CommandText = "select count(Attendance_Id) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "'";
                total_days = Convert.ToInt32(comands.ExecuteScalar());
                // set other basic data
                payable_days_label.Text = payable_days.ToString() + " / " + total_days;
                comands.CommandText = "select sum(Attendance_Pay)+sum(Overtime_Pay) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "'";
                pay_label.Text = comands.ExecuteScalar().ToString();
                comands.CommandText = "select sum(Bonus) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "'";
                bonus_label.Text = comands.ExecuteScalar().ToString();
                comands.CommandText = "select sum(Net_Pay) from Employee_Attendance where Employee_Id='" + employee_Organizer.Emp_Id + "' and Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "'";
                net_pay_label.Text = comands.ExecuteScalar().ToString();
                // set mnager name
                comands.CommandText = "select Name from Manager";
                manager_name_label.Text = comands.ExecuteScalar().ToString();
                Connect.Close();

                // set other basic data
                payable_days_label.Text = payable_days.ToString() + " / " + total_days;

            }catch(Exception)
            {

            }
        }
        private void set_employee_image()
        {
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
                        pictureBox1.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }

                Connect.Close();
            }
            catch(Exception )
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

        private void Employee_salary_form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            // populate employee data
            emp_name_label.Text = employee_Organizer.name;
            emp_designation_label.Text = employee_Organizer.designation;
            emp_phon_no_label.Text = employee_Organizer.phone_number;
            label3.Text = "Employee ID: " + employee_Organizer.Emp_Id;
            monthly_pay_label.Text = employee_Organizer.monthly_salary;
            daily_pay_label.Text = (employee_Organizer.daily_salary).ToString();
            today_date_label.Text = DateTime.Now.ToString("dd  MMMM  yyyy");
            start_date_Label.Text = start_date.ToString("dd  MMMM  yyyy");
            end_date_label.Text = end_date.ToString("dd  MMMM  yyyy");
        }


        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

            panel3.Location = new Point
                 (
                 panel2.ClientSize.Width / 2 - panel3.Size.Width / 2,
                 panel2.ClientSize.Height / 2 - panel3.Size.Height / 2
                 );
            panel3.Anchor = AnchorStyles.None;
        }


        private void label12_Click_1(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
            panel10.Location = new Point
               (
               panel11.ClientSize.Width / 2 - panel10.Size.Width / 2,
               panel11.ClientSize.Height / 2 - panel10.Size.Height / 2
               );
            panel10.Anchor = AnchorStyles.None;
        }

        private void label30_Click(object sender, EventArgs e)
        {
            try
            {

                if (float.Parse(net_pay_label.Text) > 0)
                {
                    float netPay = 0;
                    cut_addvance_rupees_form obj = new cut_addvance_rupees_form(employee_Organizer.Emp_Id);
                    obj.ShowDialog();
                    if (obj.get_flag() == true)
                    {
                        netPay = (float.Parse(net_pay_label.Text) - float.Parse(obj.get_Entered_advance_rupees().ToString()));
                        net_pay_label.Text = netPay.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Employee don't have salary for paying advance rupees", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }catch(Exception)
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to clear the record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Connect.Open();
                comands.CommandText = "delete from Employee_Attendance where ((Employee_Id='" + employee_Organizer.Emp_Id + "') and (Attendance_Date between '" + start_date.ToString("yyyyMMdd") + "' and '" + end_date.ToString("yyyyMMdd") + "'))";
                comands.ExecuteNonQuery();
                Connect.Close();

                // clear data
                ((DataTable)show_attendance_dataGridView.DataSource).Rows.Clear();
                show_attendance_dataGridView.Refresh();

                present_no_label.Text = "0";
                absent_no_label.Text = "0";
                overtime_no_label.Text = "0";
                double_no_label.Text = "0";
                leaves_label.Text = "0";
                halfDay_no_label.Text = "0";

                payable_days_label.Text = "0 / 0";
                pay_label.Text = "0.00";
                bonus_label.Text = "0.00";
                net_pay_label.Text = "0.00";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
