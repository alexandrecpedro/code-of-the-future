using System.Text.Json.Serialization;

namespace Projeto_Radar.Dtos
{
    public class PedidoDto
    {
        [JsonPropertyName("valor_total")]
        public double ValorTotal { get; set; }

        [JsonPropertyName("cliente_id")]
        public int ClienteId { get; set; }
    }
}
