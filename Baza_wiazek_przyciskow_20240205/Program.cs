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
        /// Liczy wiersze zawieraj¹ce okreœlone s³owa w konkretnej komórce, zaczynaj¹c od okreœlonego wiersza.
        /// </summary>
        /// <param name="filePath">Œcie¿ka do pliku Excel.</param>
        /// <param name="startRow">Numer wiersza, od którego zaczyna siê liczenie.</param>
        /// <param name="columnIndex">Indeks kolumny do przeszukania (1-based index).</param>
        /// <returns>Liczba wierszy spe³niaj¹cych kryteria.</returns>
        public int GetRowCount(string filePath, int startRow, int columnIndex)
        {
            int count = 0;
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Lista wi¹zek");
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
                Console.WriteLine("Wyst¹pi³ b³¹d: " + ex.Message);
                return -1; // Zwraca -1 w przypadku b³êdu
            }
            return count;
        }
        /// <summary>
        /// Wype³nia tablice numerami BTE oraz nazwami wi¹zek
        /// </summary>
        /// <param name="filePath">Œcie¿ka do pliku Excel.</param>
        /// <param name="rowCount">Liczba wierszy które trzeba przeiterowaæ.</param>
        /// <param name="column">Numer kolumny, która ma zostaæ zapisana do tablicy</param>
        /// <returns>Tablica wype³niona dannymi.</returns>
        public string[] FillArray(string filePath, int rowCount, int column)
        {
            string[] DATA = new string[rowCount];
            // j - zmienna pomocnicza, czasem wi¹zki s¹ pod wierszem SIMS i naklejki
            int j = 0;
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Lista wi¹zek");
                    for (int i = 1; i <= worksheet.LastRowUsed().RowNumber(); i++)
                    {
                        var value = worksheet.Cell(i + 4, 6).GetValue<string>();
                        if (value.Contains("F0") || value.Contains("F1") || value.Contains("F2")) 
                        {
                            // Pobierz wartoœæ komórki i przypisz do tablicy
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
                Console.WriteLine("Wyst¹pi³ b³¹d: " + ex.Message);
            }

            return DATA;
        }
    }
    public class ConvertData 
    {
        /// <summary>
        /// Z tablicy nazw wi¹zek zwraca ID wi¹zki np. "Wi¹zka PZ" --> "PZ".
        /// </summary>
        /// <param name="NAME">Tablica z nazwami wi¹zek.</param>
        /// <returns></returns>
        public string[] GetLastTwoLetters(string[] NAME) 
        {
            string[] ID = new string[NAME.Length];
            
            for(int i = 1; i <= NAME.Length; i++) 
            {
                if (NAME[i - 1] == null) 
                {
                    Console.WriteLine("Pusta komórka tablicy NAME["+ (i - 1) + "]");
                }
                // Zapisz dwa ostatnie znaki z tablicy NAME do tablicy ID.
                ID[i - 1] = NAME[i - 1].Substring(NAME[i - 1].Length - 2);
            }
            return ID;
        }
        /// <summary>
        /// Na podstawie ID wybiera folder, np. "LZ" --> "001-Wi¹zka LZ".
        /// </summary>
        /// <param name="ID">Tablica z skrótami nazw wi¹zek np. "LZ".</param>
        /// <returns></returns>
        public string[] FolderSelection(string[] ID) 
        {
            string[] FOLDER = new string[ID.Length];
            // Œcie¿ka do pliku zawieraj¹cym dodatkowe informacje pomocnicze.
            string filePath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERAÆ_Baza_wi¹zek_kontraktu_DATA.xlsx";
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
                Console.WriteLine("Wyst¹pi³ b³¹d: " + ex.Message);
            }
            return FOLDER;
        }
        /// <summary>
        /// Scala œcie¿kê dostêpu dla wi¹zek: nazwa folderu + nazwa wi¹zki + numer BTE.
        /// Po czym zwraca fragment œcie¿ki dostêpu.
        /// </summary>
        /// <param name="FOLDER">Tablica z nazwami folderu.</param>
        /// <param name="BTE">Tablica z numerami BTE wi¹zek.</param>
        /// <param name="NAME">Tablica z nazwami wi¹zek.</param>
        /// <returns></returns>
        public string[] LinkNameWire(string[] FOLDER, string[] NAME, string[] BTE) 
        {
            string[] linkName = new string[FOLDER.Length];
            // Pocz¹tek œcie¿ki dostêpu, która siê nie zmienia.
            string startPath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\";


            for (int i = 1; i <= FOLDER.Length; i++) 
            {
                linkName[i - 1] = startPath + FOLDER[i - 1] + "\\" + NAME[i - 1] + " " + BTE[i - 1]; 
            }
            return linkName;
        }
        /// <summary>
        /// Funkcja prywatna sprawdza czy dana pozycja jest wi¹zk¹ czy p³yt¹.
        /// </summary>
        /// <param name="FOLDER">Nazwa folderu np. "001-Wi¹zka LZ".</param>
        /// <param name="rowCount">Liczba pozcyji.</param>
        /// <returns></returns>
        private string WireOrPlate(string FOLDER, int rowCount) 
        {
            string wireOrPlate = "";
            string filePath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERAÆ_Baza_wi¹zek_kontraktu_DATA.xlsx";
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
                Console.WriteLine("Wyst¹pi³ b³¹d: " + ex.Message);
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