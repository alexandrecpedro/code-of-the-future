using locacao_veiculos.Database;
using locacao_veiculos.ModelViews;
using locacao_veiculos.Servicos;
using locacao_veiculos.DTOs;

namespace locacao_veiculos.test.Servicos;

[TestClass]
public class SqlQueryFromRawTest
{
    [TestMethod]
    public void TestandoPedidoResumido()
    {
        //Arrange (Organizando variáveis para execução do teste)
        var sqlQueryFromRaw = new SqlQueryFromRaw(new LocacaoContext());
        var query = """
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

        //Act (Executando a função do teste)
        List<PedidoResumido> resultado = sqlQueryFromRaw.SqlQueryRaw(query);

        //Assert (validando o retorno da execução)
        Assert.AreEqual(0, resultado.Count());
    }

    [TestMethod]
    public void TestandoPedidoResumidoComParametro()
    {
        //Arrange (Organizando variáveis para execução do teste)
        var sqlQueryFromRaw = new SqlQueryFromRaw(new LocacaoContext());
        var query = """
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
                Where p.Id = @id
            """;

        //Act (Executando a função do teste)
        var parametros = new List<SqlParamDTO>()
        {
            new SqlParamDTO { Key = "@id", Value = "1"}
        };
        List<PedidoResumido> resultado = sqlQueryFromRaw.SqlQueryRaw(query, parametros);

        //Assert (validando o retorno da execução)
        Assert.AreEqual(0, resultado.Count());
    }
}