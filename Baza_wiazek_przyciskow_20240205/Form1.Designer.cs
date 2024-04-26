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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1_LW = new Button();
            pictureBox1 = new PictureBox();
            label1_version = new Label();
            pictureBox2 = new PictureBox();
            progressBar1 = new ProgressBar();
            dataGridView1 = new DataGridView();
            hyperlinkBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hyperlinkBindingSource).BeginInit();
            SuspendLayout();
            // 
            // button1_LW
            // 
            button1_LW.Font = new Font("Segoe UI", 12F);
            button1_LW.Location = new Point(45, 47);
            button1_LW.Margin = new Padding(6);
            button1_LW.Name = "button1_LW";
            button1_LW.Size = new Size(488, 124);
            button1_LW.TabIndex = 0;
            button1_LW.Text = "Wskaż listę wiązek kontraktu";
            button1_LW.UseVisualStyleBackColor = true;
            button1_LW.Click += button1_LW_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(192, 255, 192);
            pictureBox1.Location = new Point(19, 26);
            pictureBox1.Margin = new Padding(6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(2654, 166);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1_version
            // 
            label1_version.AutoSize = true;
            label1_version.Font = new Font("Segoe UI", 12F);
            label1_version.Location = new Point(2476, 145);
            label1_version.Margin = new Padding(6, 0, 6, 0);
            label1_version.Name = "label1_version";
            label1_version.Size = new Size(197, 45);
            label1_version.TabIndex = 2;
            label1_version.Text = "Version: 2.0v";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(192, 255, 192);
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(544, 32);
            pictureBox2.Margin = new Padding(6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(186, 160);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(741, 145);
            progressBar1.Margin = new Padding(6);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(1723, 26);
            progressBar1.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 222);
            dataGridView1.Margin = new Padding(6);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.Size = new Size(2695, 1107);
            dataGridView1.TabIndex = 5;
            // 
            // hyperlinkBindingSource
            // 
            hyperlinkBindingSource.DataSource = typeof(Source.Hyperlink);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(2695, 1329);
            Controls.Add(dataGridView1);
            Controls.Add(progressBar1);
            Controls.Add(pictureBox2);
            Controls.Add(label1_version);
            Controls.Add(button1_LW);
            Controls.Add(pictureBox1);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            ImeMode = ImeMode.Hiragana;
            Margin = new Padding(6);
            Name = "Form1";
            Text = "Baza wiązek kontraktu";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)hyperlinkBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1_LW;
        private PictureBox pictureBox1;
        private Label label1_version;
        private PictureBox pictureBox2;
        private ProgressBar progressBar1;
        private DataGridView dataGridView1;
        private BindingSource hyperlinkBindingSource;
    }
}
