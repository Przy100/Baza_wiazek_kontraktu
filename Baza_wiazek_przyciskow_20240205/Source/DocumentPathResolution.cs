using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Baza_wiazek_przyciskow_20240205.Source
{
    public sealed class DocumentPathResolution
    {
        public string BasePath { get; private init; } = string.Empty;
        public string ResolvedPath { get; private init; } = string.Empty;
        public string Availability { get; private init; } = string.Empty;
        public string Details { get; private init; } = string.Empty;
        public string[] CheckedPaths { get; private init; } = [];
        public bool CanOpen { get; private init; }

        public static DocumentPathResolution Resolve(string? basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                return new DocumentPathResolution
                {
                    Availability = "Błąd ścieżki",
                    Details = "Nie udało się zbudować ścieżki bazowej dokumentu. Sprawdź plik DATA i arkusz Folder."
                };
            }

            string[] candidates = BuildCandidatePaths(basePath).ToArray();
            string? firstAccessDeniedPath = null;
            string? firstErrorMessage = null;

            foreach (string candidate in candidates)
            {
                FileProbeResult probeResult = ProbeFile(candidate);
                if (probeResult.Status == FileProbeStatus.Exists)
                {
                    string extension = Path.GetExtension(candidate);
                    return new DocumentPathResolution
                    {
                        BasePath = basePath,
                        ResolvedPath = candidate,
                        Availability = $"Znaleziono {extension}",
                        Details = $"Dokument został znaleziony jako {extension}.",
                        CheckedPaths = candidates,
                        CanOpen = true
                    };
                }

                if (probeResult.Status == FileProbeStatus.AccessDenied && firstAccessDeniedPath == null)
                {
                    firstAccessDeniedPath = candidate;
                    firstErrorMessage = probeResult.Message;
                }
                else if (probeResult.Status == FileProbeStatus.Error && firstErrorMessage == null)
                {
                    firstErrorMessage = probeResult.Message;
                }
            }

            if (firstAccessDeniedPath != null)
            {
                return new DocumentPathResolution
                {
                    BasePath = basePath,
                    ResolvedPath = firstAccessDeniedPath,
                    Availability = "Brak dostępu",
                    Details = firstErrorMessage ?? "Windows odmówił dostępu do jednej ze sprawdzanych ścieżek.",
                    CheckedPaths = candidates
                };
            }

            return new DocumentPathResolution
            {
                BasePath = basePath,
                ResolvedPath = candidates.FirstOrDefault() ?? string.Empty,
                Availability = firstErrorMessage == null ? "Brak" : "Błąd ścieżki",
                Details = firstErrorMessage ?? "Nie znaleziono dokumentu w żadnym ze sprawdzanych wariantów.",
                CheckedPaths = candidates
            };
        }

        private static IEnumerable<string> BuildCandidatePaths(string basePath)
        {
            string xlsmPath = basePath + ".xlsm";
            string e3sPath = basePath + ".e3s";

            yield return xlsmPath;
            yield return e3sPath;

            string hyphenatedE3sPath = BuildHyphenatedE3sPath(e3sPath);
            if (!string.Equals(hyphenatedE3sPath, e3sPath, StringComparison.OrdinalIgnoreCase))
            {
                yield return hyphenatedE3sPath;
            }
        }

        private static string BuildHyphenatedE3sPath(string e3sPath)
        {
            int middleIndex = e3sPath.Length - 20;
            if (middleIndex < 0 || middleIndex >= e3sPath.Length - 1)
            {
                return e3sPath;
            }

            return e3sPath[..middleIndex] + "-" + e3sPath[(middleIndex + 1)..];
        }

        private static FileProbeResult ProbeFile(string path)
        {
            try
            {
                FileAttributes attributes = File.GetAttributes(path);
                if (attributes.HasFlag(FileAttributes.Directory))
                {
                    return FileProbeResult.Error("Sprawdzana ścieżka wskazuje folder zamiast pliku.");
                }

                return FileProbeResult.Exists();
            }
            catch (FileNotFoundException)
            {
                return FileProbeResult.Missing();
            }
            catch (DirectoryNotFoundException)
            {
                return FileProbeResult.Missing();
            }
            catch (UnauthorizedAccessException ex)
            {
                return FileProbeResult.AccessDenied(ex.Message);
            }
            catch (Exception ex) when (ex is IOException or NotSupportedException or PathTooLongException or ArgumentException)
            {
                return FileProbeResult.Error(ex.Message);
            }
        }

        private enum FileProbeStatus
        {
            Exists,
            Missing,
            AccessDenied,
            Error
        }

        private sealed record FileProbeResult(FileProbeStatus Status, string Message)
        {
            public static FileProbeResult Exists() => new(FileProbeStatus.Exists, string.Empty);
            public static FileProbeResult Missing() => new(FileProbeStatus.Missing, string.Empty);
            public static FileProbeResult AccessDenied(string message) => new(FileProbeStatus.AccessDenied, message);
            public static FileProbeResult Error(string message) => new(FileProbeStatus.Error, message);
        }
    }
}
