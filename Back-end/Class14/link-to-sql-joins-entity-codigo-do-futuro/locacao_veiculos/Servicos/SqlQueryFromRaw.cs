using locacao_veiculos.Database;
using Microsoft.EntityFrameworkCore;
using locacao_veiculos.ModelViews;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using locacao_veiculos.DTOs;

namespace locacao_veiculos.Servicos;

public struct SqlQueryFromRaw
{
    private LocacaoContext _context;
    public SqlQueryFromRaw(LocacaoContext context)
    {
        _context = context;
    }

    public List<PedidoResumido> SqlQueryRaw(string query, List<SqlParamDTO>? parameters = null)
    {
        var pedidos = new List<PedidoResumido>();
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = query;
            if(parameters is not null)
            {
                foreach(SqlParamDTO param in parameters)
                {
                    command.Parameters.Add(new MySqlParameter(param.Key,param.Value));
                }
            }

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