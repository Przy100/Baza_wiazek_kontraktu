using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class DocumentKindToBrushConverter : IValueConverter
    {
        private static readonly LinearGradientBrush PlateBrush = CreateGradient(Color.FromRgb(64, 130, 246), Color.FromRgb(31, 92, 218));
        private static readonly LinearGradientBrush WireBrush = CreateGradient(Color.FromRgb(18, 166, 119), Color.FromRgb(4, 132, 96));
        private static readonly LinearGradientBrush DefaultBrush = CreateGradient(Color.FromRgb(130, 83, 228), Color.FromRgb(100, 54, 190));

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

        private static LinearGradientBrush CreateGradient(Color startColor, Color endColor)
        {
            LinearGradientBrush brush = new()
            {
                StartPoint = new System.Windows.Point(0, 0),
                EndPoint = new System.Windows.Point(1, 1)
            };
            brush.GradientStops.Add(new GradientStop(startColor, 0));
            brush.GradientStops.Add(new GradientStop(endColor, 1));
            brush.Freeze();

            return brush;
        }
    }
}
