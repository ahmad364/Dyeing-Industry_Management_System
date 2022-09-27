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
    public partial class daily_expence_items_Form : Form
    {

        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        daily_expence_Form daily_Expence_Form;
        Int64 manager_income_rupees = 0;
        public daily_expence_items_Form(daily_expence_Form obj)
        {
            InitializeComponent();


            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            daily_Expence_Form = obj;
        }



        private void daily_expence_items_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;
            name__textBox.Text = daily_Expence_Form.manager_name;
            dateTimePicker.Value = DateTime.Today;
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
                    // jut for escaping from null exception
                    comands.CommandText = "select case when exists(select top 1* from Manager)then cast (1 as bit) else cast(0 as bit) end";
                    if (comands.ExecuteScalar().Equals(true))
                    {
                        comands.CommandText = "select Avaliable_Income_Rupees from Manager";
                        manager_income_rupees = Convert.ToInt64(comands.ExecuteScalar());
                    }
                    if (manager_income_rupees - Convert.ToInt64(rupees_textBox.Text) < 0)
                    {
                        MessageBox.Show("You can't do expence now because your income rupees is not enough for expence", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        comands.CommandText = "insert into ManagerDailyExpence(Manager_Name,Date,Expence_Rupees,Expence_Details) values('" + daily_Expence_Form.manager_name + "','" + dateTimePicker.Text + "','" + Convert.ToInt64(rupees_textBox.Text) + "','" + details_textBox.Text + "')";
                        comands.ExecuteNonQuery();
                        // minus manage income rupees with the expence rupees
                        comands.CommandText = "update Manager set Avaliable_Income_Rupees='" + (manager_income_rupees - Convert.ToInt64(rupees_textBox.Text)) + "'";
                        comands.ExecuteNonQuery();
                        Connect.Close();
                        MessageBox.Show("Expence Details has been saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        daily_Expence_Form.clear_grid_view();
                        daily_Expence_Form.calculate_total_expence_rupees();
                        daily_Expence_Form.populate_expence_data_to_gridview();
                    }
                    this.Close();
                }

                }catch (Exception exc)
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
            }
            catch (Exception )
            {
                rupees_textBox.Text = "0";
            }
        }
    }
}
