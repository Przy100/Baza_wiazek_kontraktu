﻿namespace Baza_wiazek_przyciskow_20240205
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
            label1_version = new Label();
            pictureBox2 = new PictureBox();
            progressBar1 = new ProgressBar();
            dataGridView1 = new DataGridView();
            hyperlinkBindingSource = new BindingSource(components);
            File_Name_LW = new Label();
            Author = new Label();
            recentFilesToolStripMenuItem = new MenuStrip();
            RecentFiles = new ToolStripMenuItem();
            opcjeToolStripMenuItem = new ToolStripMenuItem();
            ącyPlikToolStripMenuItem = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hyperlinkBindingSource).BeginInit();
            recentFilesToolStripMenuItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1_LW
            // 
            button1_LW.BackColor = Color.FromArgb(76, 75, 105);
            button1_LW.FlatAppearance.BorderColor = Color.FromArgb(192, 192, 255);
            button1_LW.FlatStyle = FlatStyle.Flat;
            button1_LW.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button1_LW.ForeColor = SystemColors.InactiveCaption;
            button1_LW.Location = new Point(15, 65);
            button1_LW.Margin = new Padding(6);
            button1_LW.Name = "button1_LW";
            button1_LW.Size = new Size(400, 45);
            button1_LW.TabIndex = 0;
            button1_LW.Text = "Wskaż listę wiązek kontraktu";
            button1_LW.UseVisualStyleBackColor = false;
            button1_LW.Click += button1_LW_Click;
            // 
            // label1_version
            // 
            label1_version.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1_version.AutoSize = true;
            label1_version.BackColor = Color.FromArgb(76, 75, 105);
            label1_version.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1_version.ForeColor = SystemColors.Control;
            label1_version.Location = new Point(1552, 102);
            label1_version.Margin = new Padding(6, 0, 6, 0);
            label1_version.Name = "label1_version";
            label1_version.Size = new Size(94, 34);
            label1_version.TabIndex = 2;
            label1_version.Text = "v2.1.0";
            label1_version.Click += label1_version_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(76, 75, 105);
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(427, 46);
            pictureBox2.Margin = new Padding(6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(96, 96);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(535, 113);
            progressBar1.Margin = new Padding(6);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(1018, 23);
            progressBar1.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.BackgroundColor = Color.FromArgb(44, 43, 60);
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 147);
            dataGridView1.Margin = new Padding(6);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.Size = new Size(1646, 591);
            dataGridView1.TabIndex = 5;
            dataGridView1.Tag = "";
            // 
            // File_Name_LW
            // 
            File_Name_LW.AutoSize = true;
            File_Name_LW.BackColor = Color.FromArgb(76, 75, 105);
            File_Name_LW.Font = new Font("Century Gothic", 13.125F, FontStyle.Bold, GraphicsUnit.Point, 238);
            File_Name_LW.ForeColor = SystemColors.InactiveCaption;
            File_Name_LW.Location = new Point(535, 65);
            File_Name_LW.Name = "File_Name_LW";
            File_Name_LW.Size = new Size(388, 37);
            File_Name_LW.TabIndex = 6;
            File_Name_LW.Text = "Nazwa otwartego pliku...\r\n";
            File_Name_LW.Visible = false;
            // 
            // Author
            // 
            Author.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Author.AutoSize = true;
            Author.BackColor = Color.FromArgb(76, 75, 105);
            Author.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Author.ForeColor = SystemColors.Control;
            Author.Location = new Point(1369, 46);
            Author.Name = "Author";
            Author.Size = new Size(365, 24);
            Author.TabIndex = 7;
            Author.Text = "Created by Szymon Wojciechowski\r\n";
            // 
            // recentFilesToolStripMenuItem
            // 
            recentFilesToolStripMenuItem.BackColor = Color.FromArgb(76, 75, 105);
            recentFilesToolStripMenuItem.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            recentFilesToolStripMenuItem.GripStyle = ToolStripGripStyle.Visible;
            recentFilesToolStripMenuItem.ImageScalingSize = new Size(32, 32);
            recentFilesToolStripMenuItem.Items.AddRange(new ToolStripItem[] { RecentFiles, opcjeToolStripMenuItem });
            recentFilesToolStripMenuItem.Location = new Point(0, 0);
            recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            recentFilesToolStripMenuItem.Size = new Size(1646, 40);
            recentFilesToolStripMenuItem.TabIndex = 8;
            recentFilesToolStripMenuItem.Text = "menuStrip1";
            // 
            // RecentFiles
            // 
            RecentFiles.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            RecentFiles.ForeColor = SystemColors.InactiveCaption;
            RecentFiles.Image = (Image)resources.GetObject("RecentFiles.Image");
            RecentFiles.Name = "RecentFiles";
            RecentFiles.Size = new Size(300, 36);
            RecentFiles.Text = "Ostatnio otwierane pliki";
            // 
            // opcjeToolStripMenuItem
            // 
            opcjeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ącyPlikToolStripMenuItem });
            opcjeToolStripMenuItem.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            opcjeToolStripMenuItem.ForeColor = SystemColors.InactiveCaption;
            opcjeToolStripMenuItem.Name = "opcjeToolStripMenuItem";
            opcjeToolStripMenuItem.Size = new Size(93, 36);
            opcjeToolStripMenuItem.Text = "Opcje";
            opcjeToolStripMenuItem.Click += opcjeToolStripMenuItem_Click;
            // 
            // ącyPlikToolStripMenuItem
            // 
            ącyPlikToolStripMenuItem.Name = "ącyPlikToolStripMenuItem";
            ącyPlikToolStripMenuItem.Size = new Size(234, 40);
            ącyPlikToolStripMenuItem.Text = "Otwórz LW";
            ącyPlikToolStripMenuItem.Click += ącyPlikToolStripMenuItem_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(76, 75, 105);
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Location = new Point(0, 40);
            pictureBox1.Margin = new Padding(6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1646, 107);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.ControlLight;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1646, 738);
            Controls.Add(Author);
            Controls.Add(File_Name_LW);
            Controls.Add(dataGridView1);
            Controls.Add(progressBar1);
            Controls.Add(pictureBox2);
            Controls.Add(label1_version);
            Controls.Add(button1_LW);
            Controls.Add(pictureBox1);
            Controls.Add(recentFilesToolStripMenuItem);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            ImeMode = ImeMode.Hiragana;
            MainMenuStrip = recentFilesToolStripMenuItem;
            Margin = new Padding(6);
            Name = "Form1";
            Text = "Baza wiązek kontraktu";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)hyperlinkBindingSource).EndInit();
            recentFilesToolStripMenuItem.ResumeLayout(false);
            recentFilesToolStripMenuItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1_LW;
        private Label label1_version;
        private PictureBox pictureBox2;
        private ProgressBar progressBar1;
        private DataGridView dataGridView1;
        private BindingSource hyperlinkBindingSource;
        private Label File_Name_LW;
        private Label Author;
        private MenuStrip recentFilesToolStripMenuItem;
        private ToolStripMenuItem RecentFiles;
        private PictureBox pictureBox1;
        private ToolStripMenuItem opcjeToolStripMenuItem;
        private ToolStripMenuItem ącyPlikToolStripMenuItem;
    }
}
