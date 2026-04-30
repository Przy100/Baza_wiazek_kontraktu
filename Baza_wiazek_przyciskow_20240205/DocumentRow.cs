namespace Baza_wiazek_przyciskow_20240205
{
    public sealed class DocumentRow
    {
        public int Number { get; init; }
        public string BteNumber { get; init; } = string.Empty;
        public string DocumentKind { get; init; } = string.Empty;
        public string SbcIndex { get; init; } = string.Empty;
        public string Quantity { get; init; } = string.Empty;
        public string Priority { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public string Revision { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Notes { get; init; } = string.Empty;
        public string LinkPath { get; init; } = string.Empty;
    }
}
