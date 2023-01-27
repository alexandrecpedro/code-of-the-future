using System.Text.Json.Serialization;

namespace Projeto_Radar.Dtos
{
    public class ProdutoDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }


        public string Descricao { get; set; }


        public string FotoUrl { get; set; }


        public double Valor { get; set; }

        [JsonPropertyName("qtd_estoque")]
        public int QtdEstoque { get; set; }

        public double Custo { get; set; }

        [JsonPropertyName("categoria_id")]
        public int CategoriaId { get; set; }


    }
}
