namespace Baza_wiazek_przyciskow_20240205
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1_LW = new Button();
            pictureBox1 = new PictureBox();
            label1_version = new Label();
            pictureBox2 = new PictureBox();
            progressBar1 = new ProgressBar();
            linkLabel1 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // button1_LW
            // 
            button1_LW.Font = new Font("Segoe UI", 12F);
            button1_LW.Location = new Point(24, 93);
            button1_LW.Name = "button1_LW";
            button1_LW.Size = new Size(263, 58);
            button1_LW.TabIndex = 0;
            button1_LW.Text = "Wskaż listę wiązek kontraktu";
            button1_LW.UseVisualStyleBackColor = true;
            button1_LW.Click += button1_LW_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(192, 255, 192);
            pictureBox1.Location = new Point(10, 74);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(291, 479);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1_version
            // 
            label1_version.AutoSize = true;
            label1_version.Font = new Font("Segoe UI", 12F);
            label1_version.Location = new Point(24, 514);
            label1_version.Name = "label1_version";
            label1_version.Size = new Size(98, 21);
            label1_version.TabIndex = 2;
            label1_version.Text = "Version: 2.0v";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(192, 255, 192);
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(102, 157);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(100, 100);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(24, 263);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(263, 23);
            progressBar1.TabIndex = 4;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 10F);
            linkLabel1.Location = new Point(307, 74);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(70, 19);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "linkLabel1";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Font = new Font("Segoe UI", 10F);
            linkLabel2.Location = new Point(307, 93);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(70, 19);
            linkLabel2.TabIndex = 6;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "linkLabel2";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Font = new Font("Segoe UI", 10F);
            linkLabel3.Location = new Point(307, 112);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(70, 19);
            linkLabel3.TabIndex = 7;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "linkLabel3";
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(962, 630);
            Controls.Add(linkLabel3);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(progressBar1);
            Controls.Add(pictureBox2);
            Controls.Add(label1_version);
            Controls.Add(button1_LW);
            Controls.Add(pictureBox1);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            ImeMode = ImeMode.Hiragana;
            Name = "Form1";
            Text = "Baza wiązek kontraktu";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1_LW;
        private PictureBox pictureBox1;
        private Label label1_version;
        private PictureBox pictureBox2;
        private ProgressBar progressBar1;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
    }
}
