using Baza_wiazek_przyciskow_20240205.Properties;
using Baza_wiazek_przyciskow_20240205.Source;
using System.Windows;

namespace Baza_wiazek_przyciskow_20240205
{
    public partial class LinkSettingsWindow : Window
    {
        public LinkSettingsWindow()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            Configurator configurator = new();
            InitialDirectoryTextBox.Text = configurator.InitialDirectory;
            FilePathDataTextBox.Text = configurator.filePath_DATA;
            StartPathTextBox.Text = configurator.startPath;
        }

        private void RestoreDefaults_Click(object sender, RoutedEventArgs e)
        {
            InitialDirectoryTextBox.Text = Configurator.DefaultInitialDirectory;
            FilePathDataTextBox.Text = Configurator.DefaultFilePathData;
            StartPathTextBox.Text = Configurator.DefaultStartPath;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string initialDirectory = InitialDirectoryTextBox.Text.Trim();
            string filePathData = FilePathDataTextBox.Text.Trim();
            string startPath = EnsureTrailingSlash(StartPathTextBox.Text.Trim());

            if (string.IsNullOrWhiteSpace(initialDirectory)
                || string.IsNullOrWhiteSpace(filePathData)
                || string.IsNullOrWhiteSpace(startPath))
            {
                MessageBox.Show(this, "Wszystkie trzy ścieżki są wymagane.", "Brak ścieżki", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Settings.Default.InitialDirectory = initialDirectory;
            Settings.Default.FilePathData = filePathData;
            Settings.Default.StartPath = startPath;
            Settings.Default.Save();

            DialogResult = true;
        }

        private static string EnsureTrailingSlash(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || path.EndsWith('\\') || path.EndsWith('/'))
            {
                return path;
            }

            return path + "\\";
        }
    }
}
