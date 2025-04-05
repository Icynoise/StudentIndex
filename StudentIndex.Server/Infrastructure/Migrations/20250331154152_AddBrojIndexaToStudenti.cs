using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentIndex.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBrojIndexaToStudenti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "BrojIndexa",
            table: "Studenti",
            nullable: false,
            defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
