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
    public partial class Order_Raceed_Pic_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands;

        string imgLoc = "";
    
        Recieved_Order_Form Recieved_Order_form;

        string order_id = "";
        public Order_Raceed_Pic_Form()
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);

        }
        public void set_object_of_received_order_fom(Recieved_Order_Form obj)
        {
            Recieved_Order_form = obj;
        }
        public void set_order_id(string id)
        {
            order_id = id;
        }
        private void Order_Raceed_Pic_Form_Load(object sender, EventArgs e)
        {
            Load_Image();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void back_button_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files(*.*)|*.*";
            dlg.Title = "Select Order Raceed Image";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBox.ImageLocation = imgLoc;
                pictureBox.BackColor = Color.White;
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                string sql = "insert into Order_Raceed_Image_Table(Order_Id,Order_Raceed_Image) values('" + order_id + "',@img)";
                if (Connect.State != ConnectionState.Open)
                    Connect.Open();
                comands = new SqlCommand(sql, Connect);
                comands.Parameters.Add(new SqlParameter("@img", img));
                int x = comands.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show("Image has been saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Recieved_Order_form.set_image_save(true);
                Recieved_Order_form.order_save = false;
                update_button.Visible = true;
                save_button.Visible = false;
            }catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
            
        }
        private void Load_Image()
        {
            try
            {
                if (Connect.State != ConnectionState.Open)
                    Connect.Open();
                string sqlComand = "select case when exists(select top 1* from Order_Raceed_Image_Table where Order_Id='"+order_id+"')then cast (1 as bit) else cast(0 as bit) end";
                comands = new SqlCommand(sqlComand,Connect);
                if (comands.ExecuteScalar().Equals(true))
                {
                    pictureBox.BackColor = Color.White;
                    update_button.Visible = true;
                    save_button.Visible = false;
                    string sql = "Select Order_Raceed_Image from Order_Raceed_Image_Table where Order_Id='" + order_id+ "'";
                    comands = new SqlCommand(sql, Connect);
                    SqlDataReader reader = comands.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        byte[] img = (byte[])(reader[0]);
                        if (img == null)
                            pictureBox.Image = null;
                        else
                        {
                            MemoryStream ms = new MemoryStream(img);
                            pictureBox.Image = Image.FromStream(ms);
                        }
                    }
                }
                Connect.Close();
            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                string sql = "update Order_Raceed_Image_Table set Order_Raceed_Image=@img where Order_Id='"+order_id+"'";

                comands.Parameters.Clear();

                if (Connect.State != ConnectionState.Open)
                    Connect.Open();
                comands = new SqlCommand(sql, Connect);
                comands.Parameters.Add(new SqlParameter("@img", img));
                int x = comands.ExecuteNonQuery();
                Connect.Close();
                MessageBox.Show("Image has been updated successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                Connect.Close();
            }
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
