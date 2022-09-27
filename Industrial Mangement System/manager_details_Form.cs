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

    public partial class manager_details_Form : Form
    {

        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        MenuForm menuForm;

        bool call_from_menu=false;

        string imgLoc = "";
        public manager_details_Form()
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

        }
        public void  set_object_of_menu_form(MenuForm obj)
        {
            menuForm = obj;
        }
        public void call_frommenu_for_starting_system(bool f)
        {
            call_from_menu = f;
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
            if (call_from_menu == true)
                this.ShowInTaskbar = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(call_from_menu == false)
            this.Close();
        }

        private void manager_details_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;
            // show manager image
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

            if (call_from_menu==true)
            MessageBox.Show("Enter you details before starting Dyeining Industery Management System","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);

            load_manager_data();
            menuForm.tableLayoutPanel2.Visible = false;
        }
        private void load_manager_data()
        {
            try
            {

                Connect.Open();
                comands.CommandText = "select case when exists(select top 1* from Manager )then cast (1 as bit) else cast(0 as bit) end";
                if (comands.ExecuteScalar().Equals(true))
                {
                    comands.CommandText = "select *from Manager";
                    SqlDataReader reader = comands.ExecuteReader();
                    reader.Read();
                    if(reader.HasRows)
                    {
                        Cnic_textBox.Text = reader[0].ToString();
                        name_textBox.Text = reader[1].ToString();
                        father_name_textBox.Text = reader[2].ToString();
                        ph_number_textBox.Text = reader[3].ToString();
                        Designation_textBox.Text = reader[4].ToString();
                        joining_dateTimePicker.Value = Convert.ToDateTime(reader[5]);
                        monthly_salary_textBox.Text = reader[6].ToString();
                        daily_salary_textBox.Text = reader[7].ToString();
                        address_textBox.Text = reader[8].ToString();
                        details_textBox.Text = reader[9].ToString();
                    }

                    update_button.Visible = true;
                }
                Connect.Close();
            }catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }
        private void insert_manager_image_into_database_table()
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
                comands.CommandText = "update Manager set Image=@img ";
                comands.Parameters.Add(new SqlParameter("@img", img));
                comands.ExecuteNonQuery();
                Connect.Close();
            }
            catch (Exception) { Connect.Close(); }
        }
        private void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(name_textBox.Text) || String.IsNullOrWhiteSpace(father_name_textBox.Text) || String.IsNullOrWhiteSpace(Cnic_textBox.Text))
                    MessageBox.Show("You are missing some information of Employee kindly fill it", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {

                    Connect.Open();
                    comands.CommandText = "insert into Manager(CNIC,Name,Father_Name,Phone_Number,Designation,Joining_Date,Monthly_Salary,Daily_Salary,Addresss,Details,Avaliable_Income_Rupees) values('" + Cnic_textBox.Text + "','" + name_textBox.Text + "','" + father_name_textBox.Text + "','" + ph_number_textBox.Text + "','" + Designation_textBox.Text + "','" + joining_dateTimePicker.Text + "','" + Convert.ToInt64(monthly_salary_textBox.Text) + "','" + float.Parse(daily_salary_textBox.Text) + "','" + address_textBox.Text + "','" + details_textBox.Text + "','0')";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    insert_manager_image_into_database_table();
                    menuForm.Manager_Name_label.Text = name_textBox.Text;

                    MessageBox.Show("Manager record has been saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (call_from_menu == true)
                        this.Close();
                    else
                        update_button.Visible = true;
                }
            }catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void update_manager_image()
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
                comands.CommandText = "update Manager set Image=@img ";
                comands.Parameters.Add(new SqlParameter("@img", img));
                comands.ExecuteNonQuery();
                Connect.Close();

            }
            catch (Exception)
            {
                Connect.Close();
            }
        }
        private void update_button_Click(object sender, EventArgs e)
        {
            try
            {
                // updating manager image it will be seperated for escaping from exception
                update_manager_image();

                Connect.Open();
                comands.CommandText = "update Manager set CNIC='" + Cnic_textBox.Text + "', Name='" + name_textBox.Text + "',Father_Name='" + father_name_textBox.Text + "',Phone_Number='" + ph_number_textBox.Text + "',Designation='" + Designation_textBox.Text + "',Joining_Date='" + joining_dateTimePicker.Text + "',Monthly_Salary='" + Convert.ToInt64(monthly_salary_textBox.Text) + "',Daily_Salary='" +float.Parse (daily_salary_textBox.Text)+"',Addresss='"+address_textBox.Text+"',Details='"+details_textBox.Text+"'";
                comands.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show("Manager record has been saved successfully","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);

                // changing manager name in the menu form
                menuForm.Manager_Name_label.Text = name_textBox.Text;

            }catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void monthly_salary_textBox_TextChanged(object sender, EventArgs e)
        {
            float daily_salary;
            // calculating the hourly and daily salary
            try
            {
                daily_salary = Convert.ToInt32(monthly_salary_textBox.Text);
                daily_salary = daily_salary / 30;
                daily_salary_textBox.Text = daily_salary.ToString();
            }
            catch (Exception)
            {
                monthly_salary_textBox.Text = "0";
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            if(call_from_menu==false)
            this.Close();
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {


        }

        private void label2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files(*.*)|*.*";
            dlg.Title = "Select Manager Image";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBox1.ImageLocation = imgLoc;
                pictureBox1.BackColor = Color.White;
            }
        }
    }
}
