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
    public partial class Delivered_Items_Form : Form
    {
        SqlConnection Connect;

        SqlCommand comands = new SqlCommand();

        SqlDataReader DataReader;

        order_organizer_Class Order_Organizer;
        Deliver_Order_Form Deliver_Order_form;
        Delivered_Items_Form Delivered_Items_form;

        private bool flag = false;
        float price = 0;
        string imgLoc = "";
        int delivery_number = 0;
        float rem_height=0, rem_thaan = 0;
        public Delivered_Items_Form(order_organizer_Class obj1,Deliver_Order_Form obj2)
        {
            InitializeComponent();

            // setting database connection path

            var database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationDatabase.mdf");
            var connString = $"Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename={database};Integrated Security = True";

            Connect = new SqlConnection(connString);
            Order_Organizer = obj1;
            Deliver_Order_form = obj2;
            comands.Connection = Connect;
        }
        public void set_object_of_this_form(Delivered_Items_Form obj)
        {
            Delivered_Items_form = obj;
        }
        public void populate_items_name_to_comobox()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select Item_Name from ItemTable where Order_id='" + Order_Organizer.order_id + "'";
                DataReader = comands.ExecuteReader();
                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        string name = DataReader.GetString(0);
                        item_name_comboBox.Items.Add(name);
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
        private void Delivered_Items_Form_Load(object sender, EventArgs e)
        {
            calculat_delivery_number();
            // set delivery date 
            today_date_label.Text = DateTime.Now.ToString("dd    MMMM    yyyy");
            populate_items_name_to_comobox();

        }
        private void calculat_delivery_number()
        {
            try
            {
                Connect.Open();
                comands.CommandText = "select count(*) from DeliveryTable where Order_Id='"+Order_Organizer.order_id+"'";
                    delivery_number = Convert.ToInt32(comands.ExecuteScalar().ToString());
                    delivery_number++;
                Connect.Close();
            }catch(Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {

            this.Close();
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

        private void item_name_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(item_name_comboBox.Text,"Item Name",MessageBoxButtons.OK,MessageBoxIcon.Information);
            try
            {
                Connect.Open();
                comands.CommandText = "select Total_Thans-Delivered_Thans from ItemTable where Item_Number='" + (item_name_comboBox.SelectedIndex + 1) + "' and Order_Id='" + Order_Organizer.order_id + "'";
                rem_thaan_textBox.Text = comands.ExecuteScalar().ToString();
                rem_thaan=float.Parse(comands.ExecuteScalar().ToString());
                comands.CommandText = "select Total_Height-Delivered_height from ItemTable where Item_Number='" + (item_name_comboBox.SelectedIndex + 1) + "' and Order_Id='" + Order_Organizer.order_id + "'";
                rem_height_textBox.Text = comands.ExecuteScalar().ToString();
                rem_height = float.Parse(comands.ExecuteScalar().ToString());
                comands.CommandText = "select Total_Width from ItemTable where Item_Number='" + (item_name_comboBox.SelectedIndex + 1) + "' and Order_Id='" + Order_Organizer.order_id + "'";
                total_width_textBox.Text = comands.ExecuteScalar().ToString();
                Connect.Close();

            }
            catch (Exception exc)
            {
                Connect.Close();
                MessageBox.Show(exc.Message);
            }
        }

        private void Enter_Height_textBox_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                rem_height_textBox.Text = Convert.ToString(rem_height - float.Parse(Enter_Height_textBox.Text));

                if (float.Parse(rem_height_textBox.Text) < 0)
                {
                    MessageBox.Show("You can't enter delivery height more than remaining height", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Enter_Height_textBox.Text = "0";
                }
                
            }
            catch (Exception)
            {
                Connect.Close();
                Enter_Height_textBox.Text = "0";
            }
        }

        private void Enter_Thaan_textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rem_thaan_textBox.Text = Convert.ToString(rem_thaan - Convert.ToInt32(Enter_Thaan_textBox.Text));

                if (float.Parse(rem_thaan_textBox.Text) < 0)
                {
                    MessageBox.Show("You can't enter delivery thaan more than remaining thaan", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Enter_Thaan_textBox.Text = "0";
                }
      
            }
            catch (Exception)
            {
                Enter_Thaan_textBox.Text = "0";
            }
        }

        private void receieved_rupees_label_Click(object sender, EventArgs e)
        {
            Reciev_Order_Item_Rupees_Form obj = new Reciev_Order_Item_Rupees_Form(Order_Organizer,Deliver_Order_form,Delivered_Items_form,(item_name_comboBox.SelectedIndex +1));
            obj.ShowDialog();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        public void check_order_delivered_or_Not()
        {
            bool flag = false;
            bool flag2 = true;
            Connect.Open();
            comands.CommandText = "select *from ItemTable where Order_Id='" + Order_Organizer.order_id + "'";
            DataReader = comands.ExecuteReader();
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    if (DataReader["Delivered"].Equals("Yes"))
                        flag = true;
                    else
                        flag2 = false;
                }
            }
            Connect.Close();
            if (flag2 == true)
            {
                // check receive complete rupees of order 
                if (float.Parse(Deliver_Order_form.ReceRupees_textBox.Text) == float.Parse(Deliver_Order_form.TotalPay_textBox.Text))
                {
                    Connect.Open();
                    comands.CommandText = "update OrderTable set Delivered='Yes' where Order_Id='" + Order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    comands.CommandText = "update OrderTable set Delivery_Date=('" + DateTime.Today.Date.ToString("dd MMMM yyyy") + "') where Order_Id='" + Order_Organizer.order_id + "'";
                    comands.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("You have delivered this order successfully","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    Deliver_Order_form.Employee_List_Form.populate_order_items_according_to_the_call();
                    Deliver_Order_form.Order_form.Close();
                    Deliver_Order_form.Close();
                    this.Close();
                }
                else
                    MessageBox.Show("You have delivered the order but, order rupees did not recieve yet", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (flag2 == true)
            {
                Connect.Open();
                comands.CommandText = "update OrderTable set Delivered_All_Order_Items='Yes' where Order_Id='" + Order_Organizer.order_id + "'";
                comands.ExecuteNonQuery();
                Connect.Close();
            }
        }
        private void deliver_button_Click(object sender, EventArgs e)
        {
            if (float.Parse(Enter_Height_textBox.Text) == 0 || float.Parse(Enter_Thaan_textBox.Text) == 0)
                MessageBox.Show("Kindly enter complete delivery details", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                string deliver = "No";

                // check delivery completed or not
                try
                {
                    if ((float.Parse(rem_height_textBox.Text) == 0 && Convert.ToInt32(rem_thaan_textBox.Text) == 0))
                        deliver = "Yes";

                    Connect.Open();
                    comands.CommandText = "select Delivered from ItemTable where Item_Number='" + (item_name_comboBox.SelectedIndex + 1) + "' and Order_Id='" + Order_Organizer.order_id + "'";
                    if (comands.ExecuteScalar().ToString().Equals("Yes"))
                        MessageBox.Show("You have delivered this item first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (raceed_image_pictureBox.Image == null)
                        MessageBox.Show("Add Delivery Raceed Image first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        // updating data in deliver order form
                        Deliver_Order_form.set_rece_rupees(float.Parse(Deliver_Order_form.ReceRupees_textBox.Text) + float.Parse(delivery_pay_label.Text));

                        //converting image to binary form
                        byte[] img = null;
                        FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);

                        comands.CommandText = "insert into DeliveryTable(Delivery_Date,Item_Name,Delivered_Height,Delivered_Thaan,Delivered_Size,Delivery_Pay,Order_Id,Delivered_Height_Price,Item_Number,Delivery_Number,Measured_In,Raceed_Image) values('" + today_date_label.Text + "','" + item_name_comboBox.Text + "','" + float.Parse(Enter_Height_textBox.Text) + "','" + Convert.ToInt32(Enter_Thaan_textBox.Text) + "','" + float.Parse(Enter_Height_textBox.Text) * float.Parse(total_width_textBox.Text) + "','" + float.Parse(delivery_pay_label.Text) + "','" + Order_Organizer.order_id + "','" + price + "','" + (item_name_comboBox.SelectedIndex + 1) + "','" + delivery_number + "','" + Measure_In_textBox.Text + "',@img) ";
                        comands.Parameters.Add(new SqlParameter("@img", img));
                        comands.ExecuteNonQuery();
                        comands.CommandText = "update ItemTable set Delivered_Height=Delivered_Height+'" + float.Parse(Enter_Height_textBox.Text) + "',Delivered_Thans=Delivered_Thans+'" + Convert.ToInt32(Enter_Thaan_textBox.Text) + "',Delivered='" + deliver + "'  where Item_Number='" + (item_name_comboBox.SelectedIndex + 1) + "' and Order_Id='" + Order_Organizer.order_id + "'";
                        comands.ExecuteNonQuery();
                        comands.CommandText = "update OrderTable set Order_Recieved_Rupees='" + Convert.ToInt64(Deliver_Order_form.ReceRupees_textBox.Text) + "',Order_NetPay='" + float.Parse(Deliver_Order_form.NetPay_textBox.Text) + "' where Order_Id='" + Order_Organizer.order_id + "'";
                        comands.ExecuteNonQuery();

                        if (float.Parse(delivery_pay_label.Text) > 0)
                        {
                            // inserting data into order recieved rupees table
                            comands.CommandText = "insert into RecieveOrderRupees(Date,Details,Recieved_Rupees,Total_Order_Rupees,Remaining_Rupees,Order_Id) values('" + DateTime.Today.Date.ToString("dd MMMM yyyy") + "',' Delivery number " + delivery_number + " ka time pessy diya','" + Convert.ToInt64(delivery_pay_label.Text) + "','" + float.Parse(Deliver_Order_form.TotalPay_textBox.Text) + "','" + Convert.ToInt64(Deliver_Order_form.NetPay_textBox.Text) + "','" + Order_Organizer.order_id + "')";
                            comands.ExecuteNonQuery();
                        }

                        // updating data into order organizer class which holds order data
                        Order_Organizer.rece_rupees = Convert.ToInt64(Deliver_Order_form.ReceRupees_textBox.Text);
                        Order_Organizer.netPay = float.Parse(Deliver_Order_form.NetPay_textBox.Text);

                        MessageBox.Show("Item has been delivered successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = true;
                        Deliver_Order_form.clear_grid_view();
                        Deliver_Order_form.populate_ordered_items();
                        Connect.Close();

                        //checking order was delivered or not
                        check_order_delivered_or_Not();

                        this.Close();

                    }

                    Connect.Close();
                }
                catch (Exception exc)
                {
                    Connect.Close();
                    MessageBox.Show(exc.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
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


    }
}
