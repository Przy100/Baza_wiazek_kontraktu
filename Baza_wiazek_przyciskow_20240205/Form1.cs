using Baza_wiazek_przyciskow_20240205.Source;
using System.ComponentModel;
using System.Diagnostics;


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
                    CreateNameHyperlink(NAME, newBTE);
                    Application.DoEvents(); // Pozwala na odœwie¿anie UI w trakcie pêtli
                }

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (System.IO.File.Exists(LINK[0]))
            {

                // Otwiera plik pod podan¹ œcie¿k¹
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(LINK[19]) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Link nie dzia³a.");
            }
        }
        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            MessageBox.Show($"Klikniêto link: {linkLabel.Text}");
            int Path = int.Parse(linkLabel.Name.Substring(linkLabel.Name.Length - 1));

            if (System.IO.File.Exists(LINK[Path])) 
            {
                // Otwiera plik pod podan¹ œcie¿k¹
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(LINK[Path]) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Link nie dzia³a.");
            }
        }
    }
}
