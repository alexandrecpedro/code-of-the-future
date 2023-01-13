
namespace api.DTOs;

public record AdministradorJwtDTO
{
    public int Id { get;set; }
    public string Email { get;set; } = default!;
    public string Permissao { get;set; } = default!;
    public DateTime Expiracao { get;set; } = default!;
}
