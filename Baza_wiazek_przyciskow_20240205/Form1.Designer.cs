using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SiticoneNetFrameworkUI;

namespace Baza_wiazek_przyciskow_20240205
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            headerPanel = new SiticonePanel();
            recentFilesToolStripMenuItem = new MenuStrip();
            RecentFiles = new ToolStripMenuItem();
            opcjeToolStripMenuItem = new ToolStripMenuItem();
            ącyPlikToolStripMenuItem = new ToolStripMenuItem();
            labelTitle = new Label();
            pictureBox1 = new SiticonePanel();
            button1_LW = new SiticoneButton();
            pictureBox2 = new PictureBox();
            File_Name_LW = new Label();
            progressBar1 = new SiticoneHProgressBar();
            Author = new Label();
            label1_version = new Label();
            gridPanel = new SiticonePanel();
            dataGridView1 = new DataGridView();
            hyperlinkBindingSource = new BindingSource(components);
            headerPanel.SuspendLayout();
            recentFilesToolStripMenuItem.SuspendLayout();
            pictureBox1.SuspendLayout();
            ((ISupportInitialize)pictureBox2).BeginInit();
            gridPanel.SuspendLayout();
            ((ISupportInitialize)dataGridView1).BeginInit();
            ((ISupportInitialize)hyperlinkBindingSource).BeginInit();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.AcrylicTintColor = Color.FromArgb(128, 255, 255, 255);
            headerPanel.BackColor = Color.FromArgb(16, 18, 27);
            headerPanel.BorderAlignment = PenAlignment.Center;
            headerPanel.BorderDashPattern = null;
            headerPanel.BorderGradientEndColor = Color.Purple;
            headerPanel.BorderGradientStartColor = Color.Blue;
            headerPanel.BorderThickness = 2F;
            headerPanel.Controls.Add(recentFilesToolStripMenuItem);
            headerPanel.Controls.Add(labelTitle);
            headerPanel.Controls.Add(pictureBox1);
            headerPanel.CornerRadiusBottomLeft = 0F;
            headerPanel.CornerRadiusBottomRight = 0F;
            headerPanel.CornerRadiusTopLeft = 0F;
            headerPanel.CornerRadiusTopRight = 0F;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.EnableAcrylicEffect = false;
            headerPanel.EnableMicaEffect = false;
            headerPanel.EnableRippleEffect = false;
            headerPanel.FillColor = Color.FromArgb(16, 18, 27);
            headerPanel.GradientColors = new Color[]
    {
    Color.White,
    Color.LightGray,
    Color.Gray
    };
            headerPanel.GradientPositions = new float[]
    {
    0F,
    0.5F,
    1F
    };
            headerPanel.Location = new Point(16, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.PatternStyle = HatchStyle.Max;
            headerPanel.RippleAlpha = 50;
            headerPanel.RippleAlphaDecrement = 3;
            headerPanel.RippleColor = Color.FromArgb(50, 255, 255, 255);
            headerPanel.RippleMaxSize = 600F;
            headerPanel.RippleSpeed = 15F;
            headerPanel.ShowBorder = false;
            headerPanel.Size = new Size(1614, 204);
            headerPanel.TabIndex = 0;
            headerPanel.TabStop = true;
            headerPanel.TrackSystemTheme = false;
            headerPanel.UseBorderGradient = false;
            headerPanel.UseMultiGradient = false;
            headerPanel.UsePatternTexture = false;
            headerPanel.UseRadialGradient = false;
            // 
            // recentFilesToolStripMenuItem
            // 
            recentFilesToolStripMenuItem.BackColor = Color.FromArgb(16, 18, 27);
            recentFilesToolStripMenuItem.Dock = DockStyle.None;
            recentFilesToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            recentFilesToolStripMenuItem.ForeColor = Color.FromArgb(230, 235, 245);
            recentFilesToolStripMenuItem.ImageScalingSize = new Size(24, 24);
            recentFilesToolStripMenuItem.Items.AddRange(new ToolStripItem[] { RecentFiles, opcjeToolStripMenuItem });
            recentFilesToolStripMenuItem.Location = new Point(24, 46);
            recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            recentFilesToolStripMenuItem.Padding = new Padding(6, 4, 6, 4);
            recentFilesToolStripMenuItem.Size = new Size(298, 46);
            recentFilesToolStripMenuItem.TabIndex = 8;
            recentFilesToolStripMenuItem.Text = "menuStrip1";
            // 
            // RecentFiles
            // 
            RecentFiles.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 238);
            RecentFiles.ForeColor = Color.FromArgb(230, 235, 245);
            RecentFiles.Image = (Image)resources.GetObject("RecentFiles.Image");
            RecentFiles.Margin = new Padding(0, 0, 10, 0);
            RecentFiles.Name = "RecentFiles";
            RecentFiles.Padding = new Padding(12, 5, 12, 5);
            RecentFiles.Size = new Size(204, 38);
            RecentFiles.Text = "Ostatnio otwierane pliki";
            // 
            // opcjeToolStripMenuItem
            // 
            opcjeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ącyPlikToolStripMenuItem });
            opcjeToolStripMenuItem.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 238);
            opcjeToolStripMenuItem.ForeColor = Color.FromArgb(230, 235, 245);
            opcjeToolStripMenuItem.Name = "opcjeToolStripMenuItem";
            opcjeToolStripMenuItem.Padding = new Padding(12, 5, 12, 5);
            opcjeToolStripMenuItem.Size = new Size(70, 38);
            opcjeToolStripMenuItem.Text = "Opcje";
            opcjeToolStripMenuItem.Click += opcjeToolStripMenuItem_Click;
            // 
            // ącyPlikToolStripMenuItem
            // 
            ącyPlikToolStripMenuItem.BackColor = Color.FromArgb(31, 34, 48);
            ącyPlikToolStripMenuItem.ForeColor = Color.FromArgb(230, 235, 245);
            ącyPlikToolStripMenuItem.Name = "ącyPlikToolStripMenuItem";
            ącyPlikToolStripMenuItem.Size = new Size(143, 22);
            ącyPlikToolStripMenuItem.Text = "Otwórz LW";
            ącyPlikToolStripMenuItem.Click += ącyPlikToolStripMenuItem_Click;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 238);
            labelTitle.ForeColor = Color.FromArgb(242, 245, 255);
            labelTitle.Location = new Point(24, 14);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(262, 32);
            labelTitle.TabIndex = 9;
            labelTitle.Text = "Baza wiązek kontraktu";
            // 
            // pictureBox1
            // 
            pictureBox1.AcrylicTintColor = Color.FromArgb(128, 255, 255, 255);
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.FromArgb(25, 28, 40);
            pictureBox1.BorderAlignment = PenAlignment.Center;
            pictureBox1.BorderDashPattern = null;
            pictureBox1.BorderGradientEndColor = Color.FromArgb(72, 78, 108);
            pictureBox1.BorderGradientStartColor = Color.FromArgb(52, 58, 82);
            pictureBox1.BorderThickness = 2F;
            pictureBox1.Controls.Add(button1_LW);
            pictureBox1.Controls.Add(pictureBox2);
            pictureBox1.Controls.Add(File_Name_LW);
            pictureBox1.Controls.Add(progressBar1);
            pictureBox1.Controls.Add(Author);
            pictureBox1.Controls.Add(label1_version);
            pictureBox1.CornerRadiusBottomLeft = 20F;
            pictureBox1.CornerRadiusBottomRight = 20F;
            pictureBox1.CornerRadiusTopLeft = 20F;
            pictureBox1.CornerRadiusTopRight = 20F;
            pictureBox1.EnableAcrylicEffect = false;
            pictureBox1.EnableMicaEffect = false;
            pictureBox1.EnableRippleEffect = false;
            pictureBox1.FillColor = Color.FromArgb(25, 28, 40);
            pictureBox1.GradientColors = new Color[]
    {
    Color.FromArgb(25, 28, 40),
    Color.FromArgb(33, 37, 54)
    };
            pictureBox1.GradientPositions = new float[]
    {
    0F,
    1F
    };
            pictureBox1.Location = new Point(24, 95);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.PatternStyle = HatchStyle.Max;
            pictureBox1.RippleAlpha = 50;
            pictureBox1.RippleAlphaDecrement = 3;
            pictureBox1.RippleColor = Color.FromArgb(50, 255, 255, 255);
            pictureBox1.RippleMaxSize = 600F;
            pictureBox1.RippleSpeed = 15F;
            pictureBox1.ShowBorder = true;
            pictureBox1.Size = new Size(1566, 94);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = true;
            pictureBox1.TrackSystemTheme = false;
            pictureBox1.UseBorderGradient = true;
            pictureBox1.UseMultiGradient = true;
            pictureBox1.UsePatternTexture = false;
            pictureBox1.UseRadialGradient = false;
            // 
            // button1_LW
            // 
            button1_LW.AccessibleDescription = "The default button control that accept input though the mouse, touch and keyboard";
            button1_LW.AccessibleName = "Wskaż listę wiązek kontraktu";
            button1_LW.AutoSizeBasedOnText = false;
            button1_LW.BackColor = Color.Transparent;
            button1_LW.BadgeBackColor = Color.Black;
            button1_LW.BadgeFont = new Font("Segoe UI", 8F, FontStyle.Bold);
            button1_LW.BadgeValue = 0;
            button1_LW.BadgeValueForeColor = Color.White;
            button1_LW.BorderColor = Color.FromArgb(110, 124, 255);
            button1_LW.BorderWidth = 1;
            button1_LW.ButtonBackColor = Color.FromArgb(88, 101, 242);
            button1_LW.ButtonImage = null;
            button1_LW.ButtonTextLeftPadding = 0;
            button1_LW.CanBeep = true;
            button1_LW.CanGlow = false;
            button1_LW.CanShake = true;
            button1_LW.ContextMenuStripEx = null;
            button1_LW.CornerRadiusBottomLeft = 14;
            button1_LW.CornerRadiusBottomRight = 14;
            button1_LW.CornerRadiusTopLeft = 14;
            button1_LW.CornerRadiusTopRight = 14;
            button1_LW.CustomCursor = Cursors.Hand;
            button1_LW.DisabledTextColor = Color.FromArgb(150, 150, 150);
            button1_LW.EnableLongPress = false;
            button1_LW.EnablePressAnimation = true;
            button1_LW.EnableRippleEffect = true;
            button1_LW.EnableShadow = true;
            button1_LW.EnableTextWrapping = false;
            button1_LW.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button1_LW.ForeColor = Color.White;
            button1_LW.GlowColor = Color.FromArgb(100, 255, 255, 255);
            button1_LW.GlowIntensity = 100;
            button1_LW.GlowRadius = 20F;
            button1_LW.GradientBackground = true;
            button1_LW.GradientColor = Color.FromArgb(59, 130, 246);
            button1_LW.GradientMode = LinearGradientMode.Horizontal;
            button1_LW.HintText = null;
            button1_LW.HoverBackColor = Color.FromArgb(108, 121, 255);
            button1_LW.HoverFontStyle = FontStyle.Regular;
            button1_LW.HoverTextColor = Color.White;
            button1_LW.HoverTransitionDuration = 250;
            button1_LW.ImageAlign = ContentAlignment.MiddleLeft;
            button1_LW.ImagePadding = 5;
            button1_LW.ImageSize = new Size(16, 16);
            button1_LW.IsRadial = false;
            button1_LW.IsReadOnly = false;
            button1_LW.IsToggleButton = false;
            button1_LW.IsToggled = false;
            button1_LW.Location = new Point(20, 24);
            button1_LW.LongPressDurationMS = 1000;
            button1_LW.Name = "button1_LW";
            button1_LW.NormalFontStyle = FontStyle.Bold;
            button1_LW.ParticleColor = Color.FromArgb(200, 200, 200);
            button1_LW.ParticleCount = 15;
            button1_LW.PressAnimationScale = 0.97F;
            button1_LW.PressedBackColor = Color.FromArgb(67, 80, 210);
            button1_LW.PressedFontStyle = FontStyle.Regular;
            button1_LW.PressTransitionDuration = 150;
            button1_LW.ReadOnlyTextColor = Color.FromArgb(100, 100, 100);
            button1_LW.RippleColor = Color.FromArgb(80, 255, 255, 255);
            button1_LW.RippleRadiusMultiplier = 0.6F;
            button1_LW.ShadowBlur = 12;
            button1_LW.ShadowColor = Color.FromArgb(80, 0, 0, 0);
            button1_LW.ShadowOffset = new Point(2, 4);
            button1_LW.ShakeDuration = 500;
            button1_LW.ShakeIntensity = 5;
            button1_LW.Size = new Size(320, 44);
            button1_LW.TabIndex = 0;
            button1_LW.Text = "Wskaż listę wiązek kontraktu";
            button1_LW.TextAlign = ContentAlignment.MiddleCenter;
            button1_LW.TextColor = Color.White;
            button1_LW.TooltipText = "Wczytaj listę wiązek kontraktu";
            button1_LW.UseAdvancedRendering = true;
            button1_LW.UseParticles = false;
            button1_LW.Click += button1_LW_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(367, 19);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(54, 54);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // File_Name_LW
            // 
            File_Name_LW.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            File_Name_LW.AutoEllipsis = true;
            File_Name_LW.BackColor = Color.Transparent;
            File_Name_LW.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 238);
            File_Name_LW.ForeColor = Color.FromArgb(236, 240, 250);
            File_Name_LW.Location = new Point(438, 18);
            File_Name_LW.Name = "File_Name_LW";
            File_Name_LW.Size = new Size(728, 24);
            File_Name_LW.TabIndex = 6;
            File_Name_LW.Text = "Nazwa otwartego pliku...";
            File_Name_LW.Visible = false;
            // 
            // progressBar1
            // 
            progressBar1.AccessibleDescription = "This control shows the value of the horizontal progress bar.";
            progressBar1.AccessibleName = "Advanced and modern horizontal progress bar control";
            progressBar1.AccessibleRole = AccessibleRole.ProgressBar;
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.AnimationDurationMs = 450D;
            progressBar1.AnimationTimerInterval = 15;
            progressBar1.AutoLabelColor = false;
            progressBar1.BackColor = Color.Transparent;
            progressBar1.BackgroundBarColor = Color.FromArgb(50, 55, 76);
            progressBar1.BorderColor = Color.FromArgb(255, 230, 153);
            progressBar1.CanBeep = true;
            progressBar1.CanShake = true;
            progressBar1.CornerRadiusBottomLeft = 8;
            progressBar1.CornerRadiusBottomRight = 8;
            progressBar1.CornerRadiusTopLeft = 8;
            progressBar1.CornerRadiusTopRight = 8;
            progressBar1.CustomLabel = "";
            progressBar1.EnableShadow = false;
            progressBar1.EnableValueDragging = false;
            progressBar1.ErrorColor = Color.FromArgb(220, 53, 69);
            progressBar1.GradientEndColor = Color.FromArgb(34, 197, 94);
            progressBar1.GradientStartColor = Color.FromArgb(88, 101, 242);
            progressBar1.Indeterminate = false;
            progressBar1.IndeterminateBarColor = Color.FromArgb(255, 193, 7);
            progressBar1.IsReadonly = true;
            progressBar1.LabelColor = Color.White;
            progressBar1.LabelFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            progressBar1.Location = new Point(438, 55);
            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
            progressBar1.MinimumSize = new Size(50, 20);
            progressBar1.Name = "progressBar1";
            progressBar1.ReadonlyBorderColor = Color.DimGray;
            progressBar1.ReadonlyFillColor1 = Color.Gray;
            progressBar1.ReadonlyFillColor2 = Color.DarkGray;
            progressBar1.ReadonlyForeColor = Color.White;
            progressBar1.ShowFocusCue = false;
            progressBar1.ShowPercentage = false;
            progressBar1.Size = new Size(978, 20);
            progressBar1.SuccessColor = Color.FromArgb(40, 167, 69);
            progressBar1.TabIndex = 4;
            progressBar1.Value = 0;
            progressBar1.ValueOrientation = SiticoneNetFrameworkUI.Helpers.Enum.ProgressBarOrientation.Horizontal;
            progressBar1.WarningColor = Color.FromArgb(255, 193, 7);
            // 
            // Author
            // 
            Author.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Author.BackColor = Color.Transparent;
            Author.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Author.ForeColor = Color.FromArgb(158, 168, 190);
            Author.Location = new Point(1237, 16);
            Author.Name = "Author";
            Author.Size = new Size(314, 20);
            Author.TabIndex = 7;
            Author.Text = "Created by Szymon Wojciechowski";
            Author.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1_version
            // 
            label1_version.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1_version.BackColor = Color.Transparent;
            label1_version.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1_version.ForeColor = Color.FromArgb(178, 187, 210);
            label1_version.Location = new Point(1454, 55);
            label1_version.Name = "label1_version";
            label1_version.Size = new Size(97, 22);
            label1_version.TabIndex = 2;
            label1_version.Text = "v2.2.1";
            label1_version.TextAlign = ContentAlignment.MiddleRight;
            label1_version.Click += label1_version_Click;
            // 
            // gridPanel
            // 
            gridPanel.AcrylicTintColor = Color.FromArgb(128, 255, 255, 255);
            gridPanel.BackColor = Color.FromArgb(16, 18, 27);
            gridPanel.BorderAlignment = PenAlignment.Center;
            gridPanel.BorderDashPattern = null;
            gridPanel.BorderGradientEndColor = Color.FromArgb(62, 68, 96);
            gridPanel.BorderGradientStartColor = Color.FromArgb(44, 49, 69);
            gridPanel.BorderThickness = 2F;
            gridPanel.Controls.Add(dataGridView1);
            gridPanel.CornerRadiusBottomLeft = 20F;
            gridPanel.CornerRadiusBottomRight = 20F;
            gridPanel.CornerRadiusTopLeft = 20F;
            gridPanel.CornerRadiusTopRight = 20F;
            gridPanel.Dock = DockStyle.Fill;
            gridPanel.EnableAcrylicEffect = false;
            gridPanel.EnableMicaEffect = false;
            gridPanel.EnableRippleEffect = false;
            gridPanel.FillColor = Color.FromArgb(21, 24, 34);
            gridPanel.GradientColors = new Color[]
    {
    Color.White,
    Color.LightGray,
    Color.Gray
    };
            gridPanel.GradientPositions = new float[]
    {
    0F,
    0.5F,
    1F
    };
            gridPanel.Location = new Point(16, 0);
            gridPanel.Margin = new Padding(0);
            gridPanel.Name = "gridPanel";
            gridPanel.Padding = new Padding(10, 18, 10, 10);
            gridPanel.PatternStyle = HatchStyle.Max;
            gridPanel.RippleAlpha = 50;
            gridPanel.RippleAlphaDecrement = 3;
            gridPanel.RippleColor = Color.FromArgb(50, 255, 255, 255);
            gridPanel.RippleMaxSize = 600F;
            gridPanel.RippleSpeed = 15F;
            gridPanel.ShowBorder = true;
            gridPanel.Size = new Size(1614, 722);
            gridPanel.TabIndex = 11;
            gridPanel.TabStop = true;
            gridPanel.TrackSystemTheme = false;
            gridPanel.UseBorderGradient = true;
            gridPanel.UseMultiGradient = false;
            gridPanel.UsePatternTexture = false;
            gridPanel.UseRadialGradient = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(27, 30, 42);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(230, 235, 245);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(88, 101, 242);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.BackgroundColor = Color.FromArgb(16, 18, 27);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(25, 28, 40);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(245, 247, 255);
            dataGridViewCellStyle2.Padding = new Padding(8, 0, 8, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(25, 28, 40);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeight = 42;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(21, 24, 34);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 238);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.Padding = new Padding(8, 4, 8, 4);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(88, 101, 242);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.FromArgb(45, 50, 70);
            dataGridView1.Location = new Point(10, 207);
            dataGridView1.Margin = new Padding(0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(21, 24, 34);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(145, 155, 178);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(88, 101, 242);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.RowHeadersWidth = 58;
            dataGridView1.RowTemplate.Height = 36;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1594, 505);
            dataGridView1.TabIndex = 5;
            dataGridView1.Tag = "";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(16, 18, 27);
            ClientSize = new Size(1646, 738);
            Controls.Add(headerPanel);
            Controls.Add(gridPanel);
            DoubleBuffered = true;
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = recentFilesToolStripMenuItem;
            Margin = new Padding(6);
            MinimumSize = new Size(1200, 650);
            Name = "Form1";
            Padding = new Padding(16, 0, 16, 16);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Baza wiązek kontraktu";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            recentFilesToolStripMenuItem.ResumeLayout(false);
            recentFilesToolStripMenuItem.PerformLayout();
            pictureBox1.ResumeLayout(false);
            ((ISupportInitialize)pictureBox2).EndInit();
            gridPanel.ResumeLayout(false);
            ((ISupportInitialize)dataGridView1).EndInit();
            ((ISupportInitialize)hyperlinkBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SiticonePanel headerPanel;
        private SiticonePanel gridPanel;
        private Label labelTitle;
        private SiticoneButton button1_LW;
        private Label label1_version;
        private PictureBox pictureBox2;
        private SiticoneHProgressBar progressBar1;
        private DataGridView dataGridView1;
        private BindingSource hyperlinkBindingSource;
        private Label File_Name_LW;
        private Label Author;
        private MenuStrip recentFilesToolStripMenuItem;
        private ToolStripMenuItem RecentFiles;
        private SiticonePanel pictureBox1;
        private ToolStripMenuItem opcjeToolStripMenuItem;
        private ToolStripMenuItem ącyPlikToolStripMenuItem;

        private sealed class ModernMenuColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground => Color.FromArgb(25, 28, 40);
            public override Color MenuBorder => Color.FromArgb(52, 58, 82);
            public override Color MenuItemBorder => Color.FromArgb(88, 101, 242);
            public override Color MenuItemSelected => Color.FromArgb(36, 40, 58);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(36, 40, 58);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(36, 40, 58);
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(31, 34, 48);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(31, 34, 48);
            public override Color ImageMarginGradientBegin => Color.FromArgb(25, 28, 40);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(25, 28, 40);
            public override Color ImageMarginGradientEnd => Color.FromArgb(25, 28, 40);
            public override Color SeparatorDark => Color.FromArgb(52, 58, 82);
            public override Color SeparatorLight => Color.FromArgb(52, 58, 82);
        }
    }
}
