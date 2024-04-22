using Baza_wiazek_przyciskow_20240205.Source;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;


namespace Baza_wiazek_przyciskow_20240205
{

    public partial class Form1 : Form
    {
        // Ostateczne œcie¿ki dostêpu.
        string[] LINK;
        public Form1()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// Tworzy tyle linkLabel ile jest wi¹zek, nadaje im nazwy od wi¹zek.
        /// </summary>
        /// <param name="NAME">Nazwa wi¹zki.</param>
        /// <param name="newBTE">Numer BTE wi¹zki.</param>
        private void CreateNameHyperlink(string[] NAME, string[] newBTE)
        {
            for (int i = 1; i <= NAME.Length; i++)
            {
                LinkLabel linkLabel = new LinkLabel();
                linkLabel.Name = $"linkLabel{i}";
                linkLabel.Text = NAME[i - 1] + " " + newBTE[i - 1];
                linkLabel.Location = new Point(307, 2 + i * 20); // Przyk³ad rozmieszczenia
                linkLabel.Size = new Size(250, 20);
                linkLabel.LinkClicked += LinkLabel_LinkClicked; // Dodanie obs³ugi zdarzenia klikniêcia
                this.Controls.Add(linkLabel);
            }
        }
        private void InitializeDataGridView(string[] newBTE, string[] NAME)
        {
            // Podstawowa konfiguracja
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.ColumnCount = 10;

            // Nazwy kolumn
            dataGridView1.Columns[0].Name = "Lp.";
            dataGridView1.Columns[1].Name = "Numer wi¹zki BTE";
            dataGridView1.Columns[2].Name = "Nazwa";
            dataGridView1.Columns[3].Name = "Indeks SBC";
            dataGridView1.Columns[4].Name = "Iloœæ";
            dataGridView1.Columns[5].Name = "Priorytet";
            dataGridView1.Columns[6].Name = "Status";
            dataGridView1.Columns[7].Name = "Rewizja";
            dataGridView1.Columns[8].Name = "Opis / zastosowanie";
            dataGridView1.Columns[9].Name = "Uwagi";

            for(int i = 1; i <= NAME.Length; i++) 
            {
                dataGridView1.Rows.Add(i, newBTE[i - 1], NAME[i - 1]);
            }

        }
        private void button1_LW_Click(object sender, EventArgs e)
        {
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
                progressBar1.Value = 25;
                Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Pobierz œcie¿kê do wybranego pliku
                    string filePath = openFileDialog.FileName;
                    // Podaj ile jest wierszy w tym pliku
                    var excelReader = new ExcelReader();
                    int rowCount = excelReader.GetRowCount(filePath, 5, 6);
                    // Stwórz dwie tablice string o takiej wielkoœci
                    string[] BTE = new string[rowCount];
                    string[] NAME = new string[rowCount];
                    BTE = excelReader.FillArray(filePath, rowCount, 2);
                    NAME = excelReader.FillArray(filePath, rowCount, 3);

                    progressBar1.Value = 50;
                    Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli

                    // Stwórz tablice string z ID wi¹zek na podstawie tablicy NAME.
                    var convertData = new ConvertData();
                    string[] ID = new string[rowCount];
                    ID = convertData.GetLastTwoLetters(NAME);

                    // Stwórz tablice string z nazwami folderów wi¹zek na podstawie tablicy ID.
                    string[] FOLDER = new string[rowCount];
                    FOLDER = convertData.FolderSelection(ID);

                    progressBar1.Value = 75;
                    Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli

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
                    progressBar1.Value = 100;
                    Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli

                    // Tworzy i zmienia nazwy linkLabel na nazwy wi¹zek.
                    //CreateNameHyperlink(NAME, newBTE);
                    // Tworzy tabelkê przypominaj¹c¹ t¹ z Excela.
                    InitializeDataGridView(newBTE, NAME);
                    Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli
                }

            }

        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            
            int Path = int.Parse(linkLabel.Name.Substring(linkLabel.Name.Length - 1));

            if (System.IO.File.Exists(LINK[Path])) 
            {
                // Otwiera plik pod podan¹ œcie¿k¹
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(LINK[Path]) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Plik nie istnieje lub jest b³êdnie nazwany.");
            }
        }
    }
}
