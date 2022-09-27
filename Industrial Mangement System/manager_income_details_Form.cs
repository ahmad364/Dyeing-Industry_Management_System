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
    public partial class manager_income_details_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        manager_income_details_Form manager_Income_Details_Form;

        sumary_Form Sumary_Form;

        MenuForm menuForm;

        public string manager_name;
        bool call_from_summary_form = false;
        public manager_income_details_Form()
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

        }
        public void set_object_of_menu_form(MenuForm obj)
        {
            menuForm = obj;
        }
        public void set_object_of_this_form(manager_income_details_Form obj)
        {
            manager_Income_Details_Form = obj;
        }
        public void set_object_of_sumary_form(sumary_Form obj)
        {
            Sumary_Form = obj;
        }
        public void setFalg(bool flag)
        {
            call_from_summary_form = flag;
        }
        private void populate_manager_data()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Name,CNIC,Phone_Number,Addresss from Manager";
                SqlDataReader reader = comands.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    manager_name = reader[0].ToString();
                    name_textBox.Text = reader[0].ToString();
                    cnic_textBox.Text = reader[1].ToString();
                    phone_number_textBox.Text = reader[2].ToString();
                    address_textBox.Text = reader[3].ToString();
                }
                Connect.Close();
            }catch(Exception)
            {
                Connect.Close();
            }
        }
        public void clear_grid_view()
        {
            ((DataTable)show_income_details_dataGridView.DataSource).Rows.Clear();
            show_income_details_dataGridView.Refresh();
        }
        public void populate_income_data_to_gridview()
        {
            try
            {
                Connect.Open();
                DataTable table = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select Date,Details,Manager_name as 'Manager',Recieved_From as 'Recieved From' ,Rupees from ManagerIncome order by Date",Connect);
                dataAdapter.Fill(table);
                show_income_details_dataGridView.DataSource = table;
                Connect.Close();
            }
            catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }
        public void calculate_total_income_rupees()
        {

            try
            {
                Connect.Open();

                    comands.CommandText = "select Avaliable_Income_Rupees from Manager";
                    avaliable_rupees_label.Text = comands.ExecuteScalar().ToString();

                Connect.Close();

            }catch(Exception)
            {
                Connect.Close();
            }
        }
        public void populate_income_data_according_to_the_summary(DateTime start_date,DateTime end_date)
        {
            try
            {
                Connect.Open();
                DataTable table = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select Date,Details,Manager_name as 'Manager',Recieved_From as 'Recieved From' ,Rupees from ManagerIncome where Date between'" + start_date.ToString("yyyyMMdd") + "' and'" + end_date.ToString("yyyyMMdd") + "' order by Date ", Connect);
                dataAdapter.Fill(table);
                show_income_details_dataGridView.DataSource = table;
                Connect.Close();
            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(call_from_summary_form==true)
               Sumary_Form.populpate_data();
            this.Close();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Manager_Income obj = new Manager_Income(manager_Income_Details_Form);
            obj.ShowDialog();
        }

        private void manager_income_details_Form_Load(object sender, EventArgs e)
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

            date_label.Text = DateTime.Today.ToString("dd     MMMM     yyyy");
            populate_manager_data();
            calculate_total_income_rupees();

            menuForm.tableLayoutPanel2.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to delete the record of Manager Income?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Connect.Open();
                    comands.CommandText = "delete from ManagerIncome";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("Income Details has been successfully deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if(call_from_summary_form==true)
                    Sumary_Form.manger_rece_rupees.Text = "0.00";
                    clear_grid_view();
                }
                catch (Exception)
                {
                    Connect.Close();
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(call_from_summary_form==true)
               Sumary_Form.populpate_data();
            this.Close();
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

    }
}
