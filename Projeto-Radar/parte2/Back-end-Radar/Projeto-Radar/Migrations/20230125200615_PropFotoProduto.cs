using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRadar.Migrations
{
    /// <inheritdoc />
    public partial class PropFotoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "foto_url",
                table: "tb_produtos",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<double>(
                name: "longitude",
                table: "tb_lojas",
                type: "DOUBLE",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "latitude",
                table: "tb_lojas",
                type: "DOUBLE",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foto_url",
                table: "tb_produtos");

            migrationBuilder.AlterColumn<double>(
                name: "longitude",
                table: "tb_lojas",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "DOUBLE");

            migrationBuilder.AlterColumn<double>(
                name: "latitude",
                table: "tb_lojas",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "DOUBLE");
        }
    }
}
