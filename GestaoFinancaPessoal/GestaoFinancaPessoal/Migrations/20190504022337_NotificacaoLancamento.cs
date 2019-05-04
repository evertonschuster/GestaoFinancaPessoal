using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoFinancaPessoal.Migrations
{
    public partial class NotificacaoLancamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificacaoId",
                table: "Lancamento",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "CPFCNPJ",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "CPFCNPJ",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(
                name: "IX_Lancamento_NotificacaoId",
                table: "Lancamento",
                column: "NotificacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamento_Notificacao_NotificacaoId",
                table: "Lancamento",
                column: "NotificacaoId",
                principalTable: "Notificacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lancamento_Notificacao_NotificacaoId",
                table: "Lancamento");

            migrationBuilder.DropTable(
                name: "Notificacao");

            migrationBuilder.DropIndex(
                name: "IX_Lancamento_NotificacaoId",
                table: "Lancamento");

            migrationBuilder.DropColumn(
                name: "NotificacaoId",
                table: "Lancamento");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "CPFCNPJ",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "CPFCNPJ",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
