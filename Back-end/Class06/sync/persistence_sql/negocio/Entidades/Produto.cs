using System.ComponentModel.DataAnnotations;

namespace Negocio.Entidades;

public record Produto
{
    public string Id { get; set; } = default!;

    [Required(ErrorMessage = "O nome não pode ser vazio")]
    [StringLength(50, MinimumLength = 3)]
    public string Nome { get; set; } = default!;

    [Required(ErrorMessage = "A descrição não pode ser vazia")]
    [StringLength(150, MinimumLength = 10)]
    public string Descricao { get; set; } = default!;

    [DataType(DataType.DateTime)]
    public DateTime DataCriacao { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime DataValidade { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser igual ou maior do que 0")]
    public int QuantidadeEstoque { get; set; }
}