namespace locacao_veiculos.Models;

public class PedidoCarro
{
    public PedidoCarro()
    {
        DataTrasacao = DateTime.Now;
    }
    
    public int Id { get;set; } = default!;

    public int PedidoId { get;set; } = default!;
    public Pedido? Pedido { get;set; }
    
    public int CarroId { get;set; } = default!;
    public Carro? Carro { get;set; }

    public double ValorTrasacao { get;set; }
    public DateTime DataTrasacao { get;set; }

}
