using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoFinancaPessoal.Migrations
{
    public partial class lancamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Conta",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Conta",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Conta",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categoria",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "Recorrente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantidade = table.Column<int>(nullable: false),
                    Periodo = table.Column<int>(nullable: false),
                    ParcelaInicial = table.Column<decimal>(nullable: false),
                    ParcelaTotal = table.Column<decimal>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recorrente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lancamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContaId = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    ValorPago = table.Column<decimal>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 256, nullable: false),
                    IsPago = table.Column<bool>(nullable: false),
                    IsAutomatico = table.Column<bool>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(nullable: false),
                    RecorrenteId = table.Column<int>(nullable: true),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    ContaDestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lancamento_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lancamento_Conta_ContaDestionId",
                        column: x => x.ContaDestionId,
                        principalTable: "Conta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lancamento_Conta_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Conta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lancamento_Recorrente_RecorrenteId",
                        column: x => x.RecorrenteId,
                        principalTable: "Recorrente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_CategoriaId",
                table: "Lancamento",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_ContaDestionId",
                table: "Lancamento",
                column: "ContaDestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_ContaId",
                table: "Lancamento",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_RecorrenteId",
                table: "Lancamento",
                column: "RecorrenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lancamento");

            migrationBuilder.DropTable(
                name: "Recorrente");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Conta",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Conta",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Conta",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categoria",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }
    }
}
