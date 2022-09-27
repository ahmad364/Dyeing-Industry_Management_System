
namespace Industrial_Mangement_System
{
    partial class Reciev_Order_Item_Rupees_Form
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
            this.cancel_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.entered_rupees_textBox = new System.Windows.Forms.TextBox();
            this.total_item_rupees_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cancel_button
            // 
            this.cancel_button.BackColor = System.Drawing.Color.Navy;
            this.cancel_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_button.ForeColor = System.Drawing.Color.White;
            this.cancel_button.Location = new System.Drawing.Point(533, 250);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(121, 49);
            this.cancel_button.TabIndex = 13;
            this.cancel_button.Text = "Back";
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
            this.save_button.Location = new System.Drawing.Point(707, 250);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(121, 49);
            this.save_button.TabIndex = 15;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = false;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(49, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(319, 32);
            this.label3.TabIndex = 11;
            this.label3.Text = "Enter Delivery Rupees";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(49, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(358, 32);
            this.label2.TabIndex = 10;
            this.label2.Text = "Order Remaining Rupees";
            // 
            // entered_rupees_textBox
            // 
            this.entered_rupees_textBox.AcceptsTab = true;
            this.entered_rupees_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entered_rupees_textBox.Location = new System.Drawing.Point(466, 163);
            this.entered_rupees_textBox.Multiline = true;
            this.entered_rupees_textBox.Name = "entered_rupees_textBox";
            this.entered_rupees_textBox.Size = new System.Drawing.Size(362, 35);
            this.entered_rupees_textBox.TabIndex = 13;
            this.entered_rupees_textBox.Text = "0";
            this.entered_rupees_textBox.TextChanged += new System.EventHandler(this.entered_rupees_textBox_TextChanged_1);
            // 
            // total_item_rupees_textBox
            // 
            this.total_item_rupees_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total_item_rupees_textBox.Location = new System.Drawing.Point(466, 66);
            this.total_item_rupees_textBox.Multiline = true;
            this.total_item_rupees_textBox.Name = "total_item_rupees_textBox";
            this.total_item_rupees_textBox.Size = new System.Drawing.Size(362, 35);
            this.total_item_rupees_textBox.TabIndex = 17;
            // 
            // Reciev_Order_Item_Rupees_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 386);
            this.ControlBox = false;
            this.Controls.Add(this.total_item_rupees_textBox);
            this.Controls.Add(this.entered_rupees_textBox);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Reciev_Order_Item_Rupees_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pay Delivery Rupees";
            this.Load += new System.EventHandler(this.Reciev_Order_Item_Rupees_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox entered_rupees_textBox;
        private System.Windows.Forms.TextBox total_item_rupees_textBox;
    }
}