using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace locacaoveiculos.Migrations
{
    /// <inheritdoc />
    public partial class MuitosCarrosParaMuitosPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Carros_CarroId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_CarroId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CarroId",
                table: "Pedidos");

            migrationBuilder.CreateTable(
                name: "PedidoCarros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    CarroId = table.Column<int>(type: "int", nullable: false),
                    ValorTrasacao = table.Column<double>(type: "double", nullable: false),
                    DataTrasacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoCarros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoCarros_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoCarros_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCarros_CarroId",
                table: "PedidoCarros",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCarros_PedidoId",
                table: "PedidoCarros",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoCarros");

            migrationBuilder.AddColumn<int>(
                name: "CarroId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CarroId",
                table: "Pedidos",
                column: "CarroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Carros_CarroId",
                table: "Pedidos",
                column: "CarroId",
                principalTable: "Carros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
