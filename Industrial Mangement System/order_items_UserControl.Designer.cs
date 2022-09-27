
namespace Industrial_Mangement_System
{
    partial class order_items_UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(order_items_UserControl));
            this.order_status_label = new System.Windows.Forms.Label();
            this.client_name_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.client_idCard_label = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.today_date_label = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.employee_pic = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employee_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // order_status_label
            // 
            this.order_status_label.AutoSize = true;
            this.order_status_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.order_status_label.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.order_status_label.Location = new System.Drawing.Point(283, 47);
            this.order_status_label.Name = "order_status_label";
            this.order_status_label.Size = new System.Drawing.Size(148, 29);
            this.order_status_label.TabIndex = 6;
            this.order_status_label.Text = "Ahmad Raza";
            // 
            // client_name_label
            // 
            this.client_name_label.AutoSize = true;
            this.client_name_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.client_name_label.Location = new System.Drawing.Point(282, 12);
            this.client_name_label.Name = "client_name_label";
            this.client_name_label.Size = new System.Drawing.Size(298, 32);
            this.client_name_label.TabIndex = 5;
            this.client_name_label.Text = "Per Plastic Company";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 146);
            this.panel1.TabIndex = 8;
            // 
            // client_idCard_label
            // 
            this.client_idCard_label.AutoSize = true;
            this.client_idCard_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.client_idCard_label.Location = new System.Drawing.Point(283, 94);
            this.client_idCard_label.Name = "client_idCard_label";
            this.client_idCard_label.Size = new System.Drawing.Size(198, 29);
            this.client_idCard_label.TabIndex = 7;
            this.client_idCard_label.Text = "35201-7588921-1";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoEllipsis = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1356, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 32);
            this.label9.TabIndex = 81;
            this.label9.Text = "Date";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // today_date_label
            // 
            this.today_date_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.today_date_label.AutoEllipsis = true;
            this.today_date_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.today_date_label.Location = new System.Drawing.Point(1225, 56);
            this.today_date_label.Name = "today_date_label";
            this.today_date_label.Size = new System.Drawing.Size(304, 49);
            this.today_date_label.TabIndex = 82;
            this.today_date_label.Text = "9   Jun   2021";
            this.today_date_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.employee_pic);
            this.panel5.Location = new System.Drawing.Point(51, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(136, 120);
            this.panel5.TabIndex = 4;
            // 
            // employee_pic
            // 
            this.employee_pic.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.employee_pic.BackColor = System.Drawing.Color.White;
            this.employee_pic.Image = ((System.Drawing.Image)(resources.GetObject("employee_pic.Image")));
            this.employee_pic.Location = new System.Drawing.Point(3, 6);
            this.employee_pic.Name = "employee_pic";
            this.employee_pic.Size = new System.Drawing.Size(130, 108);
            this.employee_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.employee_pic.TabIndex = 0;
            this.employee_pic.TabStop = false;
            // 
            // order_items_UserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.today_date_label);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.order_status_label);
            this.Controls.Add(this.client_name_label);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.client_idCard_label);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "order_items_UserControl";
            this.Size = new System.Drawing.Size(1562, 146);
            this.Load += new System.EventHandler(this.order_items_UserControl_Load);
            this.Click += new System.EventHandler(this.order_items_UserControl_Click);
            this.MouseEnter += new System.EventHandler(this.order_items_UserControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.order_items_UserControl_MouseLeave);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employee_pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label order_status_label;
        private System.Windows.Forms.Label client_name_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label client_idCard_label;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label today_date_label;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox employee_pic;
    }
}
