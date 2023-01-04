using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocacaoVeiculos.Models;

[Table("Modelos")]
public class Modelo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("modelo_id")]
    public int Id { get;set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    //[MaxLength(100)]
    [Column("nome", TypeName = "varchar(100)")]
    public string Nome { get;set; } = default!;
}