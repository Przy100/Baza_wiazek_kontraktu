using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baza_wiazek_przyciskow_20240205.Source
{
    public class ConvertData
    {
        /// <summary>
        /// Z tablicy nazw wiązek zwraca ID wiązki np. "Wiązka PZ" --> "PZ".
        /// </summary>
        /// <param name="NAME">Tablica z nazwami wiązek.</param>
        /// <returns></returns>
        public string[] GetLastTwoLetters(string[] NAME)
        {
            string[] ID = new string[NAME.Length];

            for (int i = 1; i <= NAME.Length; i++)
            {
                if (NAME[i - 1] == null)
                {
                    Console.WriteLine("Pusta komórka tablicy NAME[" + (i - 1) + "]");
                }
                // Zapisz dwa ostatnie znaki z tablicy NAME do tablicy ID.
                ID[i - 1] = NAME[i - 1].Substring(NAME[i - 1].Length - 2);
            }
            return ID;
        }
        /// <summary>
        /// Na podstawie ID wybiera folder, np. "LZ" --> "001-Wiązka LZ".
        /// </summary>
        /// <param name="ID">Tablica z skrótami nazw wiązek np. "LZ".</param>
        /// <returns></returns>
        public string[] FolderSelection(string[] ID)
        {
            string[] FOLDER = new string[ID.Length];
            // Ścieżka do pliku zawierającym dodatkowe informacje pomocnicze.
            Configurator cFile = new Configurator();
            string filePath = cFile.filePath_DATA;
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Folder");

                    for (int i = 1; i <= ID.Length; i++)
                    {
                        for (int j = 1; j <= ID.Length; j++)
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
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
            }
            return FOLDER;
        }
        /// <summary>
        /// Scala ścieżkę dostępu dla wiązek: nazwa folderu + nazwa wiązki + numer BTE.
        /// Po czym zwraca fragment ścieżki dostępu.
        /// </summary>
        /// <param name="FOLDER">Tablica z nazwami folderu.</param>
        /// <param name="BTE">Tablica z numerami BTE wiązek.</param>
        /// <param name="NAME">Tablica z nazwami wiązek.</param>
        /// <returns></returns>
        public string[] LinkNameWire(string[] FOLDER, string[] NAME, string[] BTE)
        {
            string[] linkName = new string[FOLDER.Length];
            // Początek ścieżki dostępu, która się nie zmienia.
            Configurator cFile = new Configurator();
            string startPath = cFile.startPath;


            for (int i = 1; i <= FOLDER.Length; i++)
            {
                linkName[i - 1] = startPath + FOLDER[i - 1] + "\\" + NAME[i - 1] + " " + BTE[i - 1];
            }
            return linkName;
        }
        /// <summary>
        /// Funkcja prywatna sprawdza czy dana pozycja jest wiązką czy płytą.
        /// </summary>
        /// <param name="FOLDER">Nazwa folderu np. "001-Wiązka LZ".</param>
        /// <param name="rowCount">Liczba pozcyji.</param>
        /// <returns></returns>
        private string WireOrPlate(string FOLDER, int rowCount)
        {
            string wireOrPlate = "";
            Configurator cFile = new Configurator();
            string filePath = cFile.filePath_DATA;
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
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
            }
        next_WireOrPlate:
            return wireOrPlate;
        }
        public string[] ExcelOrZuken(string[] linkName)
        {
            string[] finishPath = new string[linkName.Length];

            for (int i = 1; i <= linkName.Length; i++)
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
