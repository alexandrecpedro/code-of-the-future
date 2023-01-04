using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocacaoVeiculos.Models;

[Table("Pedidos")]
public class Pedido
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("pedido_id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O IdCliente é obrigatório")]
    [Column("cliente_id")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "O IdCarro é obrigatório")]
    [Column("carro_id")]
    public int CarroId { get; set; }

    [Required(ErrorMessage = "A data de locação é obrigatória")]
    [Column("data_locacao", TypeName = "DATETIME")]
    public DateTime DataLocacao { get; set; }

    [Required(ErrorMessage = "A data de entrega é obrigatória")]
    [Column("data_entrega", TypeName = "DATETIME")]
    public DateTime DataEntrega { get; set; }
}