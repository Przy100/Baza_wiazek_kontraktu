using System.Collections.Generic;
using System.Linq;

namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class FormattedTextValue
    {
        public FormattedTextValue(string text, IEnumerable<FormattedTextRun> runs)
        {
            Text = text;
            Runs = runs.ToArray();
        }

        public string Text { get; }
        public IReadOnlyList<FormattedTextRun> Runs { get; }

        public static FormattedTextValue FromPlainText(string text)
        {
            return new FormattedTextValue(text, [new FormattedTextRun(text, false)]);
        }
    }
}
