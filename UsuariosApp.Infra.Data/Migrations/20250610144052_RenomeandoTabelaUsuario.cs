using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenomeandoTabelaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_PERMISSAO_USUARIOS_USUARIO_ID",
                table: "USUARIO_PERMISSAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USUARIOS",
                table: "USUARIOS");

            migrationBuilder.RenameTable(
                name: "USUARIOS",
                newName: "USUARIO");

            migrationBuilder.RenameIndex(
                name: "IX_USUARIOS_EMAIL",
                table: "USUARIO",
                newName: "IX_USUARIO_EMAIL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USUARIO",
                table: "USUARIO",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_PERMISSAO_USUARIO_USUARIO_ID",
                table: "USUARIO_PERMISSAO",
                column: "USUARIO_ID",
                principalTable: "USUARIO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_PERMISSAO_USUARIO_USUARIO_ID",
                table: "USUARIO_PERMISSAO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USUARIO",
                table: "USUARIO");

            migrationBuilder.RenameTable(
                name: "USUARIO",
                newName: "USUARIOS");

            migrationBuilder.RenameIndex(
                name: "IX_USUARIO_EMAIL",
                table: "USUARIOS",
                newName: "IX_USUARIOS_EMAIL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USUARIOS",
                table: "USUARIOS",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_PERMISSAO_USUARIOS_USUARIO_ID",
                table: "USUARIO_PERMISSAO",
                column: "USUARIO_ID",
                principalTable: "USUARIOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
