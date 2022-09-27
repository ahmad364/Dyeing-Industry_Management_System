
namespace Industrial_Mangement_System
{
    partial class Generate_Summary_Report_Form
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Generate_Summary_Report_Form));
            this.idCard_textBox = new System.Windows.Forms.Label();
            this.name_textBox = new System.Windows.Forms.Label();
            this.employee_pic = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.designation_textBox = new System.Windows.Forms.Label();
            this.cancel_button = new System.Windows.Forms.Button();
            this.maximum_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.minimum_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.generate_salary_button = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.employee_pic)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // idCard_textBox
            // 
            this.idCard_textBox.AutoSize = true;
            this.idCard_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idCard_textBox.ForeColor = System.Drawing.Color.White;
            this.idCard_textBox.Location = new System.Drawing.Point(293, 121);
            this.idCard_textBox.Name = "idCard_textBox";
            this.idCard_textBox.Size = new System.Drawing.Size(241, 32);
            this.idCard_textBox.TabIndex = 7;
            this.idCard_textBox.Text = "35201-7588921-1";
            // 
            // name_textBox
            // 
            this.name_textBox.AutoSize = true;
            this.name_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_textBox.ForeColor = System.Drawing.Color.White;
            this.name_textBox.Location = new System.Drawing.Point(292, 15);
            this.name_textBox.Name = "name_textBox";
            this.name_textBox.Size = new System.Drawing.Size(216, 38);
            this.name_textBox.TabIndex = 5;
            this.name_textBox.Text = "Ahmad Raza";
            // 
            // employee_pic
            // 
            this.employee_pic.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.employee_pic.BackColor = System.Drawing.Color.White;
            this.employee_pic.Image = ((System.Drawing.Image)(resources.GetObject("employee_pic.Image")));
            this.employee_pic.Location = new System.Drawing.Point(6, 7);
            this.employee_pic.Name = "employee_pic";
            this.employee_pic.Size = new System.Drawing.Size(151, 130);
            this.employee_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.employee_pic.TabIndex = 0;
            this.employee_pic.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.employee_pic);
            this.panel5.Location = new System.Drawing.Point(36, 13);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(163, 143);
            this.panel5.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(241, 170);
            this.panel4.TabIndex = 8;
            // 
            // designation_textBox
            // 
            this.designation_textBox.AutoSize = true;
            this.designation_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.designation_textBox.ForeColor = System.Drawing.Color.White;
            this.designation_textBox.Location = new System.Drawing.Point(293, 57);
            this.designation_textBox.Name = "designation_textBox";
            this.designation_textBox.Size = new System.Drawing.Size(99, 32);
            this.designation_textBox.TabIndex = 6;
            this.designation_textBox.Text = "Misteri";
            // 
            // cancel_button
            // 
            this.cancel_button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cancel_button.BackColor = System.Drawing.Color.Navy;
            this.cancel_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_button.ForeColor = System.Drawing.Color.White;
            this.cancel_button.Location = new System.Drawing.Point(172, 601);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(216, 76);
            this.cancel_button.TabIndex = 26;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = false;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // maximum_dateTimePicker
            // 
            this.maximum_dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maximum_dateTimePicker.CalendarFont = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximum_dateTimePicker.CalendarForeColor = System.Drawing.Color.Navy;
            this.maximum_dateTimePicker.CalendarMonthBackground = System.Drawing.Color.Navy;
            this.maximum_dateTimePicker.CalendarTitleBackColor = System.Drawing.Color.Navy;
            this.maximum_dateTimePicker.CalendarTitleForeColor = System.Drawing.Color.White;
            this.maximum_dateTimePicker.CalendarTrailingForeColor = System.Drawing.SystemColors.ControlText;
            this.maximum_dateTimePicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.maximum_dateTimePicker.CustomFormat = "dd   MMMM   yyyy";
            this.maximum_dateTimePicker.Font = new System.Drawing.Font("Arial Unicode MS", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximum_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.maximum_dateTimePicker.Location = new System.Drawing.Point(0, 418);
            this.maximum_dateTimePicker.MaxDate = new System.DateTime(9998, 12, 1, 0, 0, 0, 0);
            this.maximum_dateTimePicker.MinDate = new System.DateTime(1753, 5, 15, 0, 0, 0, 0);
            this.maximum_dateTimePicker.Name = "maximum_dateTimePicker";
            this.maximum_dateTimePicker.Size = new System.Drawing.Size(892, 57);
            this.maximum_dateTimePicker.TabIndex = 25;
            this.maximum_dateTimePicker.Value = new System.DateTime(2021, 6, 20, 0, 0, 0, 0);
            // 
            // minimum_dateTimePicker
            // 
            this.minimum_dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minimum_dateTimePicker.CalendarFont = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimum_dateTimePicker.CalendarForeColor = System.Drawing.Color.Navy;
            this.minimum_dateTimePicker.CalendarMonthBackground = System.Drawing.Color.Navy;
            this.minimum_dateTimePicker.CalendarTitleBackColor = System.Drawing.Color.Navy;
            this.minimum_dateTimePicker.CalendarTitleForeColor = System.Drawing.Color.White;
            this.minimum_dateTimePicker.CalendarTrailingForeColor = System.Drawing.SystemColors.ControlText;
            this.minimum_dateTimePicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimum_dateTimePicker.CustomFormat = "dd   MMMM   yyyy";
            this.minimum_dateTimePicker.Font = new System.Drawing.Font("Arial Unicode MS", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimum_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.minimum_dateTimePicker.Location = new System.Drawing.Point(0, 332);
            this.minimum_dateTimePicker.MaxDate = new System.DateTime(9998, 12, 1, 0, 0, 0, 0);
            this.minimum_dateTimePicker.MinDate = new System.DateTime(1753, 5, 15, 0, 0, 0, 0);
            this.minimum_dateTimePicker.Name = "minimum_dateTimePicker";
            this.minimum_dateTimePicker.Size = new System.Drawing.Size(892, 57);
            this.minimum_dateTimePicker.TabIndex = 24;
            this.minimum_dateTimePicker.Value = new System.DateTime(2021, 6, 20, 0, 0, 0, 0);
            // 
            // generate_salary_button
            // 
            this.generate_salary_button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.generate_salary_button.BackColor = System.Drawing.Color.Navy;
            this.generate_salary_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.generate_salary_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generate_salary_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generate_salary_button.ForeColor = System.Drawing.Color.White;
            this.generate_salary_button.Location = new System.Drawing.Point(498, 601);
            this.generate_salary_button.Name = "generate_salary_button";
            this.generate_salary_button.Size = new System.Drawing.Size(216, 76);
            this.generate_salary_button.TabIndex = 4;
            this.generate_salary_button.Text = "Generate";
            this.generate_salary_button.UseVisualStyleBackColor = false;
            this.generate_salary_button.Click += new System.EventHandler(this.generate_salary_button_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Navy;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.idCard_textBox);
            this.panel3.Controls.Add(this.designation_textBox);
            this.panel3.Controls.Add(this.name_textBox);
            this.panel3.Location = new System.Drawing.Point(0, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(948, 172);
            this.panel3.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(1247, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 60);
            this.button2.TabIndex = 4;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(1363, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 60);
            this.button3.TabIndex = 5;
            this.button3.Text = "X";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button4.Location = new System.Drawing.Point(4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(204, 60);
            this.button4.TabIndex = 6;
            this.button4.Text = "         Back";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(539, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 32);
            this.label1.TabIndex = 30;
            this.label1.Text = "Generate Summary Report";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cancel_button);
            this.panel1.Controls.Add(this.maximum_dateTimePicker);
            this.panel1.Controls.Add(this.minimum_dateTimePicker);
            this.panel1.Controls.Add(this.generate_salary_button);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(297, 149);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(893, 767);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.MidnightBlue;
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.button3, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.button4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.ForeColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1481, 68);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // Generate_Summary_Report_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1481, 922);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Generate_Summary_Report_Form";
            this.ShowInTaskbar = false;
            this.Text = "Generate_Summary_Report_Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Generate_Summary_Report_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.employee_pic)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label idCard_textBox;
        private System.Windows.Forms.Label name_textBox;
        private System.Windows.Forms.PictureBox employee_pic;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label designation_textBox;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.DateTimePicker maximum_dateTimePicker;
        private System.Windows.Forms.DateTimePicker minimum_dateTimePicker;
        private System.Windows.Forms.Button generate_salary_button;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}