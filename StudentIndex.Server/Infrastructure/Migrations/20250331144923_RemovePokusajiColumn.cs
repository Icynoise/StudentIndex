using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentIndex.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePokusajiColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "Pokušaji",
            table: "StudentIspiti");

            migrationBuilder.AddColumn<string>(
            name: "Status",
            table: "StudentIspiti",
            type: "nvarchar(50)",
            nullable: false,
            defaultValue: "Na Cekanju");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
