using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
using System;
using System.Windows.Forms;
using System.IO;

namespace Baza_wiazek_przyciskow_20240205
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
    // FUNKCJE
    public class ExcelReader
    {
        /// <summary>
        /// Liczy wiersze zawieraj�ce okre�lone s�owa w konkretnej kom�rce, zaczynaj�c od okre�lonego wiersza.
        /// </summary>
        /// <param name="filePath">�cie�ka do pliku Excel.</param>
        /// <param name="startRow">Numer wiersza, od kt�rego zaczyna si� liczenie.</param>
        /// <param name="columnIndex">Indeks kolumny do przeszukania (1-based index).</param>
        /// <returns>Liczba wierszy spe�niaj�cych kryteria.</returns>
        public int GetRowCount(string filePath, int startRow, int columnIndex)
        {
            int count = 0;
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Lista wi�zek");
                    for(int row = startRow; row <= worksheet.LastRowUsed().RowNumber(); row++) 
                    {
                        var cellValue = worksheet.Cell(row, columnIndex).GetValue<string>();
                        if(cellValue.Contains("F0") || cellValue.Contains("F1") || cellValue.Contains("F2")) 
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyst�pi� b��d: " + ex.Message);
                return -1; // Zwraca -1 w przypadku b��du
            }
            return count;
        }
        /// <summary>
        /// Wype�nia tablice numerami BTE oraz nazwami wi�zek
        /// </summary>
        /// <param name="filePath">�cie�ka do pliku Excel.</param>
        /// <param name="rowCount">Liczba wierszy kt�re trzeba przeiterowa�.</param>
        /// <param name="column">Numer kolumny, kt�ra ma zosta� zapisana do tablicy</param>
        /// <returns>Tablica wype�niona dannymi.</returns>
        public string[] FillArray(string filePath, int rowCount, int column)
        {
            string[] DATA = new string[rowCount];
            // j - zmienna pomocnicza, czasem wi�zki s� pod wierszem SIMS i naklejki
            int j = 0;
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Lista wi�zek");
                    for (int i = 1; i <= worksheet.LastRowUsed().RowNumber(); i++)
                    {
                        var value = worksheet.Cell(i + 4, 6).GetValue<string>();
                        if (value.Contains("F0") || value.Contains("F1") || value.Contains("F2")) 
                        {
                            // Pobierz warto�� kom�rki i przypisz do tablicy
                            var cellValue = worksheet.Cell(i + 4, column).Value.ToString();
                            DATA[i - 1 - j] = cellValue;
                        }
                        else 
                        {
                            j++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyst�pi� b��d: " + ex.Message);
            }

            return DATA;
        }
    }
    public class ConvertData 
    {
        /// <summary>
        /// Z tablicy nazw wi�zek zwraca ID wi�zki np. "Wi�zka PZ" --> "PZ".
        /// </summary>
        /// <param name="NAME">Tablica z nazwami wi�zek.</param>
        /// <returns></returns>
        public string[] GetLastTwoLetters(string[] NAME) 
        {
            string[] ID = new string[NAME.Length];
            
            for(int i = 1; i <= NAME.Length; i++) 
            {
                if (NAME[i - 1] == null) 
                {
                    Console.WriteLine("Pusta kom�rka tablicy NAME["+ (i - 1) + "]");
                }
                // Zapisz dwa ostatnie znaki z tablicy NAME do tablicy ID.
                ID[i - 1] = NAME[i - 1].Substring(NAME[i - 1].Length - 2);
            }
            return ID;
        }
        /// <summary>
        /// Na podstawie ID wybiera folder, np. "LZ" --> "001-Wi�zka LZ".
        /// </summary>
        /// <param name="ID">Tablica z skr�tami nazw wi�zek np. "LZ".</param>
        /// <returns></returns>
        public string[] FolderSelection(string[] ID) 
        {
            string[] FOLDER = new string[ID.Length];
            // �cie�ka do pliku zawieraj�cym dodatkowe informacje pomocnicze.
            string filePath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERA�_Baza_wi�zek_kontraktu_DATA.xlsx";
            try 
            {
                using(var workbook = new XLWorkbook(filePath)) 
                {
                    var worksheet = workbook.Worksheet("Folder");

                    for(int i = 1; i <= ID.Length; i++) 
                    {
                        for(int j = 1; j <= ID.Length; j++) 
                        {
                            if (ID[i - 1] == worksheet.Cell(j + 1, 3).GetValue<string>())
                            {
                                FOLDER[i - 1] = worksheet.Cell(j + 1, 2).GetValue<string>();
                                goto next_FolderSelection;
                            }
                        }
                    next_FolderSelection:;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyst�pi� b��d: " + ex.Message);
            }
            return FOLDER;
        }
        /// <summary>
        /// Scala �cie�k� dost�pu dla wi�zek: nazwa folderu + nazwa wi�zki + numer BTE.
        /// Po czym zwraca fragment �cie�ki dost�pu.
        /// </summary>
        /// <param name="FOLDER">Tablica z nazwami folderu.</param>
        /// <param name="BTE">Tablica z numerami BTE wi�zek.</param>
        /// <param name="NAME">Tablica z nazwami wi�zek.</param>
        /// <returns></returns>
        public string[] LinkNameWire(string[] FOLDER, string[] NAME, string[] BTE) 
        {
            string[] linkName = new string[FOLDER.Length];
            // Pocz�tek �cie�ki dost�pu, kt�ra si� nie zmienia.
            string startPath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\";


            for (int i = 1; i <= FOLDER.Length; i++) 
            {
                linkName[i - 1] = startPath + FOLDER[i - 1] + "\\" + NAME[i - 1] + " " + BTE[i - 1]; 
            }
            return linkName;
        }
        /// <summary>
        /// Funkcja prywatna sprawdza czy dana pozycja jest wi�zk� czy p�yt�.
        /// </summary>
        /// <param name="FOLDER">Nazwa folderu np. "001-Wi�zka LZ".</param>
        /// <param name="rowCount">Liczba pozcyji.</param>
        /// <returns></returns>
        private string WireOrPlate(string FOLDER, int rowCount) 
        {
            string wireOrPlate = "";
            string filePath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERA�_Baza_wi�zek_kontraktu_DATA.xlsx";
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Folder");
                    for (int i = 1; i <= rowCount; i++)
                    {
                        if (FOLDER == worksheet.Cell(i - 1, 2).GetValue<string>()) 
                        {
                            wireOrPlate = worksheet.Cell(i - 1, 4).GetValue<string>();
                            goto next_WireOrPlate;
                        }
                    }       

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyst�pi� b��d: " + ex.Message);
            }
            next_WireOrPlate:
            return wireOrPlate;
        }
        public string[] ExcelOrZuken(string[] linkName) 
        {
            string[] finishPath = new string[linkName.Length];

            for(int i = 1; i <= linkName.Length; i++) 
            {
                finishPath[i - 1] = linkName[i - 1] + ".xlsx";
                if (File.Exists(finishPath[i - 1])) ;
                else 
                {
                    finishPath[i - 1] = linkName[i - 1] + ".e3s";
                }
            }

            return finishPath;
        }
    }
}