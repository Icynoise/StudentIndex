namespace StudentIndex.Server.Domain.Etities
{
    public partial class Semestri
    {
        public int SemestarId { get; set; }

        public string NazivSemestra { get; set; } = null!;

        public int BrojSemestra { get; set; }

        public virtual ICollection<PredmetiUprogramima> PredmetiUprogramimas { get; set; } = new List<PredmetiUprogramima>();
    }
}
