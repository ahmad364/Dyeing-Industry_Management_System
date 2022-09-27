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
    public partial class MenuForm : Form
    {

        SqlConnection Connect;
        SqlCommand comands = new SqlCommand();

        public MenuForm()
        {

            InitializeComponent();

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            comands.Connection = Connect;

            try
            {

                Connect.Open();
                comands.CommandText = "select case when exists(select top 1* from Manager)then cast (1 as bit) else cast(0 as bit) end";
                if (comands.ExecuteScalar().Equals(true))
                {
                    comands.CommandText = "select Name from Manager";
                    Manager_Name_label.Text = comands.ExecuteScalar().ToString();
                }
                else
                {                  
                        button11.Cursor = Cursors.WaitCursor;
                        manager_details_Form obj = new manager_details_Form();
                        obj.call_frommenu_for_starting_system(true);
                        obj.set_object_of_menu_form(this);
                        obj.ShowDialog();
                        button11.Cursor = Cursors.Hand;
                    
                }
                Connect.Close();
            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void Close_system(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void Minimize_system(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void tittle_panel_Paint(object sender, PaintEventArgs e)
        {
            tittle_panel.Location = new Point
                  (
                  this.ClientSize.Width / 2 - tittle_panel.Size.Width / 2,
                  this.ClientSize.Height / 2 - tittle_panel.Size.Height / 2
                  );
            tittle_panel.Anchor = AnchorStyles.None;
        }


        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

   
        private void Show_onFull_screen(object sender, EventArgs e)
        {

            tableLayoutPanel2.Visible = false;

            int x = SystemInformation.WorkingArea.Width;
            int y = SystemInformation.WorkingArea.Height;

            this.WindowState = FormWindowState.Normal;
            this.Location = new Point(0, 0);
            this.Size = new Size(x, y);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Location = new Point
                  (
                  tittle_panel.ClientSize.Width / 2 - panel1.Size.Width / 2,
                  tittle_panel.ClientSize.Height / 2 - panel1.Size.Height / 2
                  );
            panel1.Anchor = AnchorStyles.None;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.Location = new Point
                 (
                 panel1.ClientSize.Width / 2 - panel2.Size.Width / 2,
                 panel1.ClientSize.Height / 2 - panel2.Size.Height / 2
                 );
            panel2.Anchor = AnchorStyles.None;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            panel3.Location = new Point
                 (
                 panel2.ClientSize.Width / 2 - panel3.Size.Width / 2,
                 panel2.ClientSize.Height / 2 - panel3.Size.Height / 2
                 );
            panel3.Anchor = AnchorStyles.None;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
   
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show("Do you want to Exit the System?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Cursor = Cursors.WaitCursor;
            attenadanceMark_form obj = new attenadanceMark_form();
            obj.set_menufrom_object(this);
            obj.set_object_of_form(obj);
            obj.ShowDialog();
            button6.Cursor = Cursors.Hand;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Cursor = Cursors.WaitCursor;
            employee_list_form obj = new employee_list_form("employee details");
            obj.set_object_of_menu_form(this);
            obj.set_obj_of_form(obj);
            obj.ShowDialog();
            button5.Cursor = Cursors.Hand;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            button1.Cursor = Cursors.WaitCursor;
            add_employee_Form obj = new add_employee_Form(this);
            obj.ShowDialog();
            button1.Cursor = Cursors.Hand;
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Cursor = Cursors.WaitCursor;
            employee_list_form obj = new employee_list_form("salary");
            obj.set_object_of_menu_form(this);
            obj.ShowDialog();
            button7.Cursor = Cursors.Hand;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Cursor = Cursors.WaitCursor;
            Recieved_Order_Form obj = new Recieved_Order_Form();
            obj.set_object_of_menu_form(this);
            obj.set_object_of_this_form(obj);
            obj.ShowDialog();
            button8.Cursor = Cursors.Hand;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Cursor = Cursors.WaitCursor;
            employee_list_form obj = new employee_list_form("orders");
            obj.set_object_of_menu_form(this);
            obj.set_obj_of_form(obj);
            obj.ShowDialog();
            button9.Cursor = Cursors.Hand;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Cursor = Cursors.WaitCursor;
            Delivered_Order_List_Form obj = new Delivered_Order_List_Form();
            obj.set_object_of_menu_form(this);
            obj.setForm_Object(obj);
            obj.populate_order_items();
            obj.ShowDialog();
            button10.Cursor = Cursors.Hand;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.Cursor = Cursors.WaitCursor;
            manager_details_Form obj = new manager_details_Form();
            obj.set_object_of_menu_form(this);
            obj.ShowDialog();
            button11.Cursor = Cursors.Hand;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Cursor = Cursors.WaitCursor;
            manager_income_details_Form obj = new manager_income_details_Form();
            obj.set_object_of_menu_form(this);
            obj.set_object_of_this_form(obj);
            obj.populate_income_data_to_gridview();
            obj.ShowDialog();
            button12.Cursor = Cursors.Hand;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Cursor = Cursors.WaitCursor;
            daily_expence_Form obj = new daily_expence_Form();
            obj.set_object_of_menu_form(this);
            obj.set_object_of_this_form(obj);
            obj.calculate_total_expence_rupees();
            obj.populate_expence_data_to_gridview();
            obj.ShowDialog();
            button13.Cursor = Cursors.Hand;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            button14.Cursor = Cursors.WaitCursor;
            Generate_Summary_Report_Form obj = new Generate_Summary_Report_Form();
            obj.set_object_of_menu_form(this);
            obj.ShowDialog();
            button14.Cursor = Cursors.Hand;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tableLayoutPanel2.Visible = true;
        }

        private void Screen_clicking(object sender, EventArgs e)
        {
            tableLayoutPanel2.Visible = false;
            
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.Red;
            button3.ForeColor = Color.White;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;
        }

        private void MenuForm_MouseEnter(object sender, EventArgs e)
        {
            tableLayoutPanel2.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {

            Industerial_Expence_Form obj = new Industerial_Expence_Form();
            obj.set_object_of_menu_form(this);
            obj.ShowDialog();
        }

        private void MenuForm_SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void MenuForm_ResizeBegin(object sender, EventArgs e)
        {
     
        }

        private void MenuForm_Resize(object sender, EventArgs e)
        {

        }
    }
}
