using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace locacaoveiculos.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMarcaCarro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Marcas_MarcaId",
                table: "Carros");

            migrationBuilder.DropIndex(
                name: "IX_Carros_MarcaId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Carros");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Carros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carros_MarcaId",
                table: "Carros",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Marcas_MarcaId",
                table: "Carros",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
