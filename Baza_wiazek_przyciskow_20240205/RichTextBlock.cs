using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class RichTextBlock : TextBlock
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(FormattedTextValue),
                typeof(RichTextBlock),
                new PropertyMetadata(null, OnValueChanged));

        public FormattedTextValue? Value
        {
            get => (FormattedTextValue?)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is RichTextBlock richTextBlock)
            {
                richTextBlock.RenderRuns();
            }
        }

        private void RenderRuns()
        {
            Inlines.Clear();

            if (Value == null)
            {
                return;
            }

            foreach (FormattedTextRun formattedRun in Value.Runs)
            {
                AddRun(formattedRun);
            }
        }

        private void AddRun(FormattedTextRun formattedRun)
        {
            string[] lines = formattedRun.Text.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (i > 0)
                {
                    Inlines.Add(new LineBreak());
                }

                if (lines[i].Length == 0)
                {
                    continue;
                }

                Run run = new(lines[i]);
                if (formattedRun.IsStrikethrough)
                {
                    run.TextDecorations = System.Windows.TextDecorations.Strikethrough;
                }

                Inlines.Add(run);
            }
        }
    }
}
