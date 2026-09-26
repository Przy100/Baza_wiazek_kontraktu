using Baza_wiazek_przyciskow_20240205.Source;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Baza_wiazek_przyciskow_20240205
{
    public partial class MainWindow : Window
    {
        private string? currentFilePath;
        private readonly Dictionary<DataGridColumn, DataGridLength> normalColumnWidths = new();

        public ObservableCollection<DocumentRow> Documents { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ConfigureExcelCellStyles();
            ExcelViewToggle.IsChecked = Properties.Settings.Default.ExcelTableView;

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
                ApplyTableView();
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

            FormattedTextValue[] bteText = excelReader.FillFormattedArray(filePath, rowCount, 2);
            FormattedTextValue[] nameText = excelReader.FillFormattedArray(filePath, rowCount, 3);
            string[] bte = bteText.Select(cell => cell.Text).ToArray();
            string[] names = nameText.Select(cell => cell.Text).ToArray();
            reportProgress(20, "Analiza nazw dokumentów");

            ConvertData convertData = new();
            string[] ids = convertData.GetLastTwoLetters(names);
            string[] folders = convertData.FolderSelection(ids);
            reportProgress(30, "Dobieranie folderów");

            bte = convertData.MoreThenOneBTENumber(bte);
            string[] normalizedBte = convertData.CodePlate(names, bte);
            string[] linkNames = convertData.LinkNameWire(folders, names, normalizedBte);
            reportProgress(45, "Budowanie ścieżek dokumentacji");

            FormattedTextValue[] sbcIndexText = excelReader.FillFormattedArray(filePath, rowCount, 4);
            string[] sbcIndexes = sbcIndexText.Select(cell => cell.Text).ToArray();
            reportProgress(55, "Odczyt indeksów SBC");
            FormattedTextValue[] quantityText = excelReader.FillFormattedArray(filePath, rowCount, 5);
            string[] quantities = quantityText.Select(cell => cell.Text).ToArray();
            reportProgress(65, "Odczyt ilości");
            FormattedTextValue[] priorityText = excelReader.FillFormattedArray(filePath, rowCount, 6);
            string[] priorities = priorityText.Select(cell => cell.Text).ToArray();
            reportProgress(75, "Odczyt priorytetów");
            FormattedTextValue[] statusText = excelReader.FillFormattedArray(filePath, rowCount, 7);
            string[] statuses = statusText.Select(cell => cell.Text).ToArray();
            reportProgress(82, "Odczyt statusów");
            FormattedTextValue[] revisionText = excelReader.FillFormattedArray(filePath, rowCount, 8);
            string[] revisions = revisionText.Select(cell => cell.Text).ToArray();
            FormattedTextValue[] descriptionText = excelReader.FillFormattedArray(filePath, rowCount, 9);
            string[] descriptions = descriptionText.Select(cell => cell.Text).ToArray();
            FormattedTextValue[] notesText = excelReader.FillFormattedArray(filePath, rowCount, 10);
            string[] notes = notesText.Select(cell => cell.Text).ToArray();
            reportProgress(92, "Przygotowanie tabeli");
            reportProgress(94, "Sprawdzanie dostępności dokumentów");

            List<DocumentRow> rows = new(rowCount);
            for (int i = 0; i < rowCount; i++)
            {
                DocumentPathResolution pathResolution = DocumentPathResolution.Resolve(linkNames[i]);

                rows.Add(new DocumentRow
                {
                    Number = i + 1,
                    BteNumber = normalizedBte[i] ?? string.Empty,
                    BteNumberText = bteText[i],
                    DocumentKind = names[i] ?? string.Empty,
                    DocumentKindText = nameText[i],
                    SbcIndex = sbcIndexes[i] ?? string.Empty,
                    SbcIndexText = sbcIndexText[i],
                    Quantity = quantities[i] ?? string.Empty,
                    QuantityText = quantityText[i],
                    Priority = priorities[i] ?? string.Empty,
                    PriorityText = priorityText[i],
                    Status = statuses[i] ?? string.Empty,
                    StatusText = statusText[i],
                    Revision = revisions[i] ?? string.Empty,
                    RevisionText = revisionText[i],
                    Description = descriptions[i] ?? string.Empty,
                    DescriptionText = descriptionText[i],
                    Notes = notes[i] ?? string.Empty,
                    NotesText = notesText[i],
                    LinkPath = pathResolution.ResolvedPath,
                    BaseLinkPath = pathResolution.BasePath,
                    DocumentAvailability = pathResolution.Availability,
                    DocumentAvailabilityDetails = pathResolution.Details,
                    CheckedPaths = pathResolution.CheckedPaths,
                    CanOpenDocument = pathResolution.CanOpen
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

            if (!row.CanOpenDocument)
            {
                ShowDocumentDiagnostics(row);
                return;
            }

            OpenDocumentPath(row);
        }

        private void ShowDocumentDiagnostics_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: DocumentRow row })
            {
                ShowDocumentDiagnostics(row);
            }
        }

        private void DocumentsGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.FocusedElement is TextBoxBase) return;
            if (e.Key == Key.C && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                CopySelectedCells(includeHeaders: false);
                e.Handled = true;
            }
        }

        private void DocumentsGrid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (FindVisualParent<TextBoxBase>(e.OriginalSource as DependencyObject) != null) return;
            DataGridCell? cell = FindVisualParent<DataGridCell>(e.OriginalSource as DependencyObject);
            if (cell == null || cell.DataContext is not DocumentRow row)
            {
                return;
            }

            DataGridCellInfo clickedCell = new(row, cell.Column);
            if (!DocumentsGrid.SelectedCells.Contains(clickedCell))
            {
                DocumentsGrid.SelectedCells.Clear();
                DocumentsGrid.SelectedCells.Add(clickedCell);
            }

            DocumentsGrid.CurrentCell = clickedCell;
            cell.Focus();
        }

        private void CopySelection_Click(object sender, RoutedEventArgs e)
        {
            CopySelectedCells(includeHeaders: false);
        }

        private void CopySelectionWithHeaders_Click(object sender, RoutedEventArgs e)
        {
            CopySelectedCells(includeHeaders: true);
        }

        private void CopySelectedCells(bool includeHeaders)
        {
            List<DataGridCellInfo> selectedCells = DocumentsGrid.SelectedCells
                .Where(cell => cell.Item is DocumentRow && cell.Column != null)
                .OrderBy(cell => Documents.IndexOf((DocumentRow)cell.Item))
                .ThenBy(cell => cell.Column.DisplayIndex)
                .ToList();

            if (selectedCells.Count == 0 && DocumentsGrid.CurrentCell.Item is DocumentRow)
            {
                selectedCells.Add(DocumentsGrid.CurrentCell);
            }

            if (selectedCells.Count == 0)
            {
                return;
            }

            List<DocumentRow> selectedRows = selectedCells
                .Select(cell => (DocumentRow)cell.Item)
                .Distinct()
                .OrderBy(Documents.IndexOf)
                .ToList();
            List<DataGridColumn> selectedColumns = selectedCells
                .Select(cell => cell.Column)
                .Distinct()
                .OrderBy(column => column.DisplayIndex)
                .ToList();

            StringBuilder clipboardText = new();
            if (includeHeaders)
            {
                clipboardText.AppendLine(string.Join('\t', selectedColumns.Select(column => EscapeClipboardField(column.Header?.ToString() ?? string.Empty))));
            }

            foreach (DocumentRow row in selectedRows)
            {
                IEnumerable<string> values = selectedColumns.Select(column =>
                    selectedCells.Contains(new DataGridCellInfo(row, column))
                        ? EscapeClipboardField(GetCellClipboardValue(row, column))
                        : string.Empty);
                clipboardText.AppendLine(string.Join('\t', values));
            }

            Clipboard.SetText(clipboardText.ToString().TrimEnd('\r', '\n'));
            StatusTextBlock.Text = selectedCells.Count == 1 ? "Skopiowano komórkę" : $"Skopiowano {selectedCells.Count} komórek";
        }

        private static string GetCellClipboardValue(DocumentRow row, DataGridColumn column)
        {
            string propertyName = column.SortMemberPath;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return string.Empty;
            }

            PropertyInfo? property = typeof(DocumentRow).GetProperty(propertyName);
            var formatted = typeof(DocumentRow).GetProperty(propertyName + "Text")?.GetValue(row) as FormattedTextValue;
            string value = formatted?.Text ?? property?.GetValue(row)?.ToString() ?? string.Empty;
            return value.Replace("\r\n", "\n").Replace('\r', '\n');
        }

        private static string EscapeClipboardField(string value)
        {
            if (!value.Contains('\t') && !value.Contains('\n') && !value.Contains('"'))
            {
                return value;
            }

            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        private static T? FindVisualParent<T>(DependencyObject? child) where T : DependencyObject
        {
            while (child != null)
            {
                if (child is T parent)
                {
                    return parent;
                }

                child = child is ContentElement content
                    ? ContentOperations.GetParent(content) ?? (content as FrameworkContentElement)?.Parent
                    : VisualTreeHelper.GetParent(child);
            }

            return null;
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

        private void ConfigureExcelCellStyles()
        {
            foreach (var column in DocumentsGrid.Columns)
            {
                normalColumnWidths[column] = column.Width;
                if (column.SortMemberPath is "Number" or "DocumentAvailability") continue;
                string path = column.SortMemberPath + "Text.Appearance.";
                var style = new Style(typeof(DataGridCell), (Style)FindResource(typeof(DataGridCell)));
                var trigger = new DataTrigger
                {
                    Binding = new Binding()
                    {
                        Path = new PropertyPath(SelectableCellText.ExcelViewProperty),
                        RelativeSource = new RelativeSource(RelativeSourceMode.Self)
                    },
                    Value = true
                };
                trigger.Setters.Add(new Setter(Control.BackgroundProperty, new Binding(path + "Background") { Converter = new ExcelBrushConverter() }));
                trigger.Setters.Add(new Setter(Control.ForegroundProperty, new Binding(path + "Foreground") { Converter = new ExcelBrushConverter() }));
                trigger.Setters.Add(new Setter(Control.BorderBrushProperty, ExcelCellAppearance.Brush("#24000000")));
                trigger.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0.5)));
                trigger.Setters.Add(new Setter(Control.TemplateProperty, FindResource("ExcelCellTemplate")));
                trigger.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, new Binding(path + "VerticalAlignment")));
                trigger.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(4, 3, 4, 3)));
                if (column.SortMemberPath == "DocumentKind")
                {
                    trigger.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.Transparent));
                    trigger.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));
                    trigger.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(0)));
                    trigger.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Stretch));
                }
                style.Triggers.Add(trigger);
                if (column.SortMemberPath != "DocumentKind")
                {
                    var neutralCell = new MultiDataTrigger();
                    neutralCell.Conditions.Add(new Condition(new Binding
                    {
                        Path = new PropertyPath(SelectableCellText.ExcelViewProperty),
                        RelativeSource = new RelativeSource(RelativeSourceMode.Self)
                    }, true));
                    neutralCell.Conditions.Add(new Condition(new Binding(path + "UseThemeBackground"), true));
                    neutralCell.Setters.Add(new Setter(Control.BackgroundProperty, new DynamicResourceExtension("SurfaceMutedBrush")));
                    neutralCell.Setters.Add(new Setter(Control.ForegroundProperty, new DynamicResourceExtension("InkBrush")));
                    neutralCell.Setters.Add(new Setter(Control.BorderBrushProperty, new DynamicResourceExtension("BorderBrushSoft")));
                    style.Triggers.Add(neutralCell);
                }
                column.CellStyle = style;
            }
        }

        private void ExcelView_Changed(object sender, RoutedEventArgs e)
        {
            if (DocumentsGrid == null) return;
            ApplyTableView();
            Properties.Settings.Default.ExcelTableView = ExcelViewToggle.IsChecked == true;
            try { Properties.Settings.Default.Save(); }
            catch (System.Configuration.ConfigurationErrorsException)
            {
                StatusTextBlock.Text = "Zmieniono widok; nie udało się zapisać ustawienia.";
            }
        }

        private void ApplyTableView()
        {
            bool excel = ExcelViewToggle.IsChecked == true;
            bool wasExcel = SelectableCellText.GetExcelView(DocumentsGrid);
            if (excel && !wasExcel)
                foreach (var column in DocumentsGrid.Columns) normalColumnWidths[column] = column.Width;
            SelectableCellText.SetExcelView(DocumentsGrid, excel);
            DocumentsGrid.RowStyle = excel ? (Style)FindResource("ExcelRowStyle") : (Style)FindResource(typeof(DataGridRow));
            foreach (var column in DocumentsGrid.Columns)
            {
                if (excel && Documents.FirstOrDefault() is DocumentRow row &&
                    typeof(DocumentRow).GetProperty(column.SortMemberPath + "Text")?.GetValue(row) is FormattedTextValue { Appearance: { } appearance })
                    column.Width = new DataGridLength(Math.Max(column.SortMemberPath is "Priority" or "Revision" ? 86 : 68, appearance.Width));
                else if (!excel && normalColumnWidths.TryGetValue(column, out var width)) column.Width = width;
            }
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

        private void OpenDocumentPath(DocumentRow row)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = row.LinkPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ShowDocumentDiagnostics(row, ex.Message);
            }
        }

        private void ShowDocumentDiagnostics(DocumentRow row, string? extraMessage = null)
        {
            DocumentDiagnosticsWindow diagnosticsWindow = new(row, extraMessage)
            {
                Owner = this
            };
            diagnosticsWindow.ShowDialog();
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
