using Microsoft.EntityFrameworkCore.Migrations;

namespace IOASYS_IMDb.Migrations
{
    public partial class BaseInicial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voto_Usuario_UsuarioId1",
                table: "Voto");

            migrationBuilder.DropIndex(
                name: "IX_Voto_UsuarioId1",
                table: "Voto");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Voto");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Filme");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Voto",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Voto_UsuarioId",
                table: "Voto",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voto_Usuario_UsuarioId",
                table: "Voto",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voto_Usuario_UsuarioId",
                table: "Voto");

            migrationBuilder.DropIndex(
                name: "IX_Voto_UsuarioId",
                table: "Voto");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Voto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Voto",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Filme",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voto_UsuarioId1",
                table: "Voto",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Voto_Usuario_UsuarioId1",
                table: "Voto",
                column: "UsuarioId1",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
