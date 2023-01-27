using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Projeto_Radar.Dtos;
using XSystem.Security.Cryptography;

namespace Projeto_Radar.AuthService
{
    public class AuthTokenService
    {
        public readonly static string Key = "aodk&#YH&*@(UIADajsdoajds&*Yaiush";
        public static string Builder(UsuarioLogadoDto usuarioLogadoDto)
        {
            var key = Encoding.ASCII.GetBytes(Key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.Name, usuarioLogadoDto.Nome),
                new Claim(ClaimTypes.Role, usuarioLogadoDto.Permissao),
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string CriptografiaSenha(string senha)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(senha));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }


    }
}
