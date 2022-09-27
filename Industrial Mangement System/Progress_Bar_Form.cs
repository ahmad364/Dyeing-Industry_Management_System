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
    public partial class Progress_Bar_Form : Form
    {
        SqlConnection Connect;

        public bool AppRunSuccessfully = true;
        bool thread_run = false;
        int increment = 2;
        public Progress_Bar_Form()
        {
            InitializeComponent();

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);
        }

        private void Progress_Bar_Form_Load(object sender, EventArgs e)
        {
            
        }

        // when we run application it takes a long time for the connecting to the dataBase that's why it is connecting during progress bar
        public void connect_to_the_dataBase()
        {
            try
            {
                if (Connect.State != ConnectionState.Open)
                {
                    Connect.Open(); // it takes a little bit long time
                    increment = 70;
                }
            }catch(Exception exc)
            {
                AppRunSuccessfully = false;
                Connect.Close();
                timer1.Stop();
                MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();

            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            panel2.Width += increment;

            if (panel2.Width >= 660 && increment==70)
            {
                Connect.Close();
                timer1.Stop();
                this.Close();
            }
            else if(thread_run==false)
            {
                
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(connect_to_the_dataBase));
                thread.Start();
        
                thread_run = true; // tell this thread running or not
            }

        }
    }
}
