using Baza_wiazek_przyciskow_20240205;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

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
                        var cell = worksheet.Cell(row, columnIndex);
                        var cellValue = cell.GetValue<string>();
                        if ((cellValue.Contains("F0") || cellValue.Contains("F1") || cellValue.Contains("F2")) && IsCellCompletelyStrikethrough(cell) == false)
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
        /// Wypełnia tablice numerami BTE oraz nazwami wiązek.
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku Excel.</param>
        /// <param name="rowCount">Liczba wierszy które trzeba przeiterować.</param>
        /// <param name="column">Numer kolumny, która ma zostać zapisana do tablicy.</param>
        /// <returns>Tablica wypełniona danymi.</returns>
        public string[] FillArray(string filePath, int rowCount, int column)
        {
            return FillFormattedArray(filePath, rowCount, column).Select(cell => cell.Text).ToArray();
        }

        public FormattedTextValue[] FillFormattedArray(string filePath, int rowCount, int column)
        {
            List<FormattedTextValue> data = new(rowCount);
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Lista wiązek");
                    int lastRowNumber = worksheet.LastRowUsed().RowNumber();
                    for (int row = 5; row <= lastRowNumber && data.Count < rowCount; row++)
                    {
                        var value = worksheet.Cell(row, 6).GetValue<string>();
                        var priorityCell = worksheet.Cell(row, 6);
                        if ((value.Contains("F0") || value.Contains("F1") || value.Contains("F2")) && IsCellCompletelyStrikethrough(priorityCell) == false)
                        {
                            data.Add(ReadFormattedCell(worksheet.Cell(row, column)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd: " + ex.Message);
            }

            while (data.Count < rowCount)
            {
                data.Add(FormattedTextValue.FromPlainText(string.Empty));
            }

            return data.ToArray();
        }

        private static FormattedTextValue ReadFormattedCell(IXLCell cell)
        {
            string fallbackText = cell.GetFormattedString();
            if (!cell.HasRichText)
            {
                return new FormattedTextValue(
                    fallbackText,
                    [new FormattedTextRun(fallbackText, cell.Style.Font.Strikethrough)]);
            }

            List<FormattedTextRun> runs = new();
            foreach (IXLRichString richString in cell.GetRichText())
            {
                runs.Add(new FormattedTextRun(richString.Text, richString.Strikethrough));
            }

            return runs.Count == 0
                ? FormattedTextValue.FromPlainText(fallbackText)
                : new FormattedTextValue(string.Concat(runs.Select(run => run.Text)), runs);
        }

        private static bool IsCellCompletelyStrikethrough(IXLCell cell)
        {
            if (!cell.HasRichText)
            {
                return cell.Style.Font.Strikethrough;
            }

            List<IXLRichString> richStrings = cell.GetRichText().ToList();
            return richStrings.Count > 0 && richStrings.All(richString => string.IsNullOrEmpty(richString.Text) || richString.Strikethrough);
        }
    }
}
