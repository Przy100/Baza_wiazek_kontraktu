using Baza_wiazek_przyciskow_20240205.Properties;

namespace Baza_wiazek_przyciskow_20240205.Source
{
    public class Configurator
    {
        public const string DefaultInitialDirectory = "\\\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\Wiązki\\! Specyfikacje wiązek elektrycznych";
        public const string DefaultFilePathData = "\\\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\ELCAD\\CAE7.3.2\\schematy_343\\Wojciechowski_sz\\EXCEL_VBA\\AA_Baza_wiązek_kontraktu_Vol_2\\DATA\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
        public const string DefaultStartPath = "\\\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\Wiązki\\Urbino G4\\";

        // Konfiguracja linków.
        public string InitialDirectory { get; set; }
        public string filePath_DATA { get; set; }
        public string startPath { get; set; }

        // Konfiguracja serwera.
        public string client { get; set; }
        public string database { get; set; }
        public string collection { get; set; }
        public string Version { get; set; }

        public Configurator()
        {
            // dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained false

            InitialDirectory = GetConfiguredPath(Settings.Default.InitialDirectory, DefaultInitialDirectory);
            filePath_DATA = GetConfiguredPath(Settings.Default.FilePathData, DefaultFilePathData);
            startPath = GetConfiguredPath(Settings.Default.StartPath, DefaultStartPath);

            // Serwer
            client = "mongodb://localhost:27017";
            database = "baza_wiazek_kontraktu";
            collection = "users";

            // Wersja
            Version = "v2.5.1";
        }

        private static string GetConfiguredPath(string? configuredPath, string defaultPath)
        {
            return string.IsNullOrWhiteSpace(configuredPath) ? defaultPath : configuredPath;
        }
    }
}


// InitialDirectory = "\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\Wiązki\\! Specyfikacje wiązek elektrycznych";
// filePath_DATA = "\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\ELCAD\\CAE7.3.2\\schematy_343\\Wojciechowski_sz\\EXCEL_VBA\\AA_Baza_wiązek_kontraktu_Vol_2\\DATA\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
// startPath = "\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\Wiązki\\Urbino G4\\";

// LEGION:
// InitialDirectory = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu";
// filePath_DATA = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
// startPath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\";

// ASUS:
// InitialDirectory = "C:\\AA_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu";
// filePath_DATA = "C:\\AA_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
// startPath = "C:\\AA_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\";


// Komendy na publisha:

// dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true

// Druga, prostsza opcja: nie pakować do single-file i kopiować cały folder publish:
// dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile = false

// Jeszcze jedna ważna rzecz: --self - contained false oznacza, że na komputerze docelowym musi być zainstalowany .NET Desktop Runtime 8 x64. Jeśli chcesz uruchamiać na komputerach bez zainstalowanego .NET, użyj:
// dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile = true - p:IncludeAllContentForSelfExtract = true
