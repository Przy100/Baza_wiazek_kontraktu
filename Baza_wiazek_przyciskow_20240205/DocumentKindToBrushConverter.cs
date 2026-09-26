using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class DocumentKindToBrushConverter : IValueConverter
    {
        private static readonly LinearGradientBrush PlateBrush = CreateGradient(Color.FromRgb(70, 124, 188), Color.FromRgb(56, 106, 168));
        private static readonly LinearGradientBrush WireBrush = CreateGradient(Color.FromRgb(37, 139, 117), Color.FromRgb(32, 120, 100));
        private static readonly LinearGradientBrush DefaultBrush = CreateGradient(Color.FromRgb(113, 119, 132), Color.FromRgb(94, 101, 114));

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
