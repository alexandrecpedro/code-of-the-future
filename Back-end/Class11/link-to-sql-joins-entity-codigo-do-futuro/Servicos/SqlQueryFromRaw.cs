using locacao_veiculos.Database;
using Microsoft.EntityFrameworkCore;

namespace locacao_veiculos.ModelViews;

public struct SqlQueryFromRaw
{
    private LocacaoContext _context;
    public SqlQueryFromRaw(LocacaoContext context)
    {
        _context = context;
    }

    public List<PedidoResumido> SqlQueryRaw(string query, object[] parameters)
    {
        var pedidos = new List<PedidoResumido>();
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = """
                SELECT 
                    p.Id,
                    c.Nome AS NomeCliente,
                    c0.Nome AS NomeCarro,
                    m.Nome AS MarcaDoCarro,
                    p.DataLocacao AS DataLocacaoPedido,
                    p.DataEntrega AS DataEntregaPedido
                FROM Pedidos AS p
                INNER JOIN Clientes AS c ON p.ClienteId = c.Id
                INNER JOIN Carros AS c0 ON p.CarroId = c0.Id
                INNER JOIN Marcas AS m ON c0.MarcaId = m.Id
            """;

            _context.Database.OpenConnection();
            using (var result = command.ExecuteReader())
            {
                while (result.Read())
                {
                    pedidos.Add(new PedidoResumido{
                        PedidoId = Convert.ToInt32(result["Id"]),
                        NomeCliente = result["NomeCliente"]?.ToString(),
                        MarcaDoCarro = result["MarcaDoCarro"]?.ToString(),
                        DataEntregaPedido = Convert.ToDateTime(result["DataEntregaPedido"]),
                        DataLocacaoPedido = Convert.ToDateTime(result["DataLocacaoPedido"]),
                    });
                }
            }
        }

        return pedidos;
    }
}