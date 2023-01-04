using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocacaoVeiculos.Models;

[Table("Veiculos")]
public class Veiculo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("veiculo_id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    //[MaxLength(100)]
    [Column("nome", TypeName = "varchar(100)")]
    public string Nome { get;set; } = default!;

    [Required(ErrorMessage = "Id da marca é obrigatório")]
    //[MaxLength(100)]
    [Column("marca_id", TypeName = "varchar(50)")]
    public int MarcaId { get; set; } = default!;

    [Required(ErrorMessage = "Id do modelo é obrigatório")]
    //[MaxLength(100)]
    [Column("modelo_id", TypeName = "varchar(100)")]
    public int ModeloId { get; set; } = default!;
}