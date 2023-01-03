using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models;

[Table("clientes")]
public record Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    // [MaxLength(100)]
    [Column("nome", TypeName = "varchar(100)")]
    public string Nome { get; set; } = default!;

    [Column("email", TypeName = "varchar(150)")]
    public string? Email { get; set; }

    [Column("endereco", TypeName = "text")]
    public string? Endereco { get; set; }
}