
namespace Industrial_Mangement_System
{
    partial class Shopers_UserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shopers_UserControl));
            this.employee_designation = new System.Windows.Forms.Label();
            this.employee_name = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.employee_idCard = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.employee_pic = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employee_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // employee_designation
            // 
            this.employee_designation.AutoSize = true;
            this.employee_designation.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employee_designation.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.employee_designation.Location = new System.Drawing.Point(278, 50);
            this.employee_designation.Name = "employee_designation";
            this.employee_designation.Size = new System.Drawing.Size(92, 29);
            this.employee_designation.TabIndex = 6;
            this.employee_designation.Text = "Shoper";
            // 
            // employee_name
            // 
            this.employee_name.AutoSize = true;
            this.employee_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employee_name.Location = new System.Drawing.Point(277, 12);
            this.employee_name.Name = "employee_name";
            this.employee_name.Size = new System.Drawing.Size(196, 36);
            this.employee_name.TabIndex = 5;
            this.employee_name.Text = "Ahmad Raza";
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
            // employee_idCard
            // 
            this.employee_idCard.AutoSize = true;
            this.employee_idCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employee_idCard.Location = new System.Drawing.Point(278, 97);
            this.employee_idCard.Name = "employee_idCard";
            this.employee_idCard.Size = new System.Drawing.Size(198, 29);
            this.employee_idCard.TabIndex = 7;
            this.employee_idCard.Text = "35201-7588921-1";
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.employee_pic);
            this.panel5.Location = new System.Drawing.Point(51, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(136, 120);
            this.panel5.TabIndex = 5;
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
            // Shopers_UserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.employee_designation);
            this.Controls.Add(this.employee_name);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.employee_idCard);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Shopers_UserControl";
            this.Size = new System.Drawing.Size(1562, 146);
            this.Load += new System.EventHandler(this.Shopers_UserControl_Load);
            this.Click += new System.EventHandler(this.Shopers_UserControl_Click);
            this.MouseEnter += new System.EventHandler(this.Shopers_UserControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Shopers_UserControl_MouseLeave);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employee_pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label employee_designation;
        private System.Windows.Forms.Label employee_name;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label employee_idCard;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox employee_pic;
    }
}
