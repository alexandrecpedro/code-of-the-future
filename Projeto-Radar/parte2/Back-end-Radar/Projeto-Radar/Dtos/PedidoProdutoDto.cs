using System.Text.Json.Serialization;

namespace Projeto_Radar.Dtos
{
    public class PedidoProdutoDto
    {
        public double Valor { get; set; }

        public int Quantidade { get; set; }

        [JsonPropertyName("produto_id")]
        public int produtoId { get; set; }
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }

    }
}
