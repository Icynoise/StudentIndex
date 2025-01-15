using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentIndex.Models;

public partial class StudentAplikacijaContext : DbContext
{
    public StudentAplikacijaContext()
    {
    }

    public StudentAplikacijaContext(DbContextOptions<StudentAplikacijaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Godine> Godines { get; set; }

    public virtual DbSet<Ispiti> Ispitis { get; set; }

    public virtual DbSet<OsnovneStudije> OsnovneStudijes { get; set; }

    public virtual DbSet<PokušajiIspitum> PokušajiIspita { get; set; }

    public virtual DbSet<Predmeti> Predmetis { get; set; }

    public virtual DbSet<PredmetiUprogramima> PredmetiUprogramimas { get; set; }

    public virtual DbSet<StudentIspiti> StudentIspitis { get; set; }

    public virtual DbSet<StudentStudijskiProgram> StudentStudijskiPrograms { get; set; }

    public virtual DbSet<Studenti> Studentis { get; set; }

    public virtual DbSet<StudijskiProgrami> StudijskiProgramis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ramicdj-LAP\\MSSQLSERVER01;Initial Catalog=StudentAplikacija;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

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

            entity.HasOne(d => d.StudijskiProgram).WithMany(p => p.PredmetiUprogramimas)
                .HasForeignKey(d => d.StudijskiProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PredmetiU__Studi__5165187F");
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
