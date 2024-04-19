using Baza_wiazek_przyciskow_20240205.Source;
using System.ComponentModel;


namespace Baza_wiazek_przyciskow_20240205
{
  
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

                    // Stwórz tablice z fragmentem œcie¿ki dostêpu.
                    string[] linkName = new string[rowCount];
                    linkName = convertData.LinkNameWire(FOLDER, NAME, BTE);

                    string[] finishPath = new string[rowCount];
                    finishPath = convertData.ExcelOrZuken(linkName);

                    progressBar1.Value = 100;
                    Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli
                }

            }

        }
    }
}
