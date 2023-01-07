namespace locacao_veiculos.Models;

public class Pedido
{
    public int Id { get;set; } = default!;

    public int ClienteId { get;set; } = default!;

    public Cliente? Cliente { get;set; }

    public int CarroId { get;set; } = default!;

    public Carro? Carro { get;set; }

    public DateTime DataLocacao { get;set; } = default!;
    public DateTime DataEntrega { get;set; } = default!;
}
