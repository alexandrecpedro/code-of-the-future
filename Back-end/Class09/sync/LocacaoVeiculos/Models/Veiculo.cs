using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocacaoVeiculos.Models;

[Table("Veiculos")]
public class Veiculo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    //[MaxLength(100)]
    [Column("nome", TypeName = "varchar(100)")]
    public string Nome { get;set; } = default!;

    [Required(ErrorMessage = "Id da marca é obrigatório")]
    [Column("marca_id")]
    public int MarcaId { get; set; }
    [ForeignKey("MarcaId")]
    public Marca? Marca { get; set; }

    [Required(ErrorMessage = "Id do modelo é obrigatório")]
    public int ModeloId { get; set; }
    [ForeignKey("ModeloId")]
    public Modelo? Modelo { get; set; }

    public ICollection<Pedido>? Pedidos { get; set; }
}