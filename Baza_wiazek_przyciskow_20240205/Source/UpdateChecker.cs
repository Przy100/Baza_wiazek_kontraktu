using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq; // Używam Newtonsoft.Json do parsowania JSON

namespace Baza_wiazek_przyciskow_20240205.Source
{
    public class UpdateChecker
    {
        private readonly string repositoryUrl;
        private readonly string token;  // Token dostępu - prywatne repo

        public UpdateChecker(string repositoryUrl, string token)
        {
            this.repositoryUrl = repositoryUrl;
            this.token = token;
        }

        public async Task CheckForUpdatesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"token {this.token}");
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

                    MessageBox.Show($"Najnowsza wersja to: {latestVersion}");
                    // Tutaj możesz dodać logikę porównującą bieżącą wersję z najnowszą
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Nie udało się sprawdzić aktualizacji: {ex.Message}");
                }
            }
        }
    }
}
