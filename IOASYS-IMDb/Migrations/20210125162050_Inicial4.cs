using Microsoft.EntityFrameworkCore.Migrations;

namespace IOASYS_IMDb.Migrations
{
    public partial class Inicial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Voto",
                table: "Filme");

            migrationBuilder.AddColumn<int>(
                name: "Nota",
                table: "Votacao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nota",
                table: "Votacao");

            migrationBuilder.AddColumn<int>(
                name: "Voto",
                table: "Filme",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
