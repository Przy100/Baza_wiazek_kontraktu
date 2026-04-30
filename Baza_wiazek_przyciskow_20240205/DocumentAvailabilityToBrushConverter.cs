using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class DocumentAvailabilityToBrushConverter : IValueConverter
    {
        private static readonly SolidColorBrush FoundBrush = new(Color.FromRgb(5, 150, 105));
        private static readonly SolidColorBrush MissingBrush = new(Color.FromRgb(220, 38, 38));
        private static readonly SolidColorBrush AccessDeniedBrush = new(Color.FromRgb(217, 119, 6));
        private static readonly SolidColorBrush ErrorBrush = new(Color.FromRgb(109, 40, 217));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string availability = value?.ToString() ?? string.Empty;

            if (availability.StartsWith("Znaleziono", StringComparison.OrdinalIgnoreCase))
            {
                return FoundBrush;
            }

            if (availability.Equals("Brak", StringComparison.OrdinalIgnoreCase))
            {
                return MissingBrush;
            }

            if (availability.Equals("Brak dostępu", StringComparison.OrdinalIgnoreCase))
            {
                return AccessDeniedBrush;
            }

            return ErrorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
