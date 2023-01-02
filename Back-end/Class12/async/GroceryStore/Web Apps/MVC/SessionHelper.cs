using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC
{
    public class SessionHelper : ISessionHelper
    {
        private readonly IHttpContextAccessor contextAccessor;
        public IConfiguration Configuration { get; }

        public SessionHelper(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            this.contextAccessor = contextAccessor;
            Configuration = configuration;
        }

        public int? GetOrderId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("orderId");
        }

        public void SetOrderId(int orderId)
        {
            contextAccessor.HttpContext.Session.SetInt32("orderId", orderId);
        }

        public async Task<string> GetAccessToken(string scope)
        {
            var tokenClient = new TokenClient(new HttpClient() { BaseAddress = new Uri(Configuration["IdentityUrl"] + "connect/token") }, new TokenClientOptions { ClientId = "MVC", ClientSecret = "secret" });

            var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync(scope);
            return tokenResponse.AccessToken;
        }

        public void SetAccessToken(string accessToken)
        {
            contextAccessor.HttpContext.Session.SetString("accessToken", accessToken);
        }
    }
}
