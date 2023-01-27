using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRadar.Migrations
{
    /// <inheritdoc />
    public partial class alterandoPropriedadePosicoesProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "posicao_y",
                table: "tb_posicoesProdutos",
                type: "DOUBLE",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<double>(
                name: "posicao_x",
                table: "tb_posicoesProdutos",
                type: "DOUBLE",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<string>(
                name: "url_foto_prateleira",
                table: "tb_campanhas",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "posicao_y",
                table: "tb_posicoesProdutos",
                type: "INT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "DOUBLE");

            migrationBuilder.AlterColumn<int>(
                name: "posicao_x",
                table: "tb_posicoesProdutos",
                type: "INT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "DOUBLE");

            migrationBuilder.UpdateData(
                table: "tb_campanhas",
                keyColumn: "url_foto_prateleira",
                keyValue: null,
                column: "url_foto_prateleira",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "url_foto_prateleira",
                table: "tb_campanhas",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
