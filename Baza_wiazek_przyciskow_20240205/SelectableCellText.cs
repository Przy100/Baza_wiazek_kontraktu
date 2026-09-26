using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Baza_wiazek_przyciskow_20240205;

public sealed class SelectableCellText : RichTextBox
{
    public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
        nameof(TextAlignment), typeof(TextAlignment), typeof(SelectableCellText),
        new PropertyMetadata(TextAlignment.Left, OnAppearanceChanged));
    public TextAlignment TextAlignment
    {
        get => (TextAlignment)GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }
    public static readonly DependencyProperty ExcelViewProperty = DependencyProperty.RegisterAttached(
        "ExcelView", typeof(bool), typeof(SelectableCellText),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnAppearanceChanged));
    public static bool GetExcelView(DependencyObject obj) => (bool)obj.GetValue(ExcelViewProperty);
    public static void SetExcelView(DependencyObject obj, bool value) => obj.SetValue(ExcelViewProperty, value);

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value), typeof(FormattedTextValue), typeof(SelectableCellText),
        new PropertyMetadata(null, OnAppearanceChanged));
    public FormattedTextValue? Value
    {
        get => (FormattedTextValue?)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public SelectableCellText()
    {
        IsReadOnly = true;
        IsReadOnlyCaretVisible = true;
        IsUndoEnabled = false;
        AcceptsTab = false;
        Background = Brushes.Transparent;
        BorderThickness = new Thickness(0);
        Padding = new Thickness(0);
        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        Cursor = Cursors.IBeam;
        FontFamily = new FontFamily("Segoe UI");
        FontSize = 13;
        SetResourceReference(ForegroundProperty, "InkBrush");
        var menu = new ContextMenu();
        menu.Items.Add(new MenuItem { Header = "Kopiuj tekst", Command = ApplicationCommands.Copy, CommandTarget = this });
        menu.Items.Add(new MenuItem { Header = "Zaznacz cały tekst", Command = ApplicationCommands.SelectAll, CommandTarget = this });
        ContextMenu = menu;
    }

    private static void OnAppearanceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        if (obj is SelectableCellText text) text.RenderText();
    }

    private void RenderText()
    {
        var paragraph = new Paragraph { Margin = new Thickness(0) };
        var appearance = GetExcelView(this) ? Value?.Appearance : null;
        SetCurrentValue(VerticalAlignmentProperty, appearance?.VerticalAlignment ?? VerticalAlignment.Center);
        paragraph.TextAlignment = appearance?.Alignment ?? TextAlignment;
        foreach (var formatted in Value?.Runs ?? [])
        {
            var run = new Run(formatted.Text.Replace("\r\n", "\n").Replace('\r', '\n'));
            var font = GetExcelView(this) ? formatted.Appearance ?? appearance : null;
            if (font != null)
            {
                run.FontFamily = new FontFamily(font.FontFamily);
                run.FontSize = font.FontSize;
                run.FontWeight = font.Bold ? FontWeights.Bold : FontWeights.Normal;
                run.FontStyle = font.Italic ? FontStyles.Italic : FontStyles.Normal;
                run.Foreground = ExcelCellAppearance.Brush(font.Foreground);
                if (appearance?.UseThemeBackground == true && run.Foreground is SolidColorBrush ink &&
                    ink.Color.R == ink.Color.G && ink.Color.G == ink.Color.B && ink.Color.R < 128)
                    run.SetResourceReference(TextElement.ForegroundProperty, "InkBrush");
            }
            var decorations = new TextDecorationCollection();
            if (formatted.IsStrikethrough) decorations.Add(TextDecorations.Strikethrough);
            if (font?.Underline == true) decorations.Add(TextDecorations.Underline);
            run.TextDecorations = decorations;
            paragraph.Inlines.Add(run);
        }
        Document = new FlowDocument(paragraph) { PagePadding = new Thickness(0), FontFamily = FontFamily, FontSize = FontSize, FontWeight = FontWeight };
    }
}
