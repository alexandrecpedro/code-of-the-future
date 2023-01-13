using api.DTOs;
using api.ModelViews;
using Jose;

namespace api.Servicos.Autenticacao;

public class TokenJWT
{
    public static string Builder(AdministradorLogado AdministradorLogado)
    {
        var key = "SEGREDO_do_CoDigoDO-Futuro";

        var payload = new AdministradorJwtDTO
        {
           Id = AdministradorLogado.Id,
           Email = AdministradorLogado.Email,
           Permissao = AdministradorLogado.Permissao,
           Expiracao = DateTime.Now.AddDays(2)
        };

        string token = Jose.JWT.Encode(payload, key, JwsAlgorithm.none);

        return token;
    }

}
