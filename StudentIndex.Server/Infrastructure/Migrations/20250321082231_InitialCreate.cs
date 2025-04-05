using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentIndex.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Godine",
                columns: table => new
                {
                    GodinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivGodine = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Godine__83A9868C49691883", x => x.GodinaId);
                });

            migrationBuilder.CreateTable(
                name: "OsnovneStudije",
                columns: table => new
                {
                    FakultetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OsnovneS__7088CEBFF983D7FB", x => x.FakultetId);
                });

            migrationBuilder.CreateTable(
                name: "Predmeti",
                columns: table => new
                {
                    PredmetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ECTS = table.Column<short>(type: "smallint", nullable: false),
                    RezultatIspita = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Predmeti__8EE1D625BFFACB53", x => x.PredmetId);
                });

            migrationBuilder.CreateTable(
                name: "Semestri",
                columns: table => new
                {
                    SemestarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivSemestra = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BrojSemestra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Semestri__8D4B7201649ECD86", x => x.SemestarId);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DatumRođenja = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RezultatIspita = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Studenti__32C52B99C4BFFF0D", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "StudijskiProgrami",
                columns: table => new
                {
                    StudijskiProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FakultetId = table.Column<int>(type: "int", nullable: true),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Studijsk__0D5A63A45B51391D", x => x.StudijskiProgramId);
                    table.ForeignKey(
                        name: "FK__Studijski__Fakul__3F466844",
                        column: x => x.FakultetId,
                        principalTable: "OsnovneStudije",
                        principalColumn: "FakultetId");
                });

            migrationBuilder.CreateTable(
                name: "Ispiti",
                columns: table => new
                {
                    IspitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PredmetId = table.Column<int>(type: "int", nullable: true),
                    DatumIspita = table.Column<DateOnly>(type: "date", nullable: true),
                    TipIspita = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ispiti__2F2104AD5FBC6371", x => x.IspitId);
                    table.ForeignKey(
                        name: "FK__Ispiti__PredmetI__5629CD9C",
                        column: x => x.PredmetId,
                        principalTable: "Predmeti",
                        principalColumn: "PredmetId");
                });

            migrationBuilder.CreateTable(
                name: "PredmetiUProgramima",
                columns: table => new
                {
                    PredmetUProgramuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudijskiProgramId = table.Column<int>(type: "int", nullable: false),
                    PredmetId = table.Column<int>(type: "int", nullable: false),
                    GodinaId = table.Column<int>(type: "int", nullable: false),
                    SemestarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Predmeti__0D0E70F1FD4FEC8A", x => x.PredmetUProgramuId);
                    table.ForeignKey(
                        name: "FK_PredmetiUProgramima_Semestri",
                        column: x => x.SemestarId,
                        principalTable: "Semestri",
                        principalColumn: "SemestarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PredmetiU__Godin__534D60F1",
                        column: x => x.GodinaId,
                        principalTable: "Godine",
                        principalColumn: "GodinaId");
                    table.ForeignKey(
                        name: "FK__PredmetiU__Predm__52593CB8",
                        column: x => x.PredmetId,
                        principalTable: "Predmeti",
                        principalColumn: "PredmetId");
                    table.ForeignKey(
                        name: "FK__PredmetiU__Studi__5165187F",
                        column: x => x.StudijskiProgramId,
                        principalTable: "StudijskiProgrami",
                        principalColumn: "StudijskiProgramId");
                });

            migrationBuilder.CreateTable(
                name: "StudentStudijskiProgram",
                columns: table => new
                {
                    StudentStudijskiProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    StudijskiProgramId = table.Column<int>(type: "int", nullable: true),
                    DatumPočetka = table.Column<DateOnly>(type: "date", nullable: true),
                    DatumZavršetka = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentS__36668DF3D761B9AA", x => x.StudentStudijskiProgramId);
                    table.ForeignKey(
                        name: "FK__StudentSt__Stude__4D94879B",
                        column: x => x.StudentId,
                        principalTable: "Studenti",
                        principalColumn: "StudentId");
                    table.ForeignKey(
                        name: "FK__StudentSt__Studi__4E88ABD4",
                        column: x => x.StudijskiProgramId,
                        principalTable: "StudijskiProgrami",
                        principalColumn: "StudijskiProgramId");
                });

            migrationBuilder.CreateTable(
                name: "StudentIspiti",
                columns: table => new
                {
                    StudentIspitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    IspitId = table.Column<int>(type: "int", nullable: true),
                    RezultatIspita = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Pokušaji = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StudentI__BD14C2F8BC9C8773", x => x.StudentIspitId);
                    table.ForeignKey(
                        name: "FK__StudentIs__Ispit__59FA5E80",
                        column: x => x.IspitId,
                        principalTable: "Ispiti",
                        principalColumn: "IspitId");
                    table.ForeignKey(
                        name: "FK__StudentIs__Stude__59063A47",
                        column: x => x.StudentId,
                        principalTable: "Studenti",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "PokušajiIspita",
                columns: table => new
                {
                    PokušajIspitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentIspitId = table.Column<int>(type: "int", nullable: true),
                    DatumPokušaja = table.Column<DateOnly>(type: "date", nullable: true),
                    RezultatIspita = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pokušaji__94A2E5F3372F2D48", x => x.PokušajIspitaId);
                    table.ForeignKey(
                        name: "FK__PokušajiI__Stude__5CD6CB2B",
                        column: x => x.StudentIspitId,
                        principalTable: "StudentIspiti",
                        principalColumn: "StudentIspitId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ispiti_PredmetId",
                table: "Ispiti",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_PokušajiIspita_StudentIspitId",
                table: "PokušajiIspita",
                column: "StudentIspitId");

            migrationBuilder.CreateIndex(
                name: "IX_PredmetiUProgramima_GodinaId",
                table: "PredmetiUProgramima",
                column: "GodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_PredmetiUProgramima_PredmetId",
                table: "PredmetiUProgramima",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_PredmetiUProgramima_SemestarId",
                table: "PredmetiUProgramima",
                column: "SemestarId");

            migrationBuilder.CreateIndex(
                name: "UC_PredmetiUProgramima_StudijskiProgramId_PredmetId",
                table: "PredmetiUProgramima",
                columns: new[] { "StudijskiProgramId", "PredmetId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Studenti__A9D10534405033B8",
                table: "Studenti",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIspiti_IspitId",
                table: "StudentIspiti",
                column: "IspitId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIspiti_StudentId",
                table: "StudentIspiti",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudijskiProgram_StudentId",
                table: "StudentStudijskiProgram",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudijskiProgram_StudijskiProgramId",
                table: "StudentStudijskiProgram",
                column: "StudijskiProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudijskiProgrami_FakultetId",
                table: "StudijskiProgrami",
                column: "FakultetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokušajiIspita");

            migrationBuilder.DropTable(
                name: "PredmetiUProgramima");

            migrationBuilder.DropTable(
                name: "StudentStudijskiProgram");

            migrationBuilder.DropTable(
                name: "StudentIspiti");

            migrationBuilder.DropTable(
                name: "Semestri");

            migrationBuilder.DropTable(
                name: "Godine");

            migrationBuilder.DropTable(
                name: "StudijskiProgrami");

            migrationBuilder.DropTable(
                name: "Ispiti");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "OsnovneStudije");

            migrationBuilder.DropTable(
                name: "Predmeti");
        }
    }
}
