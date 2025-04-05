namespace StudentIndex.Server.Domain.DTOs
{
    public class DostupniIspitiDto
    {
        public int IspitId { get; set; }
        public string? PredmetNaziv { get; set; }
        public DateOnly DatumIspita { get; set; }
    }
}
