namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class DocumentRow
    {
        public int Number { get; init; }
        public string BteNumber { get; init; } = string.Empty;
        public FormattedTextValue BteNumberText { get; init; } = FormattedTextValue.FromPlainText(string.Empty);
        public string DocumentKind { get; init; } = string.Empty;
        public FormattedTextValue DocumentKindText { get; init; } = FormattedTextValue.FromPlainText(string.Empty);
        public string SbcIndex { get; init; } = string.Empty;
        public FormattedTextValue SbcIndexText { get; init; } = FormattedTextValue.FromPlainText(string.Empty);
        public string Quantity { get; init; } = string.Empty;
        public FormattedTextValue QuantityText { get; init; } = FormattedTextValue.FromPlainText(string.Empty);
        public string Priority { get; init; } = string.Empty;
        public FormattedTextValue PriorityText { get; init; } = FormattedTextValue.FromPlainText(string.Empty);
        public string Status { get; init; } = string.Empty;
        public FormattedTextValue StatusText { get; init; } = FormattedTextValue.FromPlainText(string.Empty);
        public string Revision { get; init; } = string.Empty;
        public FormattedTextValue RevisionText { get; init; } = FormattedTextValue.FromPlainText(string.Empty);
        public string Description { get; init; } = string.Empty;
        public FormattedTextValue DescriptionText { get; init; } = FormattedTextValue.FromPlainText(string.Empty);
        public string Notes { get; init; } = string.Empty;
        public FormattedTextValue NotesText { get; init; } = FormattedTextValue.FromPlainText(string.Empty);
        public string LinkPath { get; init; } = string.Empty;
        public string BaseLinkPath { get; init; } = string.Empty;
        public string DocumentAvailability { get; init; } = string.Empty;
        public string DocumentAvailabilityDetails { get; init; } = string.Empty;
        public string[] CheckedPaths { get; init; } = [];
        public bool CanOpenDocument { get; init; }
    }
}
