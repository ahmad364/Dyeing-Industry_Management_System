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
    public partial class Industerial_Expence_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        MenuForm menuForm;

        string imgLoc = "";
        Image image;

        public Industerial_Expence_Form()
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            image = pictureBox1.Image;

        }
        public void set_object_of_menu_form(MenuForm obj)
        {
            menuForm = obj;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Industerial_Expence_Form_Load(object sender, EventArgs e)
        {
            // setting today date

            joining_dateTimePicker.Value = DateTime.Today;

            comands.Connection = Connect;

            menuForm.tableLayoutPanel2.Visible = false;
        }
        private void insert_shopper_image_into_database_table()
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
                comands.CommandText = "update Shoper set Image=@img where CNIC='" +Cnic_textBox.Text + "'";
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

                if (String.IsNullOrWhiteSpace(Cnic_textBox.Text) || String.IsNullOrWhiteSpace(name_textBox.Text) || String.IsNullOrWhiteSpace(father_name_textBox.Text))
                    MessageBox.Show("You are missing some information of Shopper Kindly fill it", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {

                    Connect.Open();
                    comands.CommandText = "insert into Shoper(CNIC,Name,Father_Name,Phone_Number,Designation,Date,Address,Details,Total_Borrow_Rupees,Return_Borrow_Rupees) values('" + Cnic_textBox.Text + "','" + name_textBox.Text + "','" + father_name_textBox.Text + "','" + ph_number_textBox.Text + "','" + Designation_textBox.Text + "','" + joining_dateTimePicker.Text + "','" + address_textBox.Text + "','" + details_textBox.Text + "','0','0')";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    insert_shopper_image_into_database_table();

                    MessageBox.Show("Shoper has been saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    name_textBox.Clear();
                    father_name_textBox.Clear();
                    Cnic_textBox.Clear();
                    ph_number_textBox.Clear();
                    address_textBox.Clear();
                    details_textBox.Clear();
                    imgLoc = "";
                    pictureBox1.Image = image;
                }
            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Cursor = Cursors.AppStarting;
            Shoper_List_Form obj = new Shoper_List_Form();
            obj.set_object_of_this_form(obj);
            obj.ShowDialog();
            button5.Cursor = Cursors.Hand;

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



        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files(*.*)|*.*";
            dlg.Title = "Select Shopper Image";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBox1.ImageLocation = imgLoc;
                pictureBox1.BackColor = Color.White;
            }
        }
    }
}
