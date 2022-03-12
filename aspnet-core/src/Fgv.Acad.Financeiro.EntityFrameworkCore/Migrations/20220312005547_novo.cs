using Microsoft.EntityFrameworkCore.Migrations;

namespace Fgv.Acad.Financeiro.Migrations
{
    public partial class novo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "TipoIngresso",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Descricao",
                table: "TipoIngresso",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
