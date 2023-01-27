using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRadar.Migrations
{
    /// <inheritdoc />
    public partial class primeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_campanhas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    urlfotoprateleira = table.Column<string>(name: "url_foto_prateleira", type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_campanhas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_categorias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefone = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cpf = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cep = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logradouro = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<double>(type: "DOUBLE", nullable: false),
                    bairro = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cidade = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado = table.Column<string>(type: "varchar(15)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    complemento = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_clientes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_lojas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    observacao = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cep = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logradouro = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<int>(type: "INT", nullable: false),
                    bairro = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cidade = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    complemento = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    latitude = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    longitude = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_lojas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    senha = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    permissao = table.Column<string>(type: "varchar(15)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_posicoesProdutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    posicaox = table.Column<int>(name: "posicao_x", type: "INT", nullable: false),
                    posicaoy = table.Column<int>(name: "posicao_y", type: "INT", nullable: false),
                    campanhaid = table.Column<int>(name: "campanha_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_posicoesProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_posicoesProdutos_tb_campanhas_campanha_id",
                        column: x => x.campanhaid,
                        principalTable: "tb_campanhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valor = table.Column<double>(type: "DOUBLE", nullable: false),
                    qtdestoque = table.Column<int>(name: "qtd_estoque", type: "INT", nullable: false),
                    custo = table.Column<double>(type: "DOUBLE", nullable: false),
                    categoriaid = table.Column<int>(name: "categoria_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_produtos_tb_categorias_categoria_id",
                        column: x => x.categoriaid,
                        principalTable: "tb_categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    valortotal = table.Column<double>(name: "valor_total", type: "DOUBLE", nullable: false),
                    data = table.Column<DateOnly>(type: "DATE", nullable: false),
                    clienteid = table.Column<int>(name: "cliente_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_pedidos_tb_clientes_cliente_id",
                        column: x => x.clienteid,
                        principalTable: "tb_clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_pedidoProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    valor = table.Column<double>(type: "DOUBLE", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    produtoid = table.Column<int>(name: "produto_id", type: "int", nullable: false),
                    pedidoid = table.Column<int>(name: "pedido_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pedidoProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_pedidoProduto_tb_pedidos_pedido_id",
                        column: x => x.pedidoid,
                        principalTable: "tb_pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_pedidoProduto_tb_produtos_produto_id",
                        column: x => x.produtoid,
                        principalTable: "tb_produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedidoProduto_pedido_id",
                table: "tb_pedidoProduto",
                column: "pedido_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedidoProduto_produto_id",
                table: "tb_pedidoProduto",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_pedidos_cliente_id",
                table: "tb_pedidos",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_posicoesProdutos_campanha_id",
                table: "tb_posicoesProdutos",
                column: "campanha_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_produtos_categoria_id",
                table: "tb_produtos",
                column: "categoria_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_lojas");

            migrationBuilder.DropTable(
                name: "tb_pedidoProduto");

            migrationBuilder.DropTable(
                name: "tb_posicoesProdutos");

            migrationBuilder.DropTable(
                name: "tb_usuarios");

            migrationBuilder.DropTable(
                name: "tb_pedidos");

            migrationBuilder.DropTable(
                name: "tb_produtos");

            migrationBuilder.DropTable(
                name: "tb_campanhas");

            migrationBuilder.DropTable(
                name: "tb_clientes");

            migrationBuilder.DropTable(
                name: "tb_categorias");
        }
    }
}
