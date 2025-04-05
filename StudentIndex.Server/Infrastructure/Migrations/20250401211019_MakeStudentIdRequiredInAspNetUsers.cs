using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentIndex.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeStudentIdRequiredInAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "AspNetUsers",
                nullable: false,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "AspNetUsers",
                nullable: true,
                oldNullable: false);
        }
    }
}
