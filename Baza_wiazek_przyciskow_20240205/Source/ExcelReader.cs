using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baza_wiazek_przyciskow_20240205.Source
{
    public class ExcelReader
    {
        /// <summary>
        /// Liczy wiersze zawierające określone słowa w konkretnej komórce, zaczynając od określonego wiersza.
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku Excel.</param>
        /// <param name="startRow">Numer wiersza, od którego zaczyna się liczenie.</param>
        /// <param name="columnIndex">Indeks kolumny do przeszukania (1-based index).</param>
        /// <returns>Liczba wierszy spełniających kryteria.</returns>
        public int GetRowCount(string filePath, int startRow, int columnIndex)
        {
            int count = 0;
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Lista wiązek");
                    for (int row = startRow; row <= worksheet.LastRowUsed().RowNumber(); row++)
                    {
                        var cellValue = worksheet.Cell(row, columnIndex).GetValue<string>();
                        if (cellValue.Contains("F0") || cellValue.Contains("F1") || cellValue.Contains("F2"))
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
                return -1; // Zwraca -1 w przypadku błędu
            }
            return count;
        }
        /// <summary>
        /// Wypełnia tablice numerami BTE oraz nazwami wiązek
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku Excel.</param>
        /// <param name="rowCount">Liczba wierszy które trzeba przeiterować.</param>
        /// <param name="column">Numer kolumny, która ma zostać zapisana do tablicy</param>
        /// <returns>Tablica wypełniona dannymi.</returns>
        public string[] FillArray(string filePath, int rowCount, int column)
        {
            string[] DATA = new string[rowCount];
            // j - zmienna pomocnicza, czasem wiązki są pod wierszem SIMS i naklejki
            int j = 0;
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Lista wiązek");
                    for (int i = 1; i <= worksheet.LastRowUsed().RowNumber(); i++)
                    {
                        var value = worksheet.Cell(i + 4, 6).GetValue<string>();
                        if (value.Contains("F0") || value.Contains("F1") || value.Contains("F2"))
                        {
                            // Pobierz wartość komórki i przypisz do tablicy
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
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
            }

            return DATA;
        }

    }
}
