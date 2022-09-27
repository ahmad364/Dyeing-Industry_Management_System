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
    public partial class Generate_Summary_Report_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        string ph_number;

        MenuForm menuForm;

        public Generate_Summary_Report_Form()
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
                string sql = "Select Image from Manager";
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
        }

        public void set_object_of_menu_form(MenuForm obj)
        {
            menuForm = obj;
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

        private void generate_salary_button_Click(object sender, EventArgs e)
        {
            sumary_Form obj = new sumary_Form(name_textBox.Text,designation_textBox.Text,ph_number,minimum_dateTimePicker.Value,maximum_dateTimePicker.Value,menuForm);
            obj.set_object_of_this_form(obj);
            obj.ShowDialog();
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

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Generate_Summary_Report_Form_Load(object sender, EventArgs e)
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Name,Designation,CNIC,Phone_Number from Manager";
                SqlDataReader reader = comands.ExecuteReader();
                reader.Read();
                if(reader.HasRows)
                {
                    name_textBox.Text = reader[0].ToString();
                    designation_textBox.Text = reader[1].ToString();
                    idCard_textBox.Text = reader[2].ToString();
                    ph_number = reader[3].ToString();
                }
                Connect.Close();
                // for escaping from exception
                Connect.Open();
                // set the values of dateTimePickers
                comands.CommandText = "select min(Order_Date) from OrderTable";
                minimum_dateTimePicker.MinDate = Convert.ToDateTime(comands.ExecuteScalar().ToString());
                maximum_dateTimePicker.MinDate = Convert.ToDateTime(comands.ExecuteScalar().ToString());
                maximum_dateTimePicker.MaxDate = DateTime.Today;
                minimum_dateTimePicker.MaxDate = DateTime.Today;
                maximum_dateTimePicker.Value = DateTime.Today;
                Connect.Close();
            }catch(FormatException)
            {
                
                Connect.Close();
                MessageBox.Show("System don't have enough data for generating summary report","Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }

            menuForm.tableLayoutPanel2.Visible = false;

        }
    }
}
