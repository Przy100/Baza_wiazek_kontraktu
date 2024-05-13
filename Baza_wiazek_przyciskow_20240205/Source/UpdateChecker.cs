using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq; // Używam Newtonsoft.Json do parsowania JSON
using System.IO.Compression; // Obsługa plików ZIP

namespace Baza_wiazek_przyciskow_20240205.Source
{
    public class UpdateChecker
    {
        private readonly string repositoryUrl;
        //private readonly string token;  // Token dostępu - prywatne repo

        public UpdateChecker(string repositoryUrl)
        {
            this.repositoryUrl = repositoryUrl;
            //this.token = token;
        }
        /// <summary>
        /// Sprawdza, która wersja programu jest aktualnie najnowsza w repozytorium GitHub.
        /// </summary>
        /// <returns></returns>
        public async Task CheckForUpdatesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "request"); // Niezbędne dla GitHub API

                try
                {
                    string apiUrl = $"{repositoryUrl}/releases/latest";
                    var response = await client.GetAsync(apiUrl);
                    MessageBox.Show("Kod statusu HTTP: " + response.StatusCode);
                    response.EnsureSuccessStatusCode();

                    string jsonString = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(jsonString);
                    string latestVersion = json["tag_name"].ToString(); // Przyjmujemy, że tag_name przechowuje wersję

                    //MessageBox.Show($"Najnowsza wersja to: {latestVersion}");

                    // Pobranie ZIP najnowszego repozytorium.
                    string downloadUrl = json["zipball_url"].ToString(); // URL do pobrania pliku ZIP
                    
                    await DownloadAndUpdateAsync(downloadUrl, latestVersion);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Nie udało się sprawdzić aktualizacji: {ex.Message}");
                }
            }
        }
        public static async Task DownloadAndUpdateAsync(string downloadUrl, string version)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), $"update_{version}.zip");
            string extractPath = Path.Combine(Path.GetTempPath(), $"update_{version}");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Console.WriteLine("Pobieranie aktualizacji...");

                    HttpResponseMessage response = await client.GetAsync(downloadUrl);
                    response.EnsureSuccessStatusCode();

                    using (var fs = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    Console.WriteLine("Rozpakowywanie aktualizacji...");
                    ZipFile.ExtractToDirectory(tempFilePath, extractPath);

                    Console.WriteLine("Aktualizowanie programu...");
                    UpdateApplication(extractPath);

                    Console.WriteLine("Aktualizacja zakończona pomyślnie.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Wystąpił błąd podczas pobierania lub aktualizacji: {e.Message}");
                }
            }
        }
        public static void UpdateApplication(string extractPath)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            foreach (var file in Directory.GetFiles(extractPath))
            {
                var fileName = Path.GetFileName(file);
                var destinationFile = Path.Combine(currentDirectory, fileName);

                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }

                File.Move(file, destinationFile);
            }
        }
    }
}
