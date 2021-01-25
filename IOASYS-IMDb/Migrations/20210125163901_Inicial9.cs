using Microsoft.EntityFrameworkCore.Migrations;

namespace IOASYS_IMDb.Migrations
{
    public partial class Inicial9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votacao_Filme_FilmeId",
                table: "Votacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Votacao_Usuario_UsuarioId1",
                table: "Votacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votacao",
                table: "Votacao");

            migrationBuilder.RenameTable(
                name: "Votacao",
                newName: "Voto");

            migrationBuilder.RenameIndex(
                name: "IX_Votacao_UsuarioId1",
                table: "Voto",
                newName: "IX_Voto_UsuarioId1");

            migrationBuilder.RenameIndex(
                name: "IX_Votacao_FilmeId",
                table: "Voto",
                newName: "IX_Voto_FilmeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voto",
                table: "Voto",
                column: "VotacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voto_Filme_FilmeId",
                table: "Voto",
                column: "FilmeId",
                principalTable: "Filme",
                principalColumn: "FilmeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Voto_Usuario_UsuarioId1",
                table: "Voto",
                column: "UsuarioId1",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voto_Filme_FilmeId",
                table: "Voto");

            migrationBuilder.DropForeignKey(
                name: "FK_Voto_Usuario_UsuarioId1",
                table: "Voto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voto",
                table: "Voto");

            migrationBuilder.RenameTable(
                name: "Voto",
                newName: "Votacao");

            migrationBuilder.RenameIndex(
                name: "IX_Voto_UsuarioId1",
                table: "Votacao",
                newName: "IX_Votacao_UsuarioId1");

            migrationBuilder.RenameIndex(
                name: "IX_Voto_FilmeId",
                table: "Votacao",
                newName: "IX_Votacao_FilmeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votacao",
                table: "Votacao",
                column: "VotacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votacao_Filme_FilmeId",
                table: "Votacao",
                column: "FilmeId",
                principalTable: "Filme",
                principalColumn: "FilmeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votacao_Usuario_UsuarioId1",
                table: "Votacao",
                column: "UsuarioId1",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
