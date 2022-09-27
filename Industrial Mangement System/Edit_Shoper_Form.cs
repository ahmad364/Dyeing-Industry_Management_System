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
    public partial class Edit_Shoper_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        Shoper_Data_Holder_Class Shoper_Data_Holder;
        Shoper_List_Form Shoper_List_form;
        Shoper_Details_Form Shoper_Details_form;

        string imgLoc = "";
        public Edit_Shoper_Form(Shoper_Data_Holder_Class obj1,Shoper_List_Form obj2)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

            Shoper_Data_Holder = obj1;
            Shoper_List_form = obj2;
            Shoper_Details_form = new Shoper_Details_Form(obj1,obj2);
        }

        private void Edit_Shoper_Form_Load(object sender, EventArgs e)
        {
            comands.Connection = Connect;

            Connect.Open();

            try
            {

                //   assigning emp image to picture box
                string sql = "Select Image from Shoper where CNIC='" + Shoper_Data_Holder.cnic + "'";
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


            // assigning shoper data to textbox

            name_textBox.Text = Shoper_Data_Holder.name;
            father_name_textBox.Text = Shoper_Data_Holder.father_name;
            Cnic_textBox.Text = Shoper_Data_Holder.cnic;
            ph_number_textBox.Text = Shoper_Data_Holder.phone_number;
            joining_dateTimePicker.Text = Shoper_Data_Holder.date;
            details_textBox.Text = Shoper_Data_Holder.details;
            address_textBox.Text = Shoper_Data_Holder.address;
        }
        private void update_shopper_image()
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
                comands.CommandText = "update Shoper set Image=@img where CNIC='" + Shoper_Data_Holder.cnic + "'";
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
                //updating shopper image
                update_shopper_image();

                Connect.Open();
                comands.CommandText="update Shoper set Name='"+name_textBox.Text+"',Father_Name='"+father_name_textBox.Text+"',CNIC='"+Cnic_textBox.Text+ "',Phone_Number='"+ph_number_textBox.Text+ "',Date='"+joining_dateTimePicker.Text+ "',Address='"+address_textBox.Text+ "',Details='"+details_textBox.Text+"' where CNIC='"+Shoper_Data_Holder.cnic+"'";
                comands.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show(name_textBox.Text+" has been updated","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);

                // Update data in shoper data holder class

                Shoper_Data_Holder.name = name_textBox.Text;
                Shoper_Data_Holder.father_name = father_name_textBox.Text;
                Shoper_Data_Holder.cnic = Cnic_textBox.Text;
                Shoper_Data_Holder.phone_number = ph_number_textBox.Text;
                Shoper_Data_Holder.date = joining_dateTimePicker.Text;
                Shoper_Data_Holder.details = details_textBox.Text;
                Shoper_Data_Holder.address = address_textBox.Text;

                Shoper_Details_form = new Shoper_Details_Form(Shoper_Data_Holder,Shoper_List_form);

                Shoper_List_form.populate_shopers_userControl();

            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            Shoper_Details_form.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Shoper_Details_form.ShowDialog();
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


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
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
