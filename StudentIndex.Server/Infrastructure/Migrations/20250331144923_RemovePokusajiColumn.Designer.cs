﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentIndex.Server.Infrastructure.Data;

#nullable disable

namespace StudentIndex.Server.Infrastructure.Migrations
{
    [DbContext(typeof(StudentAplikacijaContext))]
    [Migration("20250331144923_RemovePokusajiColumn")]
    partial class RemovePokusajiColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Etities.Semestri", b =>
                {
                    b.Property<int>("SemestarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SemestarId"));

                    b.Property<int>("BrojSemestra")
                        .HasColumnType("int");

                    b.Property<string>("NazivSemestra")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SemestarId")
                        .HasName("PK__Semestri__8D4B7201649ECD86");

                    b.ToTable("Semestri", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Godine", b =>
                {
                    b.Property<int>("GodinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GodinaId"));

                    b.Property<string>("NazivGodine")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("GodinaId")
                        .HasName("PK__Godine__83A9868C49691883");

                    b.ToTable("Godine", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Ispiti", b =>
                {
                    b.Property<int>("IspitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IspitId"));

                    b.Property<DateOnly?>("DatumIspita")
                        .HasColumnType("date");

                    b.Property<int?>("PredmetId")
                        .HasColumnType("int");

                    b.Property<string>("TipIspita")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IspitId")
                        .HasName("PK__Ispiti__2F2104AD5FBC6371");

                    b.HasIndex("PredmetId");

                    b.ToTable("Ispiti", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.OsnovneStudije", b =>
                {
                    b.Property<int>("FakultetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FakultetId"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Opis")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("FakultetId")
                        .HasName("PK__OsnovneS__7088CEBFF983D7FB");

                    b.ToTable("OsnovneStudije", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.PokušajiIspitum", b =>
                {
                    b.Property<int>("PokušajIspitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PokušajIspitaId"));

                    b.Property<DateOnly?>("DatumPokušaja")
                        .HasColumnType("date");

                    b.Property<string>("RezultatIspita")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("StudentIspitId")
                        .HasColumnType("int");

                    b.HasKey("PokušajIspitaId")
                        .HasName("PK__Pokušaji__94A2E5F3372F2D48");

                    b.HasIndex("StudentIspitId");

                    b.ToTable("PokušajiIspita");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Predmeti", b =>
                {
                    b.Property<int>("PredmetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PredmetId"));

                    b.Property<short>("Ects")
                        .HasColumnType("smallint")
                        .HasColumnName("ECTS");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RezultatIspita")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PredmetId")
                        .HasName("PK__Predmeti__8EE1D625BFFACB53");

                    b.ToTable("Predmeti", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.PredmetiUprogramima", b =>
                {
                    b.Property<int>("PredmetUprogramuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PredmetUProgramuId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PredmetUprogramuId"));

                    b.Property<int>("GodinaId")
                        .HasColumnType("int");

                    b.Property<int>("PredmetId")
                        .HasColumnType("int");

                    b.Property<int>("SemestarId")
                        .HasColumnType("int");

                    b.Property<int>("StudijskiProgramId")
                        .HasColumnType("int");

                    b.HasKey("PredmetUprogramuId")
                        .HasName("PK__Predmeti__0D0E70F1FD4FEC8A");

                    b.HasIndex("GodinaId");

                    b.HasIndex("PredmetId");

                    b.HasIndex("SemestarId");

                    b.HasIndex(new[] { "StudijskiProgramId", "PredmetId" }, "UC_PredmetiUProgramima_StudijskiProgramId_PredmetId")
                        .IsUnique();

                    b.ToTable("PredmetiUProgramima", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.StudentIspiti", b =>
                {
                    b.Property<int>("StudentIspitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentIspitId"));

                    b.Property<int?>("IspitId")
                        .HasColumnType("int");

                    b.Property<int?>("Pokušaji")
                        .HasColumnType("int");

                    b.Property<string>("RezultatIspita")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("StudentIspitId")
                        .HasName("PK__StudentI__BD14C2F8BC9C8773");

                    b.HasIndex("IspitId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentIspiti", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.StudentStudijskiProgram", b =>
                {
                    b.Property<int>("StudentStudijskiProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentStudijskiProgramId"));

                    b.Property<DateOnly?>("DatumPočetka")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DatumZavršetka")
                        .HasColumnType("date");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("StudijskiProgramId")
                        .HasColumnType("int");

                    b.HasKey("StudentStudijskiProgramId")
                        .HasName("PK__StudentS__36668DF3D761B9AA");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudijskiProgramId");

                    b.ToTable("StudentStudijskiProgram", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Studenti", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<DateOnly>("DatumRođenja")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RezultatIspita")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Telefon")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("StudentId")
                        .HasName("PK__Studenti__32C52B99C4BFFF0D");

                    b.HasIndex(new[] { "Email" }, "UQ__Studenti__A9D10534405033B8")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Studenti", (string)null);
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.StudijskiProgrami", b =>
                {
                    b.Property<int>("StudijskiProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudijskiProgramId"));

                    b.Property<int?>("FakultetId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Opis")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("StudijskiProgramId")
                        .HasName("PK__Studijsk__0D5A63A45B51391D");

                    b.HasIndex("FakultetId");

                    b.ToTable("StudijskiProgrami", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Ispiti", b =>
                {
                    b.HasOne("StudentIndex.Server.Domain.Predmeti", "Predmet")
                        .WithMany("Ispitis")
                        .HasForeignKey("PredmetId")
                        .HasConstraintName("FK__Ispiti__PredmetI__5629CD9C");

                    b.Navigation("Predmet");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.PokušajiIspitum", b =>
                {
                    b.HasOne("StudentIndex.Server.Domain.StudentIspiti", "StudentIspit")
                        .WithMany("PokušajiIspita")
                        .HasForeignKey("StudentIspitId")
                        .HasConstraintName("FK__PokušajiI__Stude__5CD6CB2B");

                    b.Navigation("StudentIspit");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.PredmetiUprogramima", b =>
                {
                    b.HasOne("StudentIndex.Server.Domain.Godine", "Godina")
                        .WithMany("PredmetiUprogramimas")
                        .HasForeignKey("GodinaId")
                        .IsRequired()
                        .HasConstraintName("FK__PredmetiU__Godin__534D60F1");

                    b.HasOne("StudentIndex.Server.Domain.Predmeti", "Predmet")
                        .WithMany("PredmetiUprogramimas")
                        .HasForeignKey("PredmetId")
                        .IsRequired()
                        .HasConstraintName("FK__PredmetiU__Predm__52593CB8");

                    b.HasOne("StudentIndex.Server.Domain.Etities.Semestri", "Semestar")
                        .WithMany("PredmetiUprogramimas")
                        .HasForeignKey("SemestarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PredmetiUProgramima_Semestri");

                    b.HasOne("StudentIndex.Server.Domain.StudijskiProgrami", "StudijskiProgram")
                        .WithMany("PredmetiUprogramimas")
                        .HasForeignKey("StudijskiProgramId")
                        .IsRequired()
                        .HasConstraintName("FK__PredmetiU__Studi__5165187F");

                    b.Navigation("Godina");

                    b.Navigation("Predmet");

                    b.Navigation("Semestar");

                    b.Navigation("StudijskiProgram");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.StudentIspiti", b =>
                {
                    b.HasOne("StudentIndex.Server.Domain.Ispiti", "Ispit")
                        .WithMany("StudentIspitis")
                        .HasForeignKey("IspitId")
                        .HasConstraintName("FK__StudentIs__Ispit__59FA5E80");

                    b.HasOne("StudentIndex.Server.Domain.Studenti", "Student")
                        .WithMany("StudentIspitis")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__StudentIs__Stude__59063A47");

                    b.Navigation("Ispit");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.StudentStudijskiProgram", b =>
                {
                    b.HasOne("StudentIndex.Server.Domain.Studenti", "Student")
                        .WithMany("StudentStudijskiPrograms")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__StudentSt__Stude__4D94879B");

                    b.HasOne("StudentIndex.Server.Domain.StudijskiProgrami", "StudijskiProgram")
                        .WithMany("StudentStudijskiPrograms")
                        .HasForeignKey("StudijskiProgramId")
                        .HasConstraintName("FK__StudentSt__Studi__4E88ABD4");

                    b.Navigation("Student");

                    b.Navigation("StudijskiProgram");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.StudijskiProgrami", b =>
                {
                    b.HasOne("StudentIndex.Server.Domain.OsnovneStudije", "Fakultet")
                        .WithMany("StudijskiProgramis")
                        .HasForeignKey("FakultetId")
                        .HasConstraintName("FK__Studijski__Fakul__3F466844");

                    b.Navigation("Fakultet");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Etities.Semestri", b =>
                {
                    b.Navigation("PredmetiUprogramimas");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Godine", b =>
                {
                    b.Navigation("PredmetiUprogramimas");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Ispiti", b =>
                {
                    b.Navigation("StudentIspitis");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.OsnovneStudije", b =>
                {
                    b.Navigation("StudijskiProgramis");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Predmeti", b =>
                {
                    b.Navigation("Ispitis");

                    b.Navigation("PredmetiUprogramimas");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.StudentIspiti", b =>
                {
                    b.Navigation("PokušajiIspita");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.Studenti", b =>
                {
                    b.Navigation("StudentIspitis");

                    b.Navigation("StudentStudijskiPrograms");
                });

            modelBuilder.Entity("StudentIndex.Server.Domain.StudijskiProgrami", b =>
                {
                    b.Navigation("PredmetiUprogramimas");

                    b.Navigation("StudentStudijskiPrograms");
                });
#pragma warning restore 612, 618
        }
    }
}
