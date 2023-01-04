using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocacaoVeiculos.Models;

[Table("Configuracoes")]
public class Configuracao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("configuracao_id")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Dias de locação é obrigatório")]
    [Column("dias_de_locacao")]
    public int DiasDeLocacao { get; set; }
}