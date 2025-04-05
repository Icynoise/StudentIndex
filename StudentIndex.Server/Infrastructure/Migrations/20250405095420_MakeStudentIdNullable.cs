using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentIndex.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeStudentIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RezultatIspita",
                table: "Studenti");

            migrationBuilder.DropColumn(
                name: "RezultatIspita",
                table: "Predmeti");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RezultatIspita",
                table: "Studenti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RezultatIspita",
                table: "Predmeti",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
