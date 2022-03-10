using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fgv.Acad.Financeiro.Migrations
{
    public partial class CriacaoEvento3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraEvento",
                table: "Evento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Evento",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FimOferta",
                table: "Evento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InicioOferta",
                table: "Evento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Evento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "local",
                table: "Evento",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    Endereco = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoIngresso",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEvento = table.Column<long>(nullable: true),
                    Descricao = table.Column<long>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIngresso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoIngresso_Evento_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdTipoIngresso = table.Column<long>(nullable: true),
                    IdCliente = table.Column<long>(nullable: true),
                    EhMeiaEntrada = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venda_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venda_TipoIngresso_IdTipoIngresso",
                        column: x => x.IdTipoIngresso,
                        principalTable: "TipoIngresso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoIngresso_IdEvento",
                table: "TipoIngresso",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_IdCliente",
                table: "Venda",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_IdTipoIngresso",
                table: "Venda",
                column: "IdTipoIngresso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "TipoIngresso");

            migrationBuilder.DropColumn(
                name: "DataHoraEvento",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "FimOferta",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "InicioOferta",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "local",
                table: "Evento");
        }
    }
}
