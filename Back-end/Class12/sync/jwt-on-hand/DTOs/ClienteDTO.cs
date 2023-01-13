using System.Text.Json.Serialization;

namespace api.DTOs;

public record ClienteDTO
{
    [JsonPropertyName("name")]
    public string Nome { get;set; } = default!;
    public string Email { get;set; } = default!;

    [JsonPropertyName("phone")]
    public string? Telefone { get;set; }

    [JsonPropertyName("address")]
    public string? Endereco { get;set; }
}
