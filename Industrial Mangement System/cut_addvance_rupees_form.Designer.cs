
namespace Industrial_Mangement_System
{
    partial class cut_addvance_rupees_form
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.entered_advance_rupees_textBox = new System.Windows.Forms.TextBox();
            this.total_advance_rupees_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(45, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Addvance Rupees";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(45, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "Enter Rupees";
            // 
            // save_button
            // 
            this.save_button.BackColor = System.Drawing.Color.Navy;
            this.save_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.save_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_button.ForeColor = System.Drawing.Color.White;
            this.save_button.Location = new System.Drawing.Point(698, 241);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(121, 49);
            this.save_button.TabIndex = 7;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = false;
            this.save_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.BackColor = System.Drawing.Color.Navy;
            this.cancel_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_button.ForeColor = System.Drawing.Color.White;
            this.cancel_button.Location = new System.Drawing.Point(524, 241);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(121, 49);
            this.cancel_button.TabIndex = 6;
            this.cancel_button.Text = "Back";
            this.cancel_button.UseVisualStyleBackColor = false;
            this.cancel_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // entered_advance_rupees_textBox
            // 
            this.entered_advance_rupees_textBox.AcceptsTab = true;
            this.entered_advance_rupees_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entered_advance_rupees_textBox.Location = new System.Drawing.Point(457, 154);
            this.entered_advance_rupees_textBox.Multiline = true;
            this.entered_advance_rupees_textBox.Name = "entered_advance_rupees_textBox";
            this.entered_advance_rupees_textBox.Size = new System.Drawing.Size(362, 35);
            this.entered_advance_rupees_textBox.TabIndex = 5;
            this.entered_advance_rupees_textBox.Text = "0";
            this.entered_advance_rupees_textBox.TextChanged += new System.EventHandler(this.entered_advance_rupees_textBox_TextChanged_3);
            // 
            // total_advance_rupees_textBox
            // 
            this.total_advance_rupees_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_advance_rupees_textBox.Location = new System.Drawing.Point(457, 57);
            this.total_advance_rupees_textBox.Multiline = true;
            this.total_advance_rupees_textBox.Name = "total_advance_rupees_textBox";
            this.total_advance_rupees_textBox.Size = new System.Drawing.Size(362, 35);
            this.total_advance_rupees_textBox.TabIndex = 8;
            // 
            // cut_addvance_rupees_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(885, 361);
            this.ControlBox = false;
            this.Controls.Add(this.total_advance_rupees_textBox);
            this.Controls.Add(this.entered_advance_rupees_textBox);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cut_addvance_rupees_form";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cut Employee Advance Rupees";
            this.Load += new System.EventHandler(this.cut_addvance_rupees_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.TextBox entered_advance_rupees_textBox;
        private System.Windows.Forms.TextBox total_advance_rupees_textBox;
    }
}