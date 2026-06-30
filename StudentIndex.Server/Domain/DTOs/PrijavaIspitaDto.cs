namespace StudentIndex.Server.Domain.DTOs
{
    public class PrijavaIspitaDto
    {
        public DateTime TodaysDate { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? BrojIndexa { get; set; }
        public string? StudijskiProgramNaziv { get; set; }
    }
}
