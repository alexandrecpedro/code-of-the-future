using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Services
{
    public class IdentityParser : IIdentityParser<ApplicationUser>
    {
        public ApplicationUser Parse(IPrincipal principal)
        {
            if (principal is ClaimsPrincipal claims)
            {
                return new ApplicationUser
                {
                    Name = claims.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "",
                    Email = claims.Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? "",
                    Phone = claims.Claims.FirstOrDefault(x => x.Type == "phone")?.Value ?? "",
                    Address = claims.Claims.FirstOrDefault(x => x.Type == "address")?.Value ?? "",
                    AdditionalAddress = claims.Claims.FirstOrDefault(x => x.Type == "address_details")?.Value ?? "",
                    District = claims.Claims.FirstOrDefault(x => x.Type == "district")?.Value ?? "",
                    City = claims.Claims.FirstOrDefault(x => x.Type == "city")?.Value ?? "",
                    State = claims.Claims.FirstOrDefault(x => x.Type == "state")?.Value ?? "",
                    ZipCode = claims.Claims.FirstOrDefault(x => x.Type == "zip_code")?.Value ?? ""
                };
            }
            throw new ArgumentException(message: "The principal must be a ClaimsPrincipal", paramName: nameof(principal));
        }
    }
}
