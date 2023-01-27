using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_Radar.Entitys
{
    [Table("tb_pedidoProduto")]
    public record PedidoProduto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("valor", TypeName = "DOUBLE")]
        public double Valor { get; set; }

        [Column("quantidade")]
        public int Quantidade { get; set; }

        [ForeignKey("Produto")]
        [Column("produto_id")]
        [JsonPropertyName("produto_id")]
        public int? produtoId { get; set; }
        public Produto Produto { get; set; }

        [ForeignKey("Pedido")]
        [Column("pedido_id")]
        [JsonPropertyName("pedido_id")]
        public int? PedidoId { get; set; }
        public Pedido Pedido { get; set; }

    }
}
