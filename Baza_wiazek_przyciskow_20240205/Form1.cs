using Baza_wiazek_przyciskow_20240205.Source;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;


namespace Baza_wiazek_przyciskow_20240205
{

    public partial class Form1 : Form
    {
        // Ostateczne �cie�ki dost�pu.
        string[] LINK;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView_CellContentClick);
        }
        /// <summary>
        /// Tworzy tyle linkLabel ile jest wi�zek, nadaje im nazwy od wi�zek.
        /// </summary>
        /// <param name="NAME">Nazwa wi�zki.</param>
        /// <param name="newBTE">Numer BTE wi�zki.</param>
        private void CreateNameHyperlink(string[] NAME, string[] newBTE)
        {
            for (int i = 1; i <= NAME.Length; i++)
            {
                LinkLabel linkLabel = new LinkLabel();
                linkLabel.Name = $"linkLabel{i}";
                linkLabel.Text = NAME[i - 1] + " " + newBTE[i - 1];
                linkLabel.Location = new Point(307, 2 + i * 20); // Przyk�ad rozmieszczenia
                linkLabel.Size = new Size(250, 20);
                linkLabel.LinkClicked += LinkLabel_LinkClicked; // Dodanie obs�ugi zdarzenia klikni�cia
                this.Controls.Add(linkLabel);
            }
        }
        private void InitializeDataGridView(string[] newBTE, string[] NAME, string[] IndeksySBC, string[] Ilosc, string[] Prio, string[] Status, string[] Rewizja, string[] Opis, string[] Uwagi)
        {
            // Podstawowa konfiguracja
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.ColumnCount = 10;

            // Kolumna hiper��cze
            DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
            linkColumn.Name = "LinkColumn";
            linkColumn.Text = "Kliknij tutaj";
            linkColumn.UseColumnTextForLinkValue = true;
            dataGridView1.Columns.Add(linkColumn);
            // Nazwy kolumn
            dataGridView1.Columns[0].Name = "Lp.";
            dataGridView1.Columns[1].Name = "Numer wi�zki BTE";
            dataGridView1.Columns[2].Name = "Nazwa";
            dataGridView1.Columns[3].Name = "Indeks SBC";
            dataGridView1.Columns[4].Name = "Ilo��";
            dataGridView1.Columns[5].Name = "Priorytet";
            dataGridView1.Columns[6].Name = "Status";
            dataGridView1.Columns[7].Name = "Rewizja";
            dataGridView1.Columns[8].Name = "Opis / zastosowanie";
            dataGridView1.Columns[9].Name = "Uwagi";

            for(int i = 1; i <= NAME.Length; i++) 
            {
                dataGridView1.Rows.Add(i, newBTE[i - 1], NAME[i - 1], IndeksySBC[i - 1], Ilosc[i - 1], Prio[i - 1], Status[i - 1], Rewizja[i - 1], Opis[i - 1], Uwagi[i - 1], LINK[i - 1]);
            }

        }
        private void button1_LW_Click(object sender, EventArgs e)
        {
            // Progres bar.
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;

            // Wybranie pliku Excel z list� wi�zek
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                Configurator cFile = new Configurator();
                openFileDialog.InitialDirectory = cFile.InitialDirectory;
                openFileDialog.Filter = "Excel files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                progressBar1.Value = 10;
                Application.DoEvents(); // Pozwala na od�wie�anie UI w trakcie p�tli
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Pobierz �cie�k� do wybranego pliku
                    string filePath = openFileDialog.FileName;
                    // Podaj ile jest wierszy w tym pliku
                    var excelReader = new ExcelReader();
                    int rowCount = excelReader.GetRowCount(filePath, 5, 6);
                    // Stw�rz dwie tablice string o takiej wielko�ci
                    string[] BTE = new string[rowCount];
                    string[] NAME = new string[rowCount];
                    BTE = excelReader.FillArray(filePath, rowCount, 2);
                    NAME = excelReader.FillArray(filePath, rowCount, 3);

                    progressBar1.Value = 20;
                    Application.DoEvents(); // Pozwala na od�wie�anie UI w trakcie p�tli

                    // Stw�rz tablice string z ID wi�zek na podstawie tablicy NAME.
                    var convertData = new ConvertData();
                    string[] ID = new string[rowCount];
                    ID = convertData.GetLastTwoLetters(NAME);

                    // Stw�rz tablice string z nazwami folder�w wi�zek na podstawie tablicy ID.
                    string[] FOLDER = new string[rowCount];
                    FOLDER = convertData.FolderSelection(ID);

                    progressBar1.Value = 30;
                    Application.DoEvents(); // Pozwala na od�wie�anie UI w trakcie p�tli

                    // Zmienia kodowanie p�yt na AAx.
                    string[] newBTE = new string[rowCount];
                    newBTE = convertData.CodePlate(NAME, BTE);

                    // Stw�rz tablice z fragmentem �cie�ki dost�pu.
                    string[] linkName = new string[rowCount];
                    linkName = convertData.LinkNameWire(FOLDER, NAME, newBTE);

                    // Ko�cowa �cie�ka dost�pu.
                    string[] finishPath = new string[rowCount];
                    finishPath = convertData.ExcelOrZuken(linkName);
                    LINK = finishPath;
                    progressBar1.Value = 40;
                    Application.DoEvents(); // Pozwala na od�wie�anie UI w trakcie p�tli

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

                    // Tworzy i zmienia nazwy linkLabel na nazwy wi�zek.
                    //CreateNameHyperlink(NAME, newBTE);
                    // Tworzy tabelk� przypominaj�c� t� z Excela.
                    InitializeDataGridView(newBTE, NAME, IndeksySBC, Ilosc, Priorytet, Status, Rewizja, Opis, Uwagi);
                    Application.DoEvents(); // Pozwala na od�wie�anie UI w trakcie p�tli
                    progressBar1.Value = 100;
                }

            }

        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            
            int Path = int.Parse(linkLabel.Name.Substring(linkLabel.Name.Length - 1));

            if (System.IO.File.Exists(LINK[Path])) 
            {
                // Otwiera plik pod podan� �cie�k�
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(LINK[Path]) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Plik nie istnieje lub jest b��dnie nazwany.");
            }
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sprawdzenie, czy klikni�to kolumn� hiper��cza
            if (e.ColumnIndex == dataGridView1.Columns["LinkColumn"].Index && e.RowIndex >= 0)
            {
                string url = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                try
                {
                    System.Diagnostics.Process.Start(url);  // Otw�rz domy�ln� przegl�dark� z podanym URL
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nie mo�na otworzy� linku: " + ex.Message);
                }
            }
        }
    }
}
