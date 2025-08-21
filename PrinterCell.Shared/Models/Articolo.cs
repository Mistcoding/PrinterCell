namespace PrinterCell.Shared.Models
{
    public class Articolo
    {
        public int Id { get; set; }
        public string Codice { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public int Quantita { get; set; }
    }
}
