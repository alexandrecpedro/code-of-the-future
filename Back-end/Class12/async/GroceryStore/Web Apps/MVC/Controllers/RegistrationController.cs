using Models;
using Models.ViewModels;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Model.Redis;
using MVC.SignalR;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;

namespace Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly IIdentityParser<ApplicationUser> appUserParser;

        public RegistrationController(
            IIdentityParser<ApplicationUser> appUserParser,
            ILogger<RegistrationController> logger,
            IUserRedisRepository repository)
            : base(logger, repository)
        {
            this.appUserParser = appUserParser;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var usuario = appUserParser.Parse(HttpContext.User);
                RegistrationViewModel registration
                    = new RegistrationViewModel()
                    {
                        District = usuario.District,
                        ZipCode = usuario.ZipCode,
                        AdditionalAddress = usuario.AdditionalAddress,
                        Email = usuario.Email,
                        Address = usuario.Address,
                        City = usuario.City,
                        Name = usuario.Name,
                        Phone = usuario.Phone,
                        State = usuario.State
                    };

                await CheckUserCounterData();
                return View(registration);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                HandleException();
            }
            return View();
        }
    }
}
