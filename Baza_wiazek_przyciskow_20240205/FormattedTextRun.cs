namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class FormattedTextRun
    {
        public FormattedTextRun(string text, bool isStrikethrough)
        {
            Text = text;
            IsStrikethrough = isStrikethrough;
        }

        public string Text { get; }
        public bool IsStrikethrough { get; }
    }
}
