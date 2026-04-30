using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class RevisionToBrushConverter : IValueConverter
    {
        private static readonly SolidColorBrush EmptyBrush = CreateBrush(0, 0, 0, 0);
        private static readonly SolidColorBrush[] RevisionBrushes =
        [
            CreateBrush(96, 165, 250, 0.85),
            CreateBrush(52, 211, 153, 0.85),
            CreateBrush(251, 191, 36, 0.85),
            CreateBrush(248, 113, 113, 0.85),
            CreateBrush(167, 139, 250, 0.85),
            CreateBrush(45, 212, 191, 0.85),
            CreateBrush(251, 146, 60, 0.85),
            CreateBrush(244, 114, 182, 0.85)
        ];

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string revision = NormalizeRevision(value?.ToString());
            if (revision.Length == 0)
            {
                return EmptyBrush;
            }

            int index = 0;
            foreach (char character in revision)
            {
                index = (index * 31) + character;
            }

            return RevisionBrushes[Math.Abs(index) % RevisionBrushes.Length];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private static string NormalizeRevision(string? revision)
        {
            return string.IsNullOrWhiteSpace(revision) ? string.Empty : revision.Trim().ToUpperInvariant();
        }

        private static SolidColorBrush CreateBrush(byte red, byte green, byte blue, double opacity)
        {
            SolidColorBrush brush = new(Color.FromRgb(red, green, blue))
            {
                Opacity = opacity
            };
            brush.Freeze();

            return brush;
        }
    }
}
