
namespace Industrial_Mangement_System
{
    partial class employee_attendance_control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(employee_attendance_control));
            this.panel1 = new System.Windows.Forms.Panel();
            this.employee_idCard = new System.Windows.Forms.Label();
            this.employee_designation = new System.Windows.Forms.Label();
            this.employee_name = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.employee_pic = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employee_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 150);
            this.panel1.TabIndex = 8;
            // 
            // employee_idCard
            // 
            this.employee_idCard.AutoSize = true;
            this.employee_idCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employee_idCard.Location = new System.Drawing.Point(281, 99);
            this.employee_idCard.Name = "employee_idCard";
            this.employee_idCard.Size = new System.Drawing.Size(198, 29);
            this.employee_idCard.TabIndex = 7;
            this.employee_idCard.Text = "35201-7588921-1";
            // 
            // employee_designation
            // 
            this.employee_designation.AutoSize = true;
            this.employee_designation.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employee_designation.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.employee_designation.Location = new System.Drawing.Point(281, 52);
            this.employee_designation.Name = "employee_designation";
            this.employee_designation.Size = new System.Drawing.Size(85, 29);
            this.employee_designation.TabIndex = 6;
            this.employee_designation.Text = "Misteri";
            // 
            // employee_name
            // 
            this.employee_name.AutoSize = true;
            this.employee_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employee_name.Location = new System.Drawing.Point(280, 14);
            this.employee_name.Name = "employee_name";
            this.employee_name.Size = new System.Drawing.Size(196, 36);
            this.employee_name.TabIndex = 5;
            this.employee_name.Text = "Ahmad Raza";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1255, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.employee_pic);
            this.panel5.Location = new System.Drawing.Point(50, 15);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(136, 120);
            this.panel5.TabIndex = 3;
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
            // employee_attendance_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.employee_idCard);
            this.Controls.Add(this.employee_designation);
            this.Controls.Add(this.employee_name);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "employee_attendance_control";
            this.Size = new System.Drawing.Size(1409, 150);
            this.Load += new System.EventHandler(this.setSize_image);
            this.Click += new System.EventHandler(this.employee_attendance_control_Click);
            this.MouseEnter += new System.EventHandler(this.employee_attendance_control_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.employee_attendance_control_MouseLeave);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employee_pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label employee_idCard;
        private System.Windows.Forms.Label employee_designation;
        private System.Windows.Forms.Label employee_name;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox employee_pic;
    }
}
