using Baza_wiazek_przyciskow_20240205.Source;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Specialized;
using System;
using MongoDB.Driver;
using Baza_wiazek_przyciskow_20240205.Server;


namespace Baza_wiazek_przyciskow_20240205
{

    public partial class Form1 : Form
    {
        // Ostateczne œcie¿ki dostêpu.
        string[] LINK;
        string LinkFromRecentFiles;
        public Form1()
        {
            InitializeComponent();
            // Obs³uga zdarzenia klikniêcia w dataGridView1.
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView_CellContentClick);
            // Inicjalizacja RecentFiles.
            InitializeRecentFilesMenu();
            // Obs³uga zdarzenia za³adowania RecentFiles do zak³adki "Ostatnio otw...".
            this.Load += new EventHandler(Form_Load);

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

            // Ustawienia wygl¹du nag³ówków kolumn
            dataGridView1.EnableHeadersVisualStyles = false;  // Wy³¹czenie stylów wizualnych, aby umo¿liwiæ niestandardowe stylizowanie
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);

            // Kolumna hiper³¹cze
            DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
            linkColumn.Name = "Nazwa";
            linkColumn.UseColumnTextForLinkValue = false;
            dataGridView1.Columns.Add(linkColumn);

            // Nazwy kolumn
            dataGridView1.Columns[0].Name = "Lp.";
            dataGridView1.Columns[0].Width = 75;
            dataGridView1.Columns[1].Name = "Numer wi¹zki BTE";
            dataGridView1.Columns["Nazwa"].DisplayIndex = 2;
            dataGridView1.Columns[2].Name = "Indeks SBC";
            dataGridView1.Columns[3].Name = "Iloœæ";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].Name = "Priorytet";
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Name = "Status";
            dataGridView1.Columns[5].Width = 130;
            dataGridView1.Columns[6].Name = "Rewizja";
            dataGridView1.Columns[6].Width = 130;
            dataGridView1.Columns[7].Name = "Opis / zastosowanie";
            dataGridView1.Columns[7].Width = 700;
            dataGridView1.Columns[8].Name = "Uwagi";

            // Zmiana stylu kolumny.
            dataGridView1.Columns["Nazwa"].DefaultCellStyle.Font = new Font("Verdana", 10, FontStyle.Italic);
            dataGridView1.Columns[1].DefaultCellStyle.Font = new Font("Verdena", 10, FontStyle.Bold);
            dataGridView1.Columns[4].DefaultCellStyle.Font = new Font("Verdena", 10, FontStyle.Bold);
            dataGridView1.Columns[4].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.Columns["Nazwa"].DefaultCellStyle.BackColor = Color.LightGray;
            linkColumn.LinkColor = Color.Black;

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

        }
        private void MainProgram()
        {
            string filePath = LinkFromRecentFiles;
            // Podaj ile jest wierszy w tym pliku
            var excelReader = new ExcelReader();
            int rowCount = excelReader.GetRowCount(filePath, 5, 6);
            // Stwórz dwie tablice string o takiej wielkoœci
            string[] BTE = new string[rowCount];
            string[] NAME = new string[rowCount];
            BTE = excelReader.FillArray(filePath, rowCount, 2);
            NAME = excelReader.FillArray(filePath, rowCount, 3);

            progressBar1.Value = 20;
            Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli

            // Stwórz tablice string z ID wi¹zek na podstawie tablicy NAME.
            var convertData = new ConvertData();
            string[] ID = new string[rowCount];
            ID = convertData.GetLastTwoLetters(NAME);

            // Stwórz tablice string z nazwami folderów wi¹zek na podstawie tablicy ID.
            string[] FOLDER = new string[rowCount];
            FOLDER = convertData.FolderSelection(ID);

            progressBar1.Value = 30;
            Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli

            // Jeœli BTE ma dwa lub wiêcej numerów BTE.
            BTE = convertData.MoreThenOneBTENumber(BTE);

            // Zmienia kodowanie p³yt na AAx.
            string[] newBTE = new string[rowCount];
            newBTE = convertData.CodePlate(NAME, BTE);

            // Stwórz tablice z fragmentem œcie¿ki dostêpu.
            string[] linkName = new string[rowCount];
            linkName = convertData.LinkNameWire(FOLDER, NAME, newBTE);

            // Koñcowa œcie¿ka dostêpu.
            string[] finishPath = new string[rowCount];
            finishPath = convertData.ExcelOrZuken(linkName);
            LINK = finishPath;
            progressBar1.Value = 40;
            Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli

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

            // Tworzy tabelkê przypominaj¹c¹ t¹ z Excela.
            InitializeDataGridView(newBTE, NAME, IndeksySBC, Ilosc, Priorytet, Status, Rewizja, Opis, Uwagi);

            Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli
            progressBar1.Value = 100;
        }
        private void button1_LW_Click(object sender, EventArgs e)
        {
            // TEST MongoDB - 20240429
            new NewUsers();

            // Koniec TEST MongoDB

            // Wyczyœæ DataGridView przed utworzeniem.
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            // Progres bar.
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;

            // Wybranie pliku Excel z list¹ wi¹zek
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                Configurator cFile = new Configurator();
                openFileDialog.InitialDirectory = cFile.InitialDirectory;
                openFileDialog.Filter = "Excel files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                progressBar1.Value = 10;
                Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string LW_Name = openFileDialog.FileName;
                    this.File_Name_LW.Text = Path.GetFileName(LW_Name);
                    File_Name_LW.Visible = true;

                    // Pobierz œcie¿kê do wybranego pliku
                    string filePath = openFileDialog.FileName;
                    // Dodaj plik do RecentFile.
                    OpenFile(filePath);
                    // Dodaj link do zmiennej globalnej.
                    //LinkFromRecentFiles = filePath;
                    // PrzejdŸ do funkcji g³ównej.
                    //MainProgram();
                }

            }

        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sprawdzenie, czy klikniêto kolumnê hiper³¹cza
            if (e.ColumnIndex == dataGridView1.Columns["Nazwa"].Index && e.RowIndex >= 0)
            {
                // Zwraca numer wiersza, który chcemy otworzyæ.
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(LINK[e.RowIndex]) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nie mo¿na otworzyæ linku: " + ex.Message);
                }
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1 == null || dataGridView1.Columns["Nazwa"] == null)
                return;  // Zabezpieczenie przed niew³aœciwie zainicjalizowanymi obiektami

            if (e.ColumnIndex == dataGridView1.Columns["Nazwa"].Index && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex] == null)
                    return;

                DataGridViewLinkCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell;
                if (cell != null && cell.Tag != null)
                {
                    cell.Value = cell.Tag.ToString();  // U¿yj ToString() dla bezpieczeñstwa
                }
            }
        }
        private void InitializeRecentFilesMenu()
        {
            // Dodanie przyk³adowych wpisów
            for (int i = 0; i < 5; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem($"File {i + 1}");
                item.Click += RecentFile_Click;
                RecentFiles.DropDownItems.Add(item);
            }
        }
        private void RecentFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            MessageBox.Show($"You clicked: {clickedItem.Text}");
        }
        public void OpenFile(string filePath)
        {
            // Wyczyœæ DataGridView przed utworzeniem.
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            progressBar1.Value = 10;
            // Tutaj kod do otwierania pliku...
            LinkFromRecentFiles = filePath;
            // PrzejdŸ do funkcji g³ównej.
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

            // Usuñ œcie¿kê, jeœli ju¿ istnieje, aby unikn¹æ duplikatów
            if (recentFiles.Contains(filePath))
            {
                recentFiles.Remove(filePath);
            }

            // Dodaj œcie¿kê na pocz¹tku listy
            recentFiles.Insert(0, filePath);

            // Ogranicz listê do np. 5 wpisów
            while (recentFiles.Count > 5)
            {
                recentFiles.RemoveAt(recentFiles.Count - 1);
            }

            Properties.Settings.Default.RecentFiles = recentFiles;
            Properties.Settings.Default.Save();

            // Opcjonalnie, aktualizuj interfejs u¿ytkownika
            UpdateRecentFilesMenu();
        }
        private void UpdateRecentFilesMenu()
        {
            // Przyk³ad: aktualizacja menu w formularzu
            RecentFiles.DropDownItems.Clear();
            try
            {
                if(Properties.Settings.Default.RecentFiles != null)
                {   
                    foreach (string file in Properties.Settings.Default.RecentFiles)
                    { 
                        ToolStripMenuItem item = new ToolStripMenuItem(file);
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
            Form2 opctionForm2 = new Form2();
            opctionForm2.Show();
        }
    }
}
