using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoFinancaPessoal.Migrations
{
    public partial class CPFCNPJ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CPFCNPJ",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoPessoa = table.Column<int>(nullable: false),
                    CpfCnpj = table.Column<string>(maxLength: 15, nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    NomeContato = table.Column<string>(maxLength: 100, nullable: false),
                    RG = table.Column<string>(maxLength: 13, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    Telefones = table.Column<string>(maxLength: 256, nullable: false),
                    Rua = table.Column<string>(maxLength: 256, nullable: false),
                    Numero = table.Column<string>(maxLength: 256, nullable: false),
                    Complemento = table.Column<string>(maxLength: 256, nullable: false),
                    Bairro = table.Column<string>(maxLength: 256, nullable: false),
                    Cidade = table.Column<string>(maxLength: 256, nullable: false),
                    Estado = table.Column<string>(maxLength: 256, nullable: false),
                    Cep = table.Column<string>(maxLength: 256, nullable: false),
                    Observacao = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPFCNPJ", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CPFCNPJ");
        }
    }
}
