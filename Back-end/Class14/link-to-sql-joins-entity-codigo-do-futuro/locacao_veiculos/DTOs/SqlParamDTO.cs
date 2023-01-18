namespace locacao_veiculos.DTOs;

public record SqlParamDTO
{
    public required string Key { get;set; }
    public required string Value { get;set; }
}
