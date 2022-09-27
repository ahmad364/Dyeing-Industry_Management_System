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
    public partial class Manager_Income : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        manager_income_details_Form manager_Income_Details_Form;
        public Manager_Income(manager_income_details_Form obj)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            manager_Income_Details_Form = obj;
        }
        private void Manager_Income_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;
            dateTimePicker.Value = DateTime.Today.Date;
        }

        private void save_button_Click(object sender, EventArgs e)
        {

                try
                {
                if (Convert.ToInt32(rupees_textBox.Text) == 0)
                    MessageBox.Show("You didn't enter rupees ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    Connect.Open();
                    comands.CommandText = "insert into ManagerIncome(Manager_name,Date,Recieved_From,Details,Rupees) values('" + manager_Income_Details_Form.manager_name + "','" + dateTimePicker.Text + "','" + recieved_from__textBox.Text + "','" + details_textBox.Text + "','" + Convert.ToInt64(rupees_textBox.Text) + "')";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "update Manager set Avaliable_Income_Rupees=Avaliable_Income_Rupees+'" + Convert.ToInt64(rupees_textBox.Text) + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();
                    MessageBox.Show("Income Details has been saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    manager_Income_Details_Form.calculate_total_income_rupees();
                    manager_Income_Details_Form.clear_grid_view();
                    manager_Income_Details_Form.populate_income_data_to_gridview();
                    this.Close();
                }

                }
                catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rupees_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(rupees_textBox.Text) < 0)
                    rupees_textBox.Text = "0";
            }catch(Exception)
            {
                rupees_textBox.Text = "0";
            }
        }
    }
}
