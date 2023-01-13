
namespace api.DTOs;

public record AdministradorDTO
{
    public string Email { get;set; } = default!;

    public string Senha { get;set; } = default!;
}
