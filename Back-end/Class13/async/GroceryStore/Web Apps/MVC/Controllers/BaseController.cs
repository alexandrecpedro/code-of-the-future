﻿using Services;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MVC.Model.Redis;
using MVC.SignalR;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger logger;
        protected readonly IUserRedisRepository userRedisRepository;

        protected BaseController(ILogger logger, IUserRedisRepository repository)
        {
            this.logger = logger;
            this.userRedisRepository = repository;
        }

        protected void HandleBrokenCircuitException(IService service)
        {
            ViewBag.MsgServiceUnavailable = $"O serviço '{service.Scope}' não está ativo, por favor tente novamente mais tarde.";
        }

        protected void HandleException()
        {
            ViewBag.MsgServiceUnavailable = $"O serviço está indisponível no momento, por favor tente novamente mais tarde.";
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        protected string GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x
                => new[] {
                    JwtClaimTypes.Subject, ClaimTypes.NameIdentifier
                }.Contains(x.Type)
                && !string.IsNullOrWhiteSpace(x.Value));

            if (userIdClaim != null)
                return userIdClaim.Value;

            return null;
        }

        protected async Task CheckUserCounterData()
        {
            if (!true.Equals(ViewData["signed-out"]))
            {
                var userId = GetUserId();
                if (userId != null)
                {
                    ViewBag.UserCounterData
                        = await userRedisRepository.GetUserCounterDataAsync(userId);
                }
            }
        }
    }
}
