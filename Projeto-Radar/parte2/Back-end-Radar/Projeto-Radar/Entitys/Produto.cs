using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_Radar.Entitys
{
    [Table("tb_produtos")]
    public record Produto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("nome", TypeName = "varchar(45)")]
        public string Nome { get; set; }

        [Column("descricao", TypeName = "varchar(255)")]
        public string Descricao { get; set; }

        [Column("foto_url", TypeName = "varchar(255)")]
        public string FotoUrl { get; set; }


        [Column("valor", TypeName = "DOUBLE")]
        public double Valor { get; set; }

        [Column("qtd_estoque", TypeName = "INT")]
        [JsonPropertyName("qtd_estoque")]
        public int QtdEstoque { get; set; }



        [Column("custo", TypeName = "DOUBLE")]
        public double Custo { get; set; }

        [ForeignKey("Categoria")]
        [Column("categoria_id")]
        [JsonPropertyName("categoria_id")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = default!;

    }
}
