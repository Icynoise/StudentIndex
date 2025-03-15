using Microsoft.EntityFrameworkCore;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.Etities;


namespace StudentIndex.Server.Infrastructure.Data;

public partial class StudentAplikacijaContext : DbContext
{
    public StudentAplikacijaContext()
    {
    }

    public StudentAplikacijaContext(DbContextOptions<StudentAplikacijaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Godine> Godine { get; set; }

    public virtual DbSet<Ispiti> Ispiti { get; set; }

    public virtual DbSet<OsnovneStudije> OsnovneStudije { get; set; }

    public virtual DbSet<PokušajiIspitum> PokušajiIspita { get; set; }

    public virtual DbSet<Predmeti> Predmeti { get; set; }

    public virtual DbSet<PredmetiUprogramima> PredmetiUprogramima { get; set; }

    public virtual DbSet<Semestri> Semestri { get; set; }

    public virtual DbSet<StudentIspiti> StudentIspiti { get; set; }

    public virtual DbSet<StudentStudijskiProgram> StudentStudijskiProgram { get; set; }

    public virtual DbSet<Studenti> Studenti { get; set; }

    public virtual DbSet<StudijskiProgrami> StudijskiProgrami { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=RAMICDJ-LAP\\MSSQLSERVER01;Database=StudentAplikacija;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Godine>(entity =>
        {
            entity.HasKey(e => e.GodinaId).HasName("PK__Godine__83A9868C49691883");

            entity.ToTable("Godine");

            entity.Property(e => e.NazivGodine).HasMaxLength(20);
        });

        modelBuilder.Entity<Ispiti>(entity =>
        {
            entity.HasKey(e => e.IspitId).HasName("PK__Ispiti__2F2104AD5FBC6371");

            entity.ToTable("Ispiti");

            entity.Property(e => e.TipIspita).HasMaxLength(50);

            entity.HasOne(d => d.Predmet).WithMany(p => p.Ispitis)
                .HasForeignKey(d => d.PredmetId)
                .HasConstraintName("FK__Ispiti__PredmetI__5629CD9C");
        });

        modelBuilder.Entity<OsnovneStudije>(entity =>
        {
            entity.HasKey(e => e.FakultetId).HasName("PK__OsnovneS__7088CEBFF983D7FB");

            entity.ToTable("OsnovneStudije");

            entity.Property(e => e.Naziv).HasMaxLength(100);
            entity.Property(e => e.Opis).HasMaxLength(255);
        });

        modelBuilder.Entity<PokušajiIspitum>(entity =>
        {
            entity.HasKey(e => e.PokušajIspitaId).HasName("PK__Pokušaji__94A2E5F3372F2D48");

            entity.Property(e => e.RezultatIspita).HasMaxLength(20);

            entity.HasOne(d => d.StudentIspit).WithMany(p => p.PokušajiIspita)
                .HasForeignKey(d => d.StudentIspitId)
                .HasConstraintName("FK__PokušajiI__Stude__5CD6CB2B");
        });

        modelBuilder.Entity<Predmeti>(entity =>
        {
            entity.HasKey(e => e.PredmetId).HasName("PK__Predmeti__8EE1D625BFFACB53");

            entity.ToTable("Predmeti");

            entity.Property(e => e.Ects).HasColumnName("ECTS");
            entity.Property(e => e.Naziv).HasMaxLength(255);
            entity.Property(e => e.Opis).HasMaxLength(255);
        });

        modelBuilder.Entity<PredmetiUprogramima>(entity =>
        {
            entity.HasKey(e => e.PredmetUprogramuId).HasName("PK__Predmeti__0D0E70F1FD4FEC8A");

            entity.ToTable("PredmetiUProgramima");

            entity.HasIndex(e => new { e.StudijskiProgramId, e.PredmetId }, "UC_PredmetiUProgramima_StudijskiProgramId_PredmetId").IsUnique();

            entity.Property(e => e.PredmetUprogramuId).HasColumnName("PredmetUProgramuId");

            entity.HasOne(d => d.Godina).WithMany(p => p.PredmetiUprogramimas)
                .HasForeignKey(d => d.GodinaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PredmetiU__Godin__534D60F1");

            entity.HasOne(d => d.Predmet).WithMany(p => p.PredmetiUprogramimas)
                .HasForeignKey(d => d.PredmetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PredmetiU__Predm__52593CB8");

            entity.HasOne(d => d.Semestar).WithMany(p => p.PredmetiUprogramimas)
                .HasForeignKey(d => d.SemestarId)
                .HasConstraintName("FK_PredmetiUProgramima_Semestri");

            entity.HasOne(d => d.StudijskiProgram).WithMany(p => p.PredmetiUprogramimas)
                .HasForeignKey(d => d.StudijskiProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PredmetiU__Studi__5165187F");
        });

        modelBuilder.Entity<Semestri>(entity =>
        {
            entity.HasKey(e => e.SemestarId).HasName("PK__Semestri__8D4B7201649ECD86");

            entity.ToTable("Semestri");

            entity.Property(e => e.NazivSemestra).HasMaxLength(100);
        });

        modelBuilder.Entity<StudentIspiti>(entity =>
        {
            entity.HasKey(e => e.StudentIspitId).HasName("PK__StudentI__BD14C2F8BC9C8773");

            entity.ToTable("StudentIspiti");

            entity.Property(e => e.RezultatIspita).HasMaxLength(20);

            entity.HasOne(d => d.Ispit).WithMany(p => p.StudentIspitis)
                .HasForeignKey(d => d.IspitId)
                .HasConstraintName("FK__StudentIs__Ispit__59FA5E80");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentIspitis)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__StudentIs__Stude__59063A47");
        });

        modelBuilder.Entity<StudentStudijskiProgram>(entity =>
        {
            entity.HasKey(e => e.StudentStudijskiProgramId).HasName("PK__StudentS__36668DF3D761B9AA");

            entity.ToTable("StudentStudijskiProgram");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentStudijskiPrograms)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__StudentSt__Stude__4D94879B");

            entity.HasOne(d => d.StudijskiProgram).WithMany(p => p.StudentStudijskiPrograms)
                .HasForeignKey(d => d.StudijskiProgramId)
                .HasConstraintName("FK__StudentSt__Studi__4E88ABD4");
        });

        modelBuilder.Entity<Studenti>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Studenti__32C52B99C4BFFF0D");

            entity.ToTable("Studenti");

            entity.HasIndex(e => e.Email, "UQ__Studenti__A9D10534405033B8").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Ime).HasMaxLength(100);
            entity.Property(e => e.Prezime).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Telefon).HasMaxLength(15);
        });

        modelBuilder.Entity<StudijskiProgrami>(entity =>
        {
            entity.HasKey(e => e.StudijskiProgramId).HasName("PK__Studijsk__0D5A63A45B51391D");

            entity.ToTable("StudijskiProgrami");

            entity.Property(e => e.Naziv).HasMaxLength(100);
            entity.Property(e => e.Opis).HasMaxLength(255);

            entity.HasOne(d => d.Fakultet).WithMany(p => p.StudijskiProgramis)
                .HasForeignKey(d => d.FakultetId)
                .HasConstraintName("FK__Studijski__Fakul__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
