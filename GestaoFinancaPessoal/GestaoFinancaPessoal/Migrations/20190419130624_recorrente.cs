using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoFinancaPessoal.Migrations
{
    public partial class recorrente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParcelaTotal",
                table: "Recorrente");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Recorrente");

            migrationBuilder.DropColumn(
                name: "IsPago",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "Banco",
                table: "Conta");

            migrationBuilder.AlterColumn<int>(
                name: "ParcelaInicial",
                table: "Recorrente",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicial",
                table: "Recorrente",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Recorrente",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFinal",
                table: "Recorrente",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Periodicidade",
                table: "Recorrente",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Repetir",
                table: "Recorrente",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Recorrente",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInicial",
                table: "Recorrente");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Recorrente");

            migrationBuilder.DropColumn(
                name: "DataFinal",
                table: "Recorrente");

            migrationBuilder.DropColumn(
                name: "Periodicidade",
                table: "Recorrente");

            migrationBuilder.DropColumn(
                name: "Repetir",
                table: "Recorrente");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Recorrente");

            migrationBuilder.AlterColumn<decimal>(
                name: "ParcelaInicial",
                table: "Recorrente",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<decimal>(
                name: "ParcelaTotal",
                table: "Recorrente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Recorrente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPago",
                table: "Lancamento",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Banco",
                table: "Conta",
                nullable: true);
        }
    }
}
