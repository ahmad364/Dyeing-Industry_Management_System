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
    public partial class Reciev_Order_Item_Rupees_Form : Form
    {
        SqlConnection Connect;

        order_organizer_Class Order_Organizer;
        Deliver_Order_Form Deliver_Order_form;
        Delivered_Items_Form Delivered_Items_form;
        int item_number = 0;
        float netPay = 0;
        public Reciev_Order_Item_Rupees_Form(order_organizer_Class obj1,Deliver_Order_Form obj2,Delivered_Items_Form obj3,int i_n)
        {
            InitializeComponent();


            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            Order_Organizer = obj1;
            Deliver_Order_form = obj2;
            Delivered_Items_form = obj3;
            item_number = i_n;
        }

        private void Reciev_Order_Item_Rupees_Form_Load(object sender, EventArgs e)
        {
            total_item_rupees_textBox.Text = Deliver_Order_form.NetPay_textBox.Text;
            netPay = float.Parse(Deliver_Order_form.NetPay_textBox.Text);


        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
           try
            {

                Delivered_Items_form.delivery_pay_label.Text = entered_rupees_textBox.Text;
                this.Close();

            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
         
        }

        private void entered_rupees_textBox_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (float.Parse(entered_rupees_textBox.Text) > netPay)
                {
                    MessageBox.Show("You can't enter delivery rupees more than order remaining rupees","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    entered_rupees_textBox.Text = "0";
                }
                else
                {
                    total_item_rupees_textBox.Text = Convert.ToString(netPay - float.Parse(entered_rupees_textBox.Text));
                }
            }
            catch (Exception)
            {
                entered_rupees_textBox.Text = "0";
            }
        }
    }
}
