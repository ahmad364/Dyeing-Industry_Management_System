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
    public partial class Shoper_List_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataReader data_reader;

        Shoper_List_Form Shoper_List_form;
        Shoper_Data_Holder_Class shoper_data_Holder;
        public Shoper_List_Form()
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void set_object_of_this_form(Shoper_List_Form obj)
        {
            Shoper_List_form = obj;
        }
        private void Shoper_List_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;
            populate_shopers_userControl();
        }
        public void populate_shopers_userControl()
        {
                shopers_LaoutPannel.Controls.Clear();

            List<Shopers_UserControl> shoper = new List<Shopers_UserControl>();
            int count = 0;

            // make the array of controls panel

            Connect.Open();

            comands.CommandText = "select *from Shoper order by Name";
            data_reader = comands.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    shoper_data_Holder = new Shoper_Data_Holder_Class(data_reader[1].ToString(), data_reader[2].ToString(), data_reader[6].ToString(), data_reader[3].ToString(), data_reader[0].ToString(), data_reader[5].ToString(), data_reader[4].ToString(), data_reader[7].ToString());
                    shoper.Add(new Shopers_UserControl(Shoper_List_form, shoper_data_Holder));
                    shopers_LaoutPannel.Controls.Add(shoper[count]);
                    count++;
                }
            }

            Connect.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

  
    }
}
