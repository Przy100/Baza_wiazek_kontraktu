using ClosedXML.Excel;
using DocumentFormat.OpenXml.ExtendedProperties;
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
                try
                {
                    
                    // Zapisz dwa ostatnie znaki z tablicy NAME do tablicy ID.
                    ID[i - 1] = NAME[i - 1].Substring(NAME[i - 1].Length - 2);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wystąpił błąd: " + ex.Message);
                }
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
            string lo_filePath = cFile.filePath_DATA;
            try
            {
                using (var workbook = new XLWorkbook(lo_filePath))
                {
                    var worksheet = workbook.Worksheet("Folder");

                    for (int i = 1; i <= ID.Length; i++)
                    {
                        for (int j = 1; j <= 68; j++)
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
        /// <param name="newBTE">Tablica z numerami BTE wiązek.</param>
        /// <param name="NAME">Tablica z nazwami wiązek.</param>
        /// <returns></returns>
        public string[] LinkNameWire(string[] FOLDER, string[] NAME, string[] newBTE)
        {
            string[] linkName = new string[FOLDER.Length];
            // Początek ścieżki dostępu, która się nie zmienia.
            Configurator cFile = new Configurator();
            string startPath = cFile.startPath;


            for (int i = 1; i <= FOLDER.Length; i++)
            {
                linkName[i - 1] = startPath + FOLDER[i - 1] + "\\" + NAME[i - 1] + " " + newBTE[i - 1];
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
        /// <summary>
        /// Zmienia kodowania płyt na AAx w tablicy.
        /// </summary>
        /// <param name="linkNAME">Tablica z podanymi nścieżkami dostępu.</param>
        /// <param name="BTE">Numer BTE wiążki.</param>
        /// <returns></returns>
        public string[] CodePlate(string[] NAME, string[] BTE)
        {
            string[] codePlate = new string[BTE.Length];
            string[] Plate = ["Płyta P0", "Płyta P1", "Płyta P2", "Płyta P3", "Płyta P4", "Płyta P5", "Płyta P6", "Płyta P7", "Płyta P8", "Płyta P9"];

            for (int i = 1; i <= BTE.Length; i++)
            {
                codePlate[i - 1] = BTE[i - 1];

                for (int j = 1; j <= Plate.Length; j++)
                {
                    if (NAME[i - 1] == Plate[j - 1])
                    {
                        string newLetters = "AA";
                        int middleIndex = BTE[i - 1].Length - 6;
                        string start = BTE[i - 1].Substring(0, middleIndex);
                        string end = BTE[i - 1].Substring(middleIndex + 2);

                        codePlate[i - 1] = start + newLetters + end;
                    }
                }
            }

            return codePlate;
        }
        /// <summary>
        /// Dodaje odpowiednie rozszerzenie.
        /// </summary>
        /// <param name="linkName">Cała ścieżka dostępu z wyjątkiem rozszerzenia.</param>
        /// <returns></returns>
        public string[] ExcelOrZuken(string[] linkName)
        {
            string[] finishPath = new string[linkName.Length];

            for (int i = 1; i <= linkName.Length; i++)
            {
                finishPath[i - 1] = linkName[i - 1] + ".xlsm";
                if (File.Exists(finishPath[i - 1])) ;
                else
                {
                    finishPath[i - 1] = linkName[i - 1] + ".e3s";

                    if (File.Exists(finishPath[i - 1])) ;
                    else
                    {
                        // Dodaj "-" jeśli samo .e3s nie zadziała.
                        // Np. Wiązka RB U499-123-087-AA.e3s --> Wiązka RB-U499-123-087-AA.e3s
                        string newLetters = "-";
                        int middleIndex = finishPath[i - 1].Length - 20;
                        string start = finishPath[i - 1].Substring(0, middleIndex);
                        string end = finishPath[i - 1].Substring(middleIndex + 1);
                        finishPath[i - 1] = start + newLetters + end;
                    }
                }
            }

            return finishPath;
        }
        public string[] MoreThenOneBTENumber(string[] BTE) 
        {
            string[] newBTE = new string[BTE.Length];
            
            for(int i = 1; i <= BTE.Length; i++) 
            {
                // Jeśli BTE[i - 1].Length >= 16 --> są dwa numery BTE w jednej komórce.
                if (BTE[i - 1].Length >= 16) 
                {
                    int middleIndex = BTE[i - 1].Length - 15;
                    newBTE[i - 1] = BTE[i - 1].Substring(middleIndex);
                }
                else 
                {
                    newBTE[i - 1] = BTE[i - 1];
                }
            }

            return newBTE;
        }
    }
}
