using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocacaoVeiculos.Models;

[Table("Pedidos")]
public class Pedido
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Id do cliente é obrigatório")]
    [Column("cliente_id")]
    public int ClienteId { get; set; }
    [ForeignKey("ClienteId")]
    public Cliente? Cliente { get; set; }

    [Required(ErrorMessage = "Id do veículo é obrigatório")]
    [Column("veiculo_id")]
    public int VeiculoId { get; set; }
    [ForeignKey("VeiculoId")]
    public Veiculo? Veiculo { get; set; }


    [Required(ErrorMessage = "A data de locação é obrigatória")]
    [Column("data_locacao", TypeName = "DATETIME")]
    public DateTime DataLocacao { get; set; }

    [Required(ErrorMessage = "A data de entrega é obrigatória")]
    [Column("data_entrega", TypeName = "DATETIME")]
    public DateTime DataEntrega { get; set; }
}