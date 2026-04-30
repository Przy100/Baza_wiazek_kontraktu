using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class DocumentKindToBrushConverter : IValueConverter
    {
        private static readonly SolidColorBrush PlateBrush = new(Color.FromRgb(37, 99, 235));
        private static readonly SolidColorBrush WireBrush = new(Color.FromRgb(5, 150, 105));
        private static readonly SolidColorBrush DefaultBrush = new(Color.FromRgb(109, 40, 217));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string documentKind = value?.ToString() ?? string.Empty;

            if (documentKind.StartsWith("Płyta", StringComparison.OrdinalIgnoreCase))
            {
                return PlateBrush;
            }

            if (documentKind.StartsWith("Wiązka", StringComparison.OrdinalIgnoreCase))
            {
                return WireBrush;
            }

            return DefaultBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
