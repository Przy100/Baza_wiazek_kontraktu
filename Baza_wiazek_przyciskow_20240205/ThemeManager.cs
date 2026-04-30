using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Baza_wiazek_przyciskow_20240205
{
    public sealed record AppTheme(string Key, string DisplayName, IReadOnlyDictionary<string, Color> Colors);

    public static class ThemeManager
    {
        public const string DefaultThemeKey = "LightFresh";

        public static readonly IReadOnlyList<AppTheme> Themes =
        [
            new AppTheme("LightFresh", "Jasny Fresh", new Dictionary<string, Color>
            {
                ["AppBackgroundBrush"] = Color.FromRgb(244, 247, 251),
                ["SurfaceBrush"] = Colors.White,
                ["SurfaceMutedBrush"] = Color.FromRgb(238, 243, 248),
                ["InkBrush"] = Color.FromRgb(24, 34, 48),
                ["MutedInkBrush"] = Color.FromRgb(101, 113, 132),
                ["BorderBrushSoft"] = Color.FromRgb(216, 225, 236),
                ["AccentBrush"] = Color.FromRgb(37, 99, 235),
                ["AccentDarkBrush"] = Color.FromRgb(29, 78, 216),
                ["SuccessBrush"] = Color.FromRgb(5, 150, 105),
                ["WarningBrush"] = Color.FromRgb(217, 119, 6),
                ["IconBackgroundBrush"] = Color.FromRgb(232, 241, 255),
                ["IconBorderBrush"] = Color.FromRgb(207, 224, 255),
                ["ProgressBackgroundBrush"] = Color.FromRgb(229, 234, 241),
                ["GridLineBrush"] = Color.FromRgb(233, 238, 245),
                ["GridHeaderBrush"] = Color.FromRgb(247, 250, 253),
                ["GridHeaderBorderBrush"] = Color.FromRgb(225, 232, 241),
                ["GridHeaderTextBrush"] = Color.FromRgb(71, 85, 105),
                ["GridRowBrush"] = Colors.White,
                ["GridAlternateRowBrush"] = Color.FromRgb(251, 252, 254),
                ["GridHoverBrush"] = Color.FromRgb(241, 246, 255),
                ["GridSelectionBrush"] = Color.FromRgb(232, 241, 255),
                ["MenuHoverBrush"] = Color.FromRgb(232, 241, 255),
                ["ShadowColorBrush"] = Color.FromRgb(31, 41, 55)
            }),
            new AppTheme("LightWarm", "Jasny Warm", new Dictionary<string, Color>
            {
                ["AppBackgroundBrush"] = Color.FromRgb(247, 246, 242),
                ["SurfaceBrush"] = Color.FromRgb(255, 255, 252),
                ["SurfaceMutedBrush"] = Color.FromRgb(241, 239, 232),
                ["InkBrush"] = Color.FromRgb(35, 38, 47),
                ["MutedInkBrush"] = Color.FromRgb(112, 111, 101),
                ["BorderBrushSoft"] = Color.FromRgb(222, 217, 204),
                ["AccentBrush"] = Color.FromRgb(14, 116, 144),
                ["AccentDarkBrush"] = Color.FromRgb(21, 94, 117),
                ["SuccessBrush"] = Color.FromRgb(22, 163, 74),
                ["WarningBrush"] = Color.FromRgb(180, 83, 9),
                ["IconBackgroundBrush"] = Color.FromRgb(225, 246, 250),
                ["IconBorderBrush"] = Color.FromRgb(178, 226, 235),
                ["ProgressBackgroundBrush"] = Color.FromRgb(229, 226, 216),
                ["GridLineBrush"] = Color.FromRgb(232, 227, 216),
                ["GridHeaderBrush"] = Color.FromRgb(250, 249, 245),
                ["GridHeaderBorderBrush"] = Color.FromRgb(224, 219, 206),
                ["GridHeaderTextBrush"] = Color.FromRgb(82, 82, 72),
                ["GridRowBrush"] = Color.FromRgb(255, 255, 252),
                ["GridAlternateRowBrush"] = Color.FromRgb(250, 248, 243),
                ["GridHoverBrush"] = Color.FromRgb(230, 247, 250),
                ["GridSelectionBrush"] = Color.FromRgb(211, 242, 248),
                ["MenuHoverBrush"] = Color.FromRgb(211, 242, 248),
                ["ShadowColorBrush"] = Color.FromRgb(44, 51, 51)
            }),
            new AppTheme("DarkGraphite", "Ciemny Graphite", new Dictionary<string, Color>
            {
                ["AppBackgroundBrush"] = Color.FromRgb(16, 20, 26),
                ["SurfaceBrush"] = Color.FromRgb(24, 30, 38),
                ["SurfaceMutedBrush"] = Color.FromRgb(31, 39, 49),
                ["InkBrush"] = Color.FromRgb(235, 240, 246),
                ["MutedInkBrush"] = Color.FromRgb(152, 164, 180),
                ["BorderBrushSoft"] = Color.FromRgb(50, 61, 76),
                ["AccentBrush"] = Color.FromRgb(56, 189, 248),
                ["AccentDarkBrush"] = Color.FromRgb(2, 132, 199),
                ["SuccessBrush"] = Color.FromRgb(52, 211, 153),
                ["WarningBrush"] = Color.FromRgb(251, 191, 36),
                ["IconBackgroundBrush"] = Color.FromRgb(20, 57, 76),
                ["IconBorderBrush"] = Color.FromRgb(42, 119, 154),
                ["ProgressBackgroundBrush"] = Color.FromRgb(45, 55, 69),
                ["GridLineBrush"] = Color.FromRgb(43, 53, 67),
                ["GridHeaderBrush"] = Color.FromRgb(29, 37, 47),
                ["GridHeaderBorderBrush"] = Color.FromRgb(55, 66, 82),
                ["GridHeaderTextBrush"] = Color.FromRgb(191, 203, 218),
                ["GridRowBrush"] = Color.FromRgb(24, 30, 38),
                ["GridAlternateRowBrush"] = Color.FromRgb(27, 35, 45),
                ["GridHoverBrush"] = Color.FromRgb(36, 49, 63),
                ["GridSelectionBrush"] = Color.FromRgb(22, 74, 99),
                ["MenuHoverBrush"] = Color.FromRgb(36, 49, 63),
                ["ShadowColorBrush"] = Colors.Black
            }),
            new AppTheme("DarkNord", "Ciemny Nord", new Dictionary<string, Color>
            {
                ["AppBackgroundBrush"] = Color.FromRgb(27, 31, 42),
                ["SurfaceBrush"] = Color.FromRgb(36, 43, 56),
                ["SurfaceMutedBrush"] = Color.FromRgb(45, 53, 68),
                ["InkBrush"] = Color.FromRgb(232, 238, 247),
                ["MutedInkBrush"] = Color.FromRgb(162, 174, 192),
                ["BorderBrushSoft"] = Color.FromRgb(69, 80, 99),
                ["AccentBrush"] = Color.FromRgb(129, 140, 248),
                ["AccentDarkBrush"] = Color.FromRgb(99, 102, 241),
                ["SuccessBrush"] = Color.FromRgb(94, 234, 212),
                ["WarningBrush"] = Color.FromRgb(250, 204, 21),
                ["IconBackgroundBrush"] = Color.FromRgb(51, 56, 89),
                ["IconBorderBrush"] = Color.FromRgb(100, 111, 180),
                ["ProgressBackgroundBrush"] = Color.FromRgb(57, 66, 84),
                ["GridLineBrush"] = Color.FromRgb(57, 66, 84),
                ["GridHeaderBrush"] = Color.FromRgb(40, 48, 62),
                ["GridHeaderBorderBrush"] = Color.FromRgb(75, 86, 107),
                ["GridHeaderTextBrush"] = Color.FromRgb(202, 211, 225),
                ["GridRowBrush"] = Color.FromRgb(36, 43, 56),
                ["GridAlternateRowBrush"] = Color.FromRgb(40, 48, 62),
                ["GridHoverBrush"] = Color.FromRgb(50, 59, 78),
                ["GridSelectionBrush"] = Color.FromRgb(60, 67, 118),
                ["MenuHoverBrush"] = Color.FromRgb(50, 59, 78),
                ["ShadowColorBrush"] = Colors.Black
            })
        ];

        public static AppTheme GetTheme(string? key)
        {
            foreach (AppTheme theme in Themes)
            {
                if (string.Equals(theme.Key, key, StringComparison.OrdinalIgnoreCase))
                {
                    return theme;
                }
            }

            return Themes[0];
        }

        public static void ApplyTheme(string? key)
        {
            AppTheme theme = GetTheme(key);
            ResourceDictionary resources = Application.Current.Resources;

            foreach ((string resourceKey, Color color) in theme.Colors)
            {
                resources[resourceKey] = new SolidColorBrush(color);
            }

            if (theme.Colors.TryGetValue("ShadowColorBrush", out Color shadowColor))
            {
                resources["PanelShadow"] = new System.Windows.Media.Effects.DropShadowEffect
                {
                    BlurRadius = 24,
                    Direction = 270,
                    Opacity = theme.Key.StartsWith("Dark", StringComparison.OrdinalIgnoreCase) ? 0.32 : 0.12,
                    ShadowDepth = 10,
                    Color = shadowColor
                };
            }

            resources[SystemColors.MenuBrushKey] = new SolidColorBrush(theme.Colors["SurfaceBrush"]);
            resources[SystemColors.MenuHighlightBrushKey] = new SolidColorBrush(theme.Colors["MenuHoverBrush"]);
            resources[SystemColors.MenuTextBrushKey] = new SolidColorBrush(theme.Colors["InkBrush"]);
            resources[SystemColors.ControlBrushKey] = new SolidColorBrush(theme.Colors["SurfaceBrush"]);
            resources[SystemColors.ControlTextBrushKey] = new SolidColorBrush(theme.Colors["InkBrush"]);
            resources[SystemColors.WindowBrushKey] = new SolidColorBrush(theme.Colors["AppBackgroundBrush"]);
            resources[SystemColors.WindowTextBrushKey] = new SolidColorBrush(theme.Colors["InkBrush"]);
            resources[SystemColors.HighlightBrushKey] = new SolidColorBrush(theme.Colors["GridSelectionBrush"]);
            resources[SystemColors.HighlightTextBrushKey] = new SolidColorBrush(theme.Colors["InkBrush"]);
        }

        public static void SaveTheme(string key)
        {
            Properties.Settings.Default.ColorTheme = key;
            Properties.Settings.Default.Save();
        }
    }
}
