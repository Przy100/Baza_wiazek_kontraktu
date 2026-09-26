using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Baza_wiazek_przyciskow_20240205;

public sealed class ExcelBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is string color ? ExcelCellAppearance.Brush(color) : DependencyProperty.UnsetValue;
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
