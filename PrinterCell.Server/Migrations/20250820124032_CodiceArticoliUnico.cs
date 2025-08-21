using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrinterCell.Server.Migrations
{
    /// <inheritdoc />
    public partial class CodiceArticoliUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Codice",
                table: "Articolo",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Articolo_Codice",
                table: "Articolo",
                column: "Codice",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articolo_Codice",
                table: "Articolo");

            migrationBuilder.AlterColumn<string>(
                name: "Codice",
                table: "Articolo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
