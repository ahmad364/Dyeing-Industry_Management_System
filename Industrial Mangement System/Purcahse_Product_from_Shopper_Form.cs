using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Industrial_Mangement_System
{
    public partial class Purcahse_Product_from_Shopper_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Shoper_Data_Holder_Class Shoper_Data_Holder;
        string imgLoc = "";
        byte[] img = null;
        FileStream fs;
        BinaryReader br;
        public Purcahse_Product_from_Shopper_Form(Shoper_Data_Holder_Class obj)
        {
            InitializeComponent();


            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            Shoper_Data_Holder = obj;
        }
        private void raceed_image_pictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files(*.*)|*.*";
            dlg.Title = "Select Delivery Raceed Image";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                raceed_image_pictureBox.ImageLocation = imgLoc;
            }
        }

        private void Purcahse_Product_from_Shopper_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            // assign shoper data to text boxes
            shopper_name_textbox.Text = Shoper_Data_Holder.name;
            father_name_textBox.Text = Shoper_Data_Holder.father_name;
            shopper_cnic_textBox.Text = Shoper_Data_Holder.cnic;
            today_date_label.Text = DateTime.Now.ToString("dd     MMMM     yyyy     hh:mm:ss:tt");
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void purchase_button_Click(object sender, EventArgs e)
        {
            if (raceed_image_pictureBox.Image == null)
                MessageBox.Show("Add Purchasing Raceed Image", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (String.IsNullOrWhiteSpace(item_Name_textBox.Text) || String.IsNullOrWhiteSpace(item_quantitiy_textBox.Text) || Convert.ToInt64(Item_rupees_textBox.Text) <= 0)
                MessageBox.Show("Enter complete details of Purchasing Item","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            else
            {

                {
                    try
                    {
                        fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                        br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);
                        Connect.Open();
                        comands.CommandText = "insert into Purchase(Shopper_Name,Shopper_Father_Name,Shopper_CNIC,Product_Name,Product_Quantity,Product_Rupees,Purchasing_Date,Purchasing_Date2,Purchasing_Pay,Purchasing_Raceed_Image) values('" + Shoper_Data_Holder.name + "','" + Shoper_Data_Holder.father_name + "','" + Shoper_Data_Holder.cnic + "','" + item_Name_textBox.Text + "','" + item_quantitiy_textBox.Text + "','" + Convert.ToInt64(Item_rupees_textBox.Text) + "','" + DateTime.Now.ToString("dd   MMMM    yyyy    hh:mm:ss:tt") + "','" + DateTime.Now.ToString("dd MMMM yyyy") + "','" + Convert.ToInt64(pay_rupees_textBox.Text) + "',@img)";
                        comands.Parameters.Add(new SqlParameter("@img", img));
                        comands.ExecuteNonQuery();
                        comands.CommandText = "update Shoper set Total_Borrow_Rupees=Total_Borrow_Rupees+'" + Convert.ToInt64(Convert.ToInt64(Item_rupees_textBox.Text) - Convert.ToInt64(pay_rupees_textBox.Text)) + "'";
                        comands.ExecuteNonQuery();
                        Connect.Close();
                        MessageBox.Show("You have purchased item successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception exc)
                    {
                        Connect.Close();
                        MessageBox.Show(exc.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
        }

        private void Item_rupees_textBox_TextChanged(object sender, EventArgs e)
        {
            Int64 check;
            try
            {
                 check = Convert.ToInt64(Item_rupees_textBox.Text);
                item_rupees_label.Text = Item_rupees_textBox.Text;
            }catch(Exception)
            {
                Item_rupees_textBox.Text = "0";
            }
        }

        private void pay_rupees_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(Convert.ToInt64(pay_rupees_textBox.Text)>Convert.ToInt64(Item_rupees_textBox.Text))
                {
                    MessageBox.Show("Pay rupees must be less than or equal to Item Rupees","Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    pay_rupees_textBox.Text = "0";
                }
            }catch(Exception)
            {
                pay_rupees_textBox.Text = "0";
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            panel7.Location = new Point
               (
                panel2.ClientSize.Width / 2 - panel7.Size.Width / 2,
                panel2.ClientSize.Height / 2 - panel7.Size.Height / 2
               );
            panel7.Anchor = AnchorStyles.None;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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
