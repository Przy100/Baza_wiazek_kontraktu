namespace Baza_wiazek_przyciskow_20240205
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button2_Option = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 13.125F, FontStyle.Bold);
            label1.ForeColor = SystemColors.InactiveCaption;
            label1.Location = new Point(12, 93);
            label1.Name = "label1";
            label1.Size = new Size(388, 41);
            label1.TabIndex = 0;
            label1.Text = "Ścieżka inicjalizująca:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 13.125F, FontStyle.Bold);
            label2.ForeColor = SystemColors.InactiveCaption;
            label2.Location = new Point(152, 148);
            label2.Name = "label2";
            label2.Size = new Size(248, 41);
            label2.TabIndex = 1;
            label2.Text = "Ścieżka Data:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 13.125F, FontStyle.Bold);
            label3.ForeColor = SystemColors.InactiveCaption;
            label3.Location = new Point(91, 202);
            label3.Name = "label3";
            label3.Size = new Size(309, 41);
            label3.TabIndex = 2;
            label3.Text = "Ścieżka startowa:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(406, 95);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(1078, 39);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(406, 153);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(1078, 39);
            textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(406, 207);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(1078, 39);
            textBox3.TabIndex = 5;
            // 
            // button2_Option
            // 
            button2_Option.BackColor = Color.FromArgb(76, 75, 105);
            button2_Option.FlatAppearance.BorderColor = Color.FromArgb(192, 192, 255);
            button2_Option.FlatStyle = FlatStyle.Flat;
            button2_Option.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button2_Option.ForeColor = SystemColors.InactiveCaption;
            button2_Option.Location = new Point(456, 303);
            button2_Option.Margin = new Padding(6);
            button2_Option.Name = "button2_Option";
            button2_Option.Size = new Size(488, 88);
            button2_Option.TabIndex = 6;
            button2_Option.Text = "OK";
            button2_Option.UseVisualStyleBackColor = false;
            button2_Option.Click += button2_Option_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(76, 75, 105);
            ClientSize = new Size(1496, 406);
            Controls.Add(button2_Option);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form2";
            Text = "Opcje";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button2_Option;
    }
}