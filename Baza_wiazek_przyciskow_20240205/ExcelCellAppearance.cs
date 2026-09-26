using System.Windows;
using System.Windows.Media;

namespace Baza_wiazek_przyciskow_20240205;

public sealed class ExcelCellAppearance
{
    public string Background { get; init; } = "#FFFFFF";
    public bool UseThemeBackground => Background.Equals("#FFFFFF", System.StringComparison.OrdinalIgnoreCase) ||
                                      Background.Equals("#FFFFFFFF", System.StringComparison.OrdinalIgnoreCase);
    public string Foreground { get; init; } = "#000000";
    public string FontFamily { get; init; } = "Calibri";
    public double FontSize { get; init; } = 14.67;
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public bool Underline { get; init; }
    public TextAlignment Alignment { get; init; }
    public VerticalAlignment VerticalAlignment { get; init; } = VerticalAlignment.Center;
    public double Width { get; init; }
    public double Height { get; init; }
    public Thickness BorderThickness { get; init; }
    public string BorderColor { get; init; } = "#808080";

    public static Brush Brush(string color)
    {
        var brush = (SolidColorBrush)new BrushConverter().ConvertFromString(color)!;
        brush.Freeze();
        return brush;
    }
}
