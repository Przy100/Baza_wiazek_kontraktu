using Baza_wiazek_przyciskow_20240205.Source;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Specialized;
using System;
using MongoDB.Driver;
using Baza_wiazek_przyciskow_20240205.Server;
using System.Data;
using System.Drawing.Drawing2D;
using ExcelDataReader;
using System.IO;
using SharpCompress.Common;
using SiticoneNetFrameworkUI;


namespace Baza_wiazek_przyciskow_20240205
{

    public partial class Form1 : Form
    {
        // Ostateczne ścieżki dostępu.
        string[] LINK;
        string LinkFromRecentFiles;
        public Form1()
        {
            InitializeComponent();
            ApplyVisualTheme();
            // Obsługa zdarzenia kliknięcia w dataGridView1.
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView_CellContentClick);
            dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);
            // Inicjalizacja RecentFiles.
            InitializeRecentFilesMenu();
            // Obsługa zdarzenia załadowania RecentFiles do zakładki "Ostatnio otw...".
            this.Load += new EventHandler(Form_Load);

        }
        private void ApplyVisualTheme()
        {
            BackColor = Color.FromArgb(16, 18, 27);
            ForeColor = Color.White;

            dataGridView1.BackgroundColor = Color.FromArgb(14, 16, 24);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor = Color.FromArgb(12, 14, 20);
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 54;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 54;
            dataGridView1.RowTemplate.DividerHeight = 6;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 28, 40);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(245, 247, 255);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(25, 28, 40);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(23, 26, 38);
            dataGridView1.RowsDefaultCellStyle.ForeColor = Color.FromArgb(230, 235, 245);
            dataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(78, 92, 216);
            dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(23, 26, 38);
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(230, 235, 245);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(78, 92, 216);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 238);
            dataGridView1.DefaultCellStyle.Padding = new Padding(12, 9, 12, 9);

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(31, 35, 49);
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(230, 235, 245);
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(78, 92, 216);
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        }
        private void InitializeDataGridView(string[] newBTE, string[] NAME, string[] IndeksySBC, string[] Ilosc, string[] Prio, string[] Status, string[] Rewizja, string[] Opis, string[] Uwagi)
        {

            // Podstawowa konfiguracja
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.ColumnCount = 9;

            // Ustawienie zawijania tekstu
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Kolumna hiperłącze
            DataGridViewButtonColumn linkColumn = new DataGridViewButtonColumn();
            linkColumn.Name = "Nazwa";
            linkColumn.UseColumnTextForButtonValue = false;
            linkColumn.FlatStyle = FlatStyle.Flat;
            linkColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            linkColumn.DefaultCellStyle.Padding = new Padding(10, 6, 10, 6);
            dataGridView1.Columns.Add(linkColumn);

            // Nazwy kolumn
            dataGridView1.Columns[0].Name = "Lp.";
            dataGridView1.Columns[0].HeaderText = "Lp.";
            dataGridView1.Columns[0].ToolTipText = "Numer porządkowy pozycji";
            dataGridView1.Columns[0].Width = 75;
            dataGridView1.Columns[1].Name = "Numer wiązki BTE";
            dataGridView1.Columns[1].HeaderText = "Numer wiązki BTE";
            dataGridView1.Columns[1].ToolTipText = "Numer identyfikacyjny wiązki lub płyty";
            dataGridView1.Columns["Nazwa"].DisplayIndex = 2;
            dataGridView1.Columns["Nazwa"].HeaderText = "Typ dokumentu";
            dataGridView1.Columns["Nazwa"].ToolTipText = "Rodzaj dokumentacji do otwarcia, np. wiązka lub płyta";
            dataGridView1.Columns["Nazwa"].MinimumWidth = 160;
            dataGridView1.Columns[2].Name = "Indeks SBC";
            dataGridView1.Columns[2].HeaderText = "Indeks SBC";
            dataGridView1.Columns[2].ToolTipText = "Numer indeksu materiałowego SBC";
            dataGridView1.Columns[3].Name = "Ilość";
            dataGridView1.Columns[3].HeaderText = "Ilość";
            dataGridView1.Columns[3].ToolTipText = "Liczba sztuk w danej pozycji";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].Name = "Priorytet";
            dataGridView1.Columns[4].HeaderText = "Priorytet";
            dataGridView1.Columns[4].ToolTipText = "Priorytet realizacji pozycji";
            dataGridView1.Columns[4].Width = 110;
            dataGridView1.Columns[5].Name = "Status";
            dataGridView1.Columns[5].HeaderText = "Status";
            dataGridView1.Columns[5].ToolTipText = "Aktualny status pozycji";
            dataGridView1.Columns[5].Width = 130;
            dataGridView1.Columns[6].Name = "Rewizja";
            dataGridView1.Columns[6].HeaderText = "Rewizja";
            dataGridView1.Columns[6].ToolTipText = "Wersja lub rewizja dokumentacji";
            dataGridView1.Columns[6].Width = 110;
            dataGridView1.Columns[7].Name = "Opis / zastosowanie";
            dataGridView1.Columns[7].HeaderText = "Opis / zastosowanie";
            dataGridView1.Columns[7].ToolTipText = "Opis pozycji i jej zastosowanie";
            dataGridView1.Columns[7].Width = 700;
            dataGridView1.Columns[8].Name = "Uwagi";
            dataGridView1.Columns[8].HeaderText = "Uwagi";
            dataGridView1.Columns[8].ToolTipText = "Dodatkowe informacje i komentarze";

            // Zmiana stylu kolumny.
            dataGridView1.Columns[1].DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dataGridView1.Columns[4].DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.FromArgb(255, 214, 133);
            dataGridView1.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(34, 37, 52);
            dataGridView1.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(28, 31, 45);
            dataGridView1.Columns[6].DefaultCellStyle.BackColor = Color.FromArgb(28, 31, 45);

            for (int i = 0; i < NAME.Length; i++)
            {
                int rowIndex = dataGridView1.Rows.Add();  // Dodaje nowy wiersz i zapisuje jego indeks

                dataGridView1.Rows[rowIndex].Cells[0].Value = i + 1; // Lp.
                dataGridView1.Rows[rowIndex].Cells[1].Value = newBTE[i];
                dataGridView1.Rows[rowIndex].Cells["Nazwa"].Value = NAME[i];
                dataGridView1.Rows[rowIndex].Cells[2].Value = IndeksySBC[i];
                dataGridView1.Rows[rowIndex].Cells[3].Value = Ilosc[i];
                dataGridView1.Rows[rowIndex].Cells[4].Value = Prio[i];
                dataGridView1.Rows[rowIndex].Cells[5].Value = Status[i];
                dataGridView1.Rows[rowIndex].Cells[6].Value = Rewizja[i];
                dataGridView1.Rows[rowIndex].Cells[7].Value = Opis[i];
                dataGridView1.Rows[rowIndex].Cells[8].Value = Uwagi[i];

            }

            ApplyVisualTheme();

        }
        private void MainProgram()
        {
            string filePath = LinkFromRecentFiles;

            this.File_Name_LW.Text = Path.GetFileName(filePath);
            File_Name_LW.Visible = true;

            // Podaj ile jest wierszy w tym pliku
            var excelReader = new ExcelReader();
            var rowCount = excelReader.GetRowCount(filePath, 5, 6);
            rowCount = rowCount;
            // Stwórz dwie tablice string o takiej wielkości
            string[] BTE = new string[rowCount];
            string[] NAME = new string[rowCount];
            BTE = excelReader.FillArray(filePath, rowCount, 2);
            NAME = excelReader.FillArray(filePath, rowCount, 3);

            progressBar1.Value = 20;
            Application.DoEvents(); // Pozwala na odświeżanie UI w trakcie pętli

            // Stwórz tablice string z ID wiązek na podstawie tablicy NAME.
            var convertData = new ConvertData();
            string[] ID = new string[rowCount];
            ID = convertData.GetLastTwoLetters(NAME);

            // Stwórz tablice string z nazwami folderów wiązek na podstawie tablicy ID.
            string[] FOLDER = new string[rowCount];
            FOLDER = convertData.FolderSelection(ID);

            progressBar1.Value = 30;
            Application.DoEvents(); // Pozwala na odświeżanie UI w trakcie pętli

            // Jeśli BTE ma dwa lub więcej numerów BTE.
            BTE = convertData.MoreThenOneBTENumber(BTE);

            // Zmienia kodowanie płyt na AAx.
            string[] newBTE = new string[rowCount];
            newBTE = convertData.CodePlate(NAME, BTE);

            // Stwórz tablice z fragmentem ścieżki dostępu.
            string[] linkName = new string[rowCount];
            linkName = convertData.LinkNameWire(FOLDER, NAME, newBTE);

            // Końcowa ścieżka dostępu.
            string[] finishPath = new string[rowCount];
            finishPath = convertData.ExcelOrZuken(linkName);
            LINK = finishPath;
            progressBar1.Value = 40;
            Application.DoEvents(); // Pozwala na odświeżanie UI w trakcie pętli

            // Pobiera kolumny z LW.
            string[] IndeksySBC = excelReader.FillArray(filePath, rowCount, 4);
            progressBar1.Value = 50;
            string[] Ilosc = excelReader.FillArray(filePath, rowCount, 5);
            progressBar1.Value = 60;
            string[] Priorytet = excelReader.FillArray(filePath, rowCount, 6);
            progressBar1.Value = 70;
            string[] Status = excelReader.FillArray(filePath, rowCount, 7);
            progressBar1.Value = 80;
            string[] Rewizja = excelReader.FillArray(filePath, rowCount, 8);
            string[] Opis = excelReader.FillArray(filePath, rowCount, 9);
            string[] Uwagi = excelReader.FillArray(filePath, rowCount, 10);
            progressBar1.Value = 90;

            // Tworzy tabelkę przypominającą tą z Excela.
            InitializeDataGridView(newBTE, NAME, IndeksySBC, Ilosc, Priorytet, Status, Rewizja, Opis, Uwagi);

            Application.DoEvents(); // Pozwala na odświeżanie UI w trakcie pętli
            progressBar1.Value = 100;
        }
        private async void button1_LW_Click(object sender, EventArgs e)
        {
            // Dodawanie użytkowników do bazy.
            // new NewUsers();
            // Koniec - Dodawanie użytkowników do bazy.

            // Sprawdzanie ostatniej werji w GitHub
            // Załóżmy, że używasz repozytorium GitHub
            string repoUrl = "https://api.github.com/repos/Przy100/Baza_wiazek_kontraktu";
            //string token = "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";

            Configurator configurator = new Configurator();
            //UpdateChecker updater = new UpdateChecker(repoUrl, configurator.Version);

            //await updater.CheckForUpdatesAsync(); // Asynchroniczne wywołanie metody
            // Koniec - Sprawdzanie ostatniej werji w GitHub

            // Wyczyść DataGridView przed utworzeniem.
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            // Progres bar.
            progressBar1.Maximum = 100;

            // Wybranie pliku Excel z listą wiązek
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                Configurator cFile = new Configurator();
                openFileDialog.InitialDirectory = cFile.InitialDirectory;
                openFileDialog.Filter = "Excel files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                progressBar1.Value = 10;
                Application.DoEvents(); // Pozwala na odświeżanie UI w trakcie pętli

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string LW_Name = openFileDialog.FileName;
                    this.File_Name_LW.Text = Path.GetFileName(LW_Name);
                    File_Name_LW.Visible = true;

                    // Pobierz ścieżkę do wybranego pliku
                    string filePath = openFileDialog.FileName;
                    // Ścieżka lokalna to tymczasowej kopii
                    string filePathTemporary = Path.Combine(Path.GetTempPath(), Path.GetFileName(filePath));
                    // Utworz kopie
                    File.Copy(filePath, filePathTemporary, true);

                    // Dodaj plik do RecentFile.
                    OpenFile(filePathTemporary);
                    // Dodaj link do zmiennej globalnej.
                    //LinkFromRecentFiles = filePath;
                    // Przejdź do funkcji głównej.
                    //MainProgram();
                }

            }

        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sprawdzenie, czy kliknięto kolumnę hiperłącza
            if (e.ColumnIndex == dataGridView1.Columns["Nazwa"].Index && e.RowIndex >= 0)
            {
                // Zwraca numer wiersza, który chcemy otworzyć.
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(LINK[e.RowIndex]) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nie można otworzyć linku: " + ex.Message);
                }
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Columns["Nazwa"] == null)
            {
                return;
            }

            if (e.ColumnIndex != dataGridView1.Columns["Nazwa"].Index)
            {
                return;
            }

            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

            string buttonText = Convert.ToString(e.FormattedValue) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(buttonText))
            {
                e.Handled = true;
                return;
            }

            (Color fill, Color border, Color textColor) = GetDocumentChipPalette(buttonText);
            if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
                fill = ControlPaint.Light(fill, 0.12F);
            }

            Rectangle buttonBounds = Rectangle.Inflate(e.CellBounds, -10, -8);
            using GraphicsPath path = CreateRoundedPath(buttonBounds, 12);
            using SolidBrush fillBrush = new SolidBrush(fill);
            using Pen borderPen = new Pen(border, 1.2F);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillPath(fillBrush, path);
            e.Graphics.DrawPath(borderPen, path);

            TextRenderer.DrawText(
                e.Graphics,
                buttonText,
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                buttonBounds,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            e.Handled = true;
        }
        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
        private static (Color fill, Color border, Color textColor) GetDocumentChipPalette(string buttonText)
        {
            if (buttonText.StartsWith("Płyta", StringComparison.OrdinalIgnoreCase))
            {
                return (Color.FromArgb(46, 87, 198), Color.FromArgb(92, 133, 255), Color.White);
            }

            if (buttonText.StartsWith("Wiązka", StringComparison.OrdinalIgnoreCase))
            {
                return (Color.FromArgb(56, 124, 95), Color.FromArgb(102, 182, 144), Color.White);
            }

            return (Color.FromArgb(114, 92, 208), Color.FromArgb(156, 137, 255), Color.White);
        }
        private void InitializeRecentFilesMenu()
        {
            // Dodanie przykładowych wpisów
            for (int i = 0; i < 5; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem($"File {i + 1}");
                StyleMenuItem(item);
                item.Click += RecentFile_Click;
                RecentFiles.DropDownItems.Add(item);
            }
        }
        private void StyleMenuItem(ToolStripMenuItem item)
        {
            item.BackColor = Color.FromArgb(25, 28, 40);
            item.ForeColor = Color.FromArgb(230, 235, 245);
            item.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
        }
        private void RecentFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            MessageBox.Show($"You clicked: {clickedItem.Text}");
        }
        public void OpenFile(string filePath)
        {
            // Wyczyść DataGridView przed utworzeniem.
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            progressBar1.Value = 10;
            // Tutaj kod do otwierania pliku...
            LinkFromRecentFiles = filePath;
            // Przejdź do funkcji głównej.
            MainProgram();
            // Aktualizacja listy ostatnio otwieranych plików
            UpdateRecentFiles(filePath);
        }
        private void UpdateRecentFiles(string filePath)
        {
            StringCollection recentFiles = Properties.Settings.Default.RecentFiles;
            if (recentFiles == null)
            {
                recentFiles = new StringCollection();
            }

            // Usuń ścieżkę, jeśli już istnieje, aby uniknąć duplikatów
            if (recentFiles.Contains(filePath))
            {
                recentFiles.Remove(filePath);
            }

            // Dodaj ścieżkę na początku listy
            recentFiles.Insert(0, filePath);

            // Ogranicz listę do np. 5 wpisów
            while (recentFiles.Count > 5)
            {
                recentFiles.RemoveAt(recentFiles.Count - 1);
            }

            Properties.Settings.Default.RecentFiles = recentFiles;
            Properties.Settings.Default.Save();

            // Opcjonalnie, aktualizuj interfejs użytkownika
            UpdateRecentFilesMenu();
        }
        private void UpdateRecentFilesMenu()
        {
            // Przykład: aktualizacja menu w formularzu
            RecentFiles.DropDownItems.Clear();
            try
            {
                if (Properties.Settings.Default.RecentFiles != null)
                {
                    foreach (string file in Properties.Settings.Default.RecentFiles)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(file);
                        StyleMenuItem(item);
                        item.Click += (sender, e) => OpenFile(file);
                        RecentFiles.DropDownItems.Add(item);
                    }
                }
            }
            catch (Exception ex) { };
        }
        private void Form_Load(object sender, EventArgs e)
        {

            UpdateRecentFilesMenu();
        }

        private void opcjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Otwórz okno opcji
            //Form2 opctionForm2 = new Form2();
            //opctionForm2.Show();
        }

        private void label1_version_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Otwiera orginalny plik aktualnie przeglądanego pliku Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ącyPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = LinkFromRecentFiles;

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas otwierania pliku: " + ex.Message);
            }
        }
    }
}

