using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CadastroHospede",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    Cep = table.Column<string>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroEndereco = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastroHospede", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaQuartos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    ValorDiariaAdulto = table.Column<double>(type: "REAL", nullable: false),
                    ValorDiariaCrianca = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaQuartos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospedagems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuartoId = table.Column<int>(type: "INTEGER", nullable: false),
                    HospedeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuantidadeAdultos = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantidadeCrianca = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantidadeDias = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCheckIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataCheckOut = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HoraCheckIn = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    HoraCheckOut = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    ValorTotal = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospedagems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospedagems_CadastroHospede_HospedeId",
                        column: x => x.HospedeId,
                        principalTable: "CadastroHospede",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospedagems_CategoriaQuartos_QuartoId",
                        column: x => x.QuartoId,
                        principalTable: "CategoriaQuartos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hospedagems_HospedeId",
                table: "Hospedagems",
                column: "HospedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospedagems_QuartoId",
                table: "Hospedagems",
                column: "QuartoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hospedagems");

            migrationBuilder.DropTable(
                name: "CadastroHospede");

            migrationBuilder.DropTable(
                name: "CategoriaQuartos");
        }
    }
}
