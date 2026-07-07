using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentIndex.Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixIspitiSchemaDrift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Usklađivanje sa stvarnim stanjem baze (drift iz obrisanih migracija) —
            // sve operacije su idempotentne da bi migracija radila i na bazi koja je
            // već u ciljnom stanju i na svježoj bazi kreiranoj iz migracija.
            migrationBuilder.Sql(@"
IF COL_LENGTH('Ispiti', 'TipIspita') IS NOT NULL
    ALTER TABLE [Ispiti] DROP COLUMN [TipIspita];");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
           WHERE TABLE_NAME = 'Ispiti' AND COLUMN_NAME = 'DatumIspita'
             AND (DATA_TYPE <> 'datetime2' OR IS_NULLABLE = 'YES'))
BEGIN
    UPDATE [Ispiti] SET [DatumIspita] = '0001-01-01' WHERE [DatumIspita] IS NULL;
    ALTER TABLE [Ispiti] ALTER COLUMN [DatumIspita] datetime2 NOT NULL;
END");

            migrationBuilder.Sql(@"
IF COL_LENGTH('Ispiti', 'RokZaRegistraciju') IS NULL
    ALTER TABLE [Ispiti] ADD [RokZaRegistraciju] datetime2 NULL;");

            migrationBuilder.Sql(@"
IF COL_LENGTH('Ispiti', 'Rokovi') IS NULL
    ALTER TABLE [Ispiti] ADD [Rokovi] int NOT NULL DEFAULT 0;");

            migrationBuilder.Sql(@"
IF COL_LENGTH('Ispiti', 'Status') IS NULL
    ALTER TABLE [Ispiti] ADD [Status] nvarchar(20) NOT NULL DEFAULT N'Open';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RokZaRegistraciju",
                table: "Ispiti");

            migrationBuilder.DropColumn(
                name: "Rokovi",
                table: "Ispiti");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ispiti");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DatumIspita",
                table: "Ispiti",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "TipIspita",
                table: "Ispiti",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
