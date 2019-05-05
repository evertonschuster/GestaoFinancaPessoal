using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoFinancaPessoal.Migrations
{
    public partial class lancamentoDataPagamentoNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPagamento",
                table: "Lancamento",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPagamento",
                table: "Lancamento",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
