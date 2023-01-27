using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRadar.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoTipoPropriedadeLAtLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "longitude",
                table: "tb_lojas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "latitude",
                table: "tb_lojas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "longitude",
                table: "tb_lojas",
                type: "varchar(45)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "latitude",
                table: "tb_lojas",
                type: "varchar(45)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
