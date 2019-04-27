using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoFinancaPessoal.Migrations
{
    public partial class nomenclatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Lancamento");

            migrationBuilder.AddColumn<string>(
                name: "TipoLancamento",
                table: "Lancamento",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoLancamento",
                table: "Lancamento");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Lancamento",
                nullable: false,
                defaultValue: "");
        }
    }
}
