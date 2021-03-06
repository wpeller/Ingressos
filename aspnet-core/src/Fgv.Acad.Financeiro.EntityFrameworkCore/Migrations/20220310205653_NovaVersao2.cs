using Microsoft.EntityFrameworkCore.Migrations;

namespace Fgv.Acad.Financeiro.Migrations
{
    public partial class NovaVersao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Cliente_IdCliente",
                table: "Venda");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_TipoIngresso_IdTipoIngresso",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_IdCliente",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_IdTipoIngresso",
                table: "Venda");

            migrationBuilder.AlterColumn<long>(
                name: "IdTipoIngresso",
                table: "Venda",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "IdCliente",
                table: "Venda",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClienteId",
                table: "Venda",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TipoIngressoId",
                table: "Venda",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venda_ClienteId",
                table: "Venda",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_TipoIngressoId",
                table: "Venda",
                column: "TipoIngressoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Cliente_ClienteId",
                table: "Venda",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_TipoIngresso_TipoIngressoId",
                table: "Venda",
                column: "TipoIngressoId",
                principalTable: "TipoIngresso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Cliente_ClienteId",
                table: "Venda");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_TipoIngresso_TipoIngressoId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_ClienteId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_TipoIngressoId",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "TipoIngressoId",
                table: "Venda");

            migrationBuilder.AlterColumn<long>(
                name: "IdTipoIngresso",
                table: "Venda",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "IdCliente",
                table: "Venda",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Venda_IdCliente",
                table: "Venda",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_IdTipoIngresso",
                table: "Venda",
                column: "IdTipoIngresso");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Cliente_IdCliente",
                table: "Venda",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_TipoIngresso_IdTipoIngresso",
                table: "Venda",
                column: "IdTipoIngresso",
                principalTable: "TipoIngresso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
