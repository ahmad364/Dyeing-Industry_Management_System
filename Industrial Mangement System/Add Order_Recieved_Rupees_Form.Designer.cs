
namespace Industrial_Mangement_System
{
    partial class Add_Order_Recieved_Rupees_Form
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
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.rupees_textBox = new System.Windows.Forms.TextBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rem_rupees_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.details_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.CalendarForeColor = System.Drawing.Color.Navy;
            this.dateTimePicker.CalendarMonthBackground = System.Drawing.Color.Navy;
            this.dateTimePicker.CalendarTitleBackColor = System.Drawing.Color.Navy;
            this.dateTimePicker.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dateTimePicker.CalendarTrailingForeColor = System.Drawing.SystemColors.ControlText;
            this.dateTimePicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateTimePicker.CustomFormat = "dd   MMMM   yyyy";
            this.dateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(33, 57);
            this.dateTimePicker.MaxDate = new System.DateTime(9998, 12, 1, 0, 0, 0, 0);
            this.dateTimePicker.MinDate = new System.DateTime(1753, 5, 15, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(437, 34);
            this.dateTimePicker.TabIndex = 58;
            this.dateTimePicker.Value = new System.DateTime(2021, 6, 20, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(28, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(220, 29);
            this.label6.TabIndex = 57;
            this.label6.Text = "Received Rupees";
            // 
            // rupees_textBox
            // 
            this.rupees_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rupees_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rupees_textBox.Location = new System.Drawing.Point(34, 258);
            this.rupees_textBox.Multiline = true;
            this.rupees_textBox.Name = "rupees_textBox";
            this.rupees_textBox.Size = new System.Drawing.Size(437, 34);
            this.rupees_textBox.TabIndex = 50;
            this.rupees_textBox.TextChanged += new System.EventHandler(this.rupees_textBox_TextChanged);
            // 
            // cancel_button
            // 
            this.cancel_button.BackColor = System.Drawing.Color.Navy;
            this.cancel_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_button.ForeColor = System.Drawing.Color.White;
            this.cancel_button.Location = new System.Drawing.Point(35, 539);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(121, 49);
            this.cancel_button.TabIndex = 55;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = false;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // save_button
            // 
            this.save_button.BackColor = System.Drawing.Color.Navy;
            this.save_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.save_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_button.ForeColor = System.Drawing.Color.White;
            this.save_button.Location = new System.Drawing.Point(209, 539);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(121, 49);
            this.save_button.TabIndex = 54;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = false;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(29, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(235, 29);
            this.label4.TabIndex = 53;
            this.label4.Text = "Remaining Rupees";
            // 
            // rem_rupees_textBox
            // 
            this.rem_rupees_textBox.AcceptsTab = true;
            this.rem_rupees_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rem_rupees_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rem_rupees_textBox.Location = new System.Drawing.Point(35, 154);
            this.rem_rupees_textBox.Multiline = true;
            this.rem_rupees_textBox.Name = "rem_rupees_textBox";
            this.rem_rupees_textBox.Size = new System.Drawing.Size(437, 34);
            this.rem_rupees_textBox.TabIndex = 52;
            this.rem_rupees_textBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(28, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 29);
            this.label1.TabIndex = 51;
            this.label1.Text = "Details";
            // 
            // details_textBox
            // 
            this.details_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.details_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.details_textBox.Location = new System.Drawing.Point(34, 357);
            this.details_textBox.Multiline = true;
            this.details_textBox.Name = "details_textBox";
            this.details_textBox.Size = new System.Drawing.Size(437, 127);
            this.details_textBox.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(28, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 29);
            this.label2.TabIndex = 49;
            this.label2.Text = "Date";
            // 
            // Add_Order_Recieved_Rupees_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 676);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rupees_textBox);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rem_rupees_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.details_textBox);
            this.Controls.Add(this.label2);
            this.MinimumSize = new System.Drawing.Size(657, 705);
            this.Name = "Add_Order_Recieved_Rupees_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Order Recieved Rupees";
            this.Load += new System.EventHandler(this.Add_Order_Recieved_Rupees_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox rupees_textBox;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rem_rupees_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox details_textBox;
        private System.Windows.Forms.Label label2;
    }
}