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
    public partial class add_employee_Form : Form
    {
        MenuForm menuForm;

        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        string imgLoc = "";
        Image emp_image;
        public add_employee_Form(MenuForm obj)
        {
            InitializeComponent();

            // setting database connection path
            try
            {
                var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
                var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

                Connect = new SqlConnection(connString);

                // it will help to find that user enter employee image or not
                emp_image = pictureBox1.Image;

            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            menuForm = obj;
        }

        private void backe_button_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void insert_emp_image_into_database_table()
        {
            try
            {
                comands.Parameters.Clear();

                // convert image to binary
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                Connect.Open();
                comands.CommandText = "update Employee set Employee_Image=@img where Employee_Id='"+CnicNumber_textBox.Text+"'";
                comands.Parameters.Add(new SqlParameter("@img", img));
                comands.ExecuteNonQuery();
                Connect.Close();
            }
            catch (Exception) { Connect.Close(); }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {

            float hourly_salary, daily_salary;
            try
            {

                // calculating the hourly and daily salary
                daily_salary =float.Parse((MonthlySalary_textBox.Text));
                daily_salary = daily_salary / 30;
                hourly_salary = daily_salary / 12;
                if (String.IsNullOrWhiteSpace(name_textbox.Text)||String.IsNullOrWhiteSpace(fatherName_textBox.Text)|| String.IsNullOrWhiteSpace(CnicNumber_textBox.Text)|| String.IsNullOrWhiteSpace(Designation_textBox.Text)||String.IsNullOrWhiteSpace(phNumber_textBox.Text) ||Convert.ToInt32(MonthlySalary_textBox.Text)<=0)
                    MessageBox.Show("You are missing some information of Employee kindly fill it", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {

                    Connect.Open();
                    // query
                    comands.CommandText = "insert into Employee(Employee_Id,Employee_Name,Employee_FatherName,Employee_PhoneNumber,Employee_Designation,Employee_JoiningDate,Employee_Details,Employee_Adress,Employee_MonthlySalary,Employee_DailySalary,Employee_HourlySalary,Employee_Advance_Rupees,Employee_Paid_Advance_Rupees) values('" + CnicNumber_textBox.Text + "','" + name_textbox.Text + "','" + fatherName_textBox.Text + "','" + phNumber_textBox.Text + "','" + Designation_textBox.Text + "','" + Joining_dateTimePicker.Text + "','" + details_textBox.Text + "','" + adress_textBox.Text + "','" + Convert.ToInt64(MonthlySalary_textBox.Text) + "','" + daily_salary + "','" + hourly_salary + "',0,0)";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    // inserting emp image
                    insert_emp_image_into_database_table();

                    MessageBox.Show("Employee has been successfully saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // clearing emp data for adding another employee 

                    pictureBox1.Image = emp_image;
                    name_textbox.Clear();
                    fatherName_textBox.Clear();
                    CnicNumber_textBox.Clear();
                    Designation_textBox.Clear();
                    phNumber_textBox.Clear();
                    adress_textBox.Clear();
                    details_textBox.Clear();
                    MonthlySalary_textBox.Text = "0";
                    imgLoc = "";

                }
            }
            catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void add_employee_Form_Load(object sender, EventArgs e)
        {
            Joining_dateTimePicker.Value = DateTime.Today.Date;
            comands.Connection = Connect;
            menuForm.tableLayoutPanel2.Visible = false;
        }


        private void MonthlySalary_textBox_TextChanged(object sender, EventArgs e)
        {
            float daily_salary;
            // calculating the hourly and daily salary
            try
            {
                daily_salary = Convert.ToInt32(MonthlySalary_textBox.Text);
                daily_salary = daily_salary / 30;
                dailySalary_textBox.Text = daily_salary.ToString();
            }
            catch(Exception )
            {
                MonthlySalary_textBox.Text = "0";
            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
    }
}
