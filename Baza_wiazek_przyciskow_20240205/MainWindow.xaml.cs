using Baza_wiazek_przyciskow_20240205.Source;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Baza_wiazek_przyciskow_20240205
{
    public partial class MainWindow : Window
    {
        private string? currentFilePath;

        public ObservableCollection<DocumentRow> Documents { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            string selectedTheme = Properties.Settings.Default.ColorTheme;
            ThemeManager.ApplyTheme(selectedTheme);
            UpdateThemeMenuState(selectedTheme);

            Configurator configurator = new();
            VersionTextBlock.Text = configurator.Version;
            UpdateRecentFilesMenu();
            UpdateEmptyState();
        }

        private async void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            Configurator configurator = new();
            OpenFileDialog openFileDialog = new()
            {
                InitialDirectory = ResolveInitialDirectory(configurator.InitialDirectory),
                Filter = "Excel files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            bool? dialogResult;
            try
            {
                dialogResult = openFileDialog.ShowDialog(this);
            }
            catch (FileNotFoundException)
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialogResult = openFileDialog.ShowDialog(this);
            }

            if (dialogResult != true)
            {
                return;
            }

            try
            {
                string temporaryPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(openFileDialog.FileName));
                File.Copy(openFileDialog.FileName, temporaryPath, true);
                await OpenFileAsync(temporaryPath);
                UpdateRecentFiles(temporaryPath);
            }
            catch (Exception ex)
            {
                ShowError("Nie udało się wczytać pliku LW.", ex);
            }
        }

        private static string ResolveInitialDirectory(string configuredPath)
        {
            if (!string.IsNullOrWhiteSpace(configuredPath) && Directory.Exists(configuredPath))
            {
                return configuredPath;
            }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!string.IsNullOrWhiteSpace(documentsPath) && Directory.Exists(documentsPath))
            {
                return documentsPath;
            }

            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        private async Task OpenFileAsync(string filePath)
        {
            SetBusyState(true);
            Documents.Clear();
            UpdateEmptyState();
            currentFilePath = filePath;
            FileNameTextBlock.Text = Path.GetFileName(filePath);
            RowCountTextBlock.Text = "0 pozycji";
            UpdateProgress(10, "Wczytywanie danych");

            try
            {
                List<DocumentRow> rows = await Task.Run(() => BuildRows(filePath, UpdateProgress));

                Documents.Clear();
                foreach (DocumentRow row in rows)
                {
                    Documents.Add(row);
                }

                RowCountTextBlock.Text = $"{Documents.Count} pozycji";
                UpdateProgress(100, "Gotowe");
            }
            catch (Exception ex)
            {
                UpdateProgress(0, "Błąd wczytywania");
                ShowError("Nie udało się przetworzyć pliku LW.", ex);
            }
            finally
            {
                SetBusyState(false);
                UpdateEmptyState();
            }
        }

        private static List<DocumentRow> BuildRows(string filePath, Action<int, string> reportProgress)
        {
            ExcelReader excelReader = new();
            int rowCount = excelReader.GetRowCount(filePath, 5, 6);
            if (rowCount < 0)
            {
                throw new InvalidOperationException("Nie można odczytać arkusza \"Lista wiązek\".");
            }

            string[] bte = excelReader.FillArray(filePath, rowCount, 2);
            string[] names = excelReader.FillArray(filePath, rowCount, 3);
            reportProgress(20, "Analiza nazw dokumentów");

            ConvertData convertData = new();
            string[] ids = convertData.GetLastTwoLetters(names);
            string[] folders = convertData.FolderSelection(ids);
            reportProgress(30, "Dobieranie folderów");

            bte = convertData.MoreThenOneBTENumber(bte);
            string[] normalizedBte = convertData.CodePlate(names, bte);
            string[] linkNames = convertData.LinkNameWire(folders, names, normalizedBte);
            string[] finishPaths = convertData.ExcelOrZuken(linkNames);
            reportProgress(45, "Budowanie ścieżek dokumentacji");

            string[] sbcIndexes = excelReader.FillArray(filePath, rowCount, 4);
            reportProgress(55, "Odczyt indeksów SBC");
            string[] quantities = excelReader.FillArray(filePath, rowCount, 5);
            reportProgress(65, "Odczyt ilości");
            string[] priorities = excelReader.FillArray(filePath, rowCount, 6);
            reportProgress(75, "Odczyt priorytetów");
            string[] statuses = excelReader.FillArray(filePath, rowCount, 7);
            reportProgress(82, "Odczyt statusów");
            string[] revisions = excelReader.FillArray(filePath, rowCount, 8);
            string[] descriptions = excelReader.FillArray(filePath, rowCount, 9);
            string[] notes = excelReader.FillArray(filePath, rowCount, 10);
            reportProgress(92, "Przygotowanie tabeli");

            List<DocumentRow> rows = new(rowCount);
            for (int i = 0; i < rowCount; i++)
            {
                rows.Add(new DocumentRow
                {
                    Number = i + 1,
                    BteNumber = normalizedBte[i] ?? string.Empty,
                    DocumentKind = names[i] ?? string.Empty,
                    SbcIndex = sbcIndexes[i] ?? string.Empty,
                    Quantity = quantities[i] ?? string.Empty,
                    Priority = priorities[i] ?? string.Empty,
                    Status = statuses[i] ?? string.Empty,
                    Revision = revisions[i] ?? string.Empty,
                    Description = descriptions[i] ?? string.Empty,
                    Notes = notes[i] ?? string.Empty,
                    LinkPath = finishPaths[i] ?? string.Empty
                });
            }

            return rows;
        }

        private void OpenDocument_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button { DataContext: DocumentRow row })
            {
                return;
            }

            OpenPath(row.LinkPath, "Nie można otworzyć dokumentacji.");
        }

        private void OpenCurrentFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentFilePath))
            {
                MessageBox.Show(this, "Najpierw wczytaj plik LW.", "Brak pliku", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            OpenPath(currentFilePath, "Nie można otworzyć aktualnego pliku LW.");
        }

        private void ConfigureLinks_Click(object sender, RoutedEventArgs e)
        {
            LinkSettingsWindow settingsWindow = new()
            {
                Owner = this
            };

            if (settingsWindow.ShowDialog() == true)
            {
                StatusTextBlock.Text = "Zapisano ścieżki";
            }
        }

        private void ThemeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem { Tag: string themeKey })
            {
                return;
            }

            ThemeManager.ApplyTheme(themeKey);
            ThemeManager.SaveTheme(themeKey);
            UpdateThemeMenuState(themeKey);
        }

        private void UpdateThemeMenuState(string selectedThemeKey)
        {
            ThemeLightFreshMenuItem.IsChecked = selectedThemeKey == "LightFresh";
            ThemeLightWarmMenuItem.IsChecked = selectedThemeKey == "LightWarm";
            ThemeDarkGraphiteMenuItem.IsChecked = selectedThemeKey == "DarkGraphite";
            ThemeDarkNordMenuItem.IsChecked = selectedThemeKey == "DarkNord";
        }

        private async void RecentFile_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem { Tag: string filePath })
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show(this, "Ten plik nie jest już dostępny.", "Brak pliku", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                await OpenFileAsync(filePath);
                UpdateRecentFiles(filePath);
            }
        }

        private void UpdateRecentFiles(string filePath)
        {
            StringCollection recentFiles = Properties.Settings.Default.RecentFiles ?? new StringCollection();

            if (recentFiles.Contains(filePath))
            {
                recentFiles.Remove(filePath);
            }

            recentFiles.Insert(0, filePath);

            while (recentFiles.Count > 5)
            {
                recentFiles.RemoveAt(recentFiles.Count - 1);
            }

            Properties.Settings.Default.RecentFiles = recentFiles;
            Properties.Settings.Default.Save();
            UpdateRecentFilesMenu();
        }

        private void UpdateRecentFilesMenu()
        {
            RecentFilesMenuItem.Items.Clear();

            StringCollection? recentFiles = Properties.Settings.Default.RecentFiles;
            if (recentFiles == null || recentFiles.Count == 0)
            {
                RecentFilesMenuItem.Items.Add(new MenuItem
                {
                    Header = "Brak ostatnich plików",
                    IsEnabled = false
                });
                return;
            }

            foreach (string? file in recentFiles)
            {
                if (string.IsNullOrWhiteSpace(file))
                {
                    continue;
                }

                MenuItem item = new()
                {
                    Header = Path.GetFileName(file),
                    ToolTip = file,
                    Tag = file
                };
                item.Click += RecentFile_Click;
                RecentFilesMenuItem.Items.Add(item);
            }
        }

        private void OpenPath(string? path, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show(this, "Brak ścieżki do otwarcia.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ShowError(errorMessage, ex);
            }
        }

        private void UpdateProgress(int value, string status)
        {
            Dispatcher.Invoke(() =>
            {
                ImportProgressBar.Value = value;
                StatusTextBlock.Text = status;
            }, DispatcherPriority.Background);
        }

        private void SetBusyState(bool isBusy)
        {
            Cursor = isBusy ? System.Windows.Input.Cursors.Wait : null;
        }

        private void UpdateEmptyState()
        {
            EmptyStatePanel.Visibility = Documents.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show(this, $"{message}\n\n{ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
