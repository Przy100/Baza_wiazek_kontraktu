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

           // dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView_CellContentClick);
            // NIE CHCE DZIA�A�
            
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
            linkColumn.UseColumnTextForLinkValue = false;
            dataGridView1.Columns.Add(linkColumn);
            // Nazwy kolumn
            dataGridView1.Columns[0].Name = "Lp.";
            dataGridView1.Columns[0].Width = 75;
            dataGridView1.Columns[1].Name = "Numer wi�zki BTE";
            dataGridView1.Columns[2].Name = "Nazwa";
            dataGridView1.Columns[3].Name = "Indeks SBC";
            dataGridView1.Columns[4].Name = "Ilo��";
            dataGridView1.Columns[4].Width = 75;
            dataGridView1.Columns[5].Name = "Priorytet";
            dataGridView1.Columns[5].Width = 130;
            dataGridView1.Columns[6].Name = "Status";
            dataGridView1.Columns[6].Width = 130;
            dataGridView1.Columns[7].Name = "Rewizja";
            dataGridView1.Columns[7].Width = 130;
            dataGridView1.Columns[8].Name = "Opis / zastosowanie";
            dataGridView1.Columns[9].Name = "Uwagi";


            for(int i = 0; i < NAME.Length; i++) 
            {
                int rowIndex = dataGridView1.Rows.Add();  // Dodaje nowy wiersz i zapisuje jego indeks
                
                dataGridView1.Rows[rowIndex].Cells[0].Value = i + 1; // Lp.
                dataGridView1.Rows[rowIndex].Cells[1].Value = newBTE[i];
                dataGridView1.Rows[rowIndex].Cells[2].Value = NAME[i];
                dataGridView1.Rows[rowIndex].Cells[3].Value = IndeksySBC[i];
                dataGridView1.Rows[rowIndex].Cells[4].Value = Ilosc[i];
                dataGridView1.Rows[rowIndex].Cells[5].Value = Prio[i];
                dataGridView1.Rows[rowIndex].Cells[6].Value = Status[i];
                dataGridView1.Rows[rowIndex].Cells[7].Value = Rewizja[i];
                dataGridView1.Rows[rowIndex].Cells[8].Value = Opis[i];
                dataGridView1.Rows[rowIndex].Cells[9].Value = Uwagi[i];
                dataGridView1.Rows[rowIndex].Cells[10].Value = LINK[i];  // Ustawianie URL jako warto�ci kom�rki dla hiper��cza
                //dataGridView1.Rows[rowIndex].Cells["LinkColumn"].Tag = NAME[i];

               
               // Zmiana tekstu wy�wietlanego zamiast URL.
               // DataGridViewLinkCell linkCell = dataGridView1.Rows[rowIndex].Cells["LinkColumn"] as DataGridViewLinkCell;
               //linkCell.Value = LINK[i];  // URL, kt�ry b�dzie otwarty
               // linkCell.Tag = NAME[i];  // Tekst, kt�ry b�dzie wy�wietlany
               //dataGridView1.CellFormatting += dataGridView1_CellFormatting;
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
                    string LW_Name = openFileDialog.FileName;
                    this.File_Name_LW.Text = Path.GetFileName(LW_Name);
                    File_Name_LW.Visible = true;

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
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sprawdzenie, czy klikni�to kolumn� hiper��cza
            if (e.ColumnIndex == dataGridView1.Columns["LinkColumn"].Index && e.RowIndex >= 0)
            {
                string url = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nie mo�na otworzy� linku: " + ex.Message);
                }
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1 == null || dataGridView1.Columns["LinkColumn"] == null)
                return;  // Zabezpieczenie przed niew�a�ciwie zainicjalizowanymi obiektami

            if (e.ColumnIndex == dataGridView1.Columns["LinkColumn"].Index && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex] == null)
                    return;

                DataGridViewLinkCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell;
                if (cell != null && cell.Tag != null)
                {
                    cell.Value = cell.Tag.ToString();  // U�yj ToString() dla bezpiecze�stwa
                }
            }
        }
    }
}
