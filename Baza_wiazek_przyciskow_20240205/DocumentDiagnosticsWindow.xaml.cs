using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace Baza_wiazek_przyciskow_20240205
{
    public partial class DocumentDiagnosticsWindow : Window
    {
        private readonly DocumentRow documentRow;

        public DocumentDiagnosticsWindow(DocumentRow documentRow, string? extraMessage = null)
        {
            InitializeComponent();

            this.documentRow = documentRow;
            TitleTextBlock.Text = documentRow.DocumentAvailability == "Brak"
                ? "Nie znaleziono dokumentacji"
                : "Diagnostyka dokumentu";
            SubtitleTextBlock.Text = $"{documentRow.DocumentKind}, BTE {documentRow.BteNumber}";
            DiagnosticsTextBox.Text = BuildReport(documentRow, extraMessage);
        }

        private void OpenBaseFolder_Click(object sender, RoutedEventArgs e)
        {
            string? baseFolder = GetBaseFolder(documentRow);
            if (string.IsNullOrWhiteSpace(baseFolder) || !Directory.Exists(baseFolder))
            {
                MessageBox.Show(this, "Folder bazowy nie istnieje albo nie jest dostępny.", "Brak folderu", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = baseFolder,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Nie można otworzyć folderu bazowego.\n\n{ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CopyReport_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(DiagnosticsTextBox.Text);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private static string BuildReport(DocumentRow row, string? extraMessage)
        {
            StringBuilder report = new();

            report.AppendLine("Pozycja:");
            report.AppendLine($"- Typ dokumentu: {row.DocumentKind}");
            report.AppendLine($"- BTE: {row.BteNumber}");
            report.AppendLine($"- Status dokumentu: {row.DocumentAvailability}");

            if (!string.IsNullOrWhiteSpace(row.DocumentAvailabilityDetails))
            {
                report.AppendLine($"- Szczegóły: {row.DocumentAvailabilityDetails}");
            }

            if (!string.IsNullOrWhiteSpace(extraMessage))
            {
                report.AppendLine($"- Dodatkowy komunikat: {extraMessage}");
            }

            report.AppendLine();
            report.AppendLine("Ścieżka bazowa:");
            report.AppendLine(string.IsNullOrWhiteSpace(row.BaseLinkPath) ? "(brak)" : row.BaseLinkPath);

            report.AppendLine();
            report.AppendLine("Sprawdzone warianty:");
            if (row.CheckedPaths.Length == 0)
            {
                report.AppendLine("(brak sprawdzonych ścieżek)");
            }
            else
            {
                for (int i = 0; i < row.CheckedPaths.Length; i++)
                {
                    report.AppendLine($"{i + 1}. {row.CheckedPaths[i]}");
                }
            }

            report.AppendLine();
            report.AppendLine("Możliwe przyczyny:");
            report.AppendLine("- dokument nie został jeszcze utworzony,");
            report.AppendLine("- nazwa dokumentu albo numer BTE w LW różni się od nazwy pliku,");
            report.AppendLine("- plik DATA wskazuje zły folder dla tego typu dokumentu,");
            report.AppendLine("- brakuje dostępu do katalogu sieciowego albo połączenia z siecią.");

            return report.ToString();
        }

        private static string? GetBaseFolder(DocumentRow row)
        {
            string path = !string.IsNullOrWhiteSpace(row.LinkPath) ? row.LinkPath : row.BaseLinkPath;
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            return Path.GetDirectoryName(path);
        }
    }
}
