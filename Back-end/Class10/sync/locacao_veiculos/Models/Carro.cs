namespace locacao_veiculos.Models;

public class Carro
{
    public int Id { get;set; } = default!;
    public string Nome { get;set; } = default!;

    public int ModeloId { get;set; } = default!;

    public Modelo? Modelo { get;set; }

}
