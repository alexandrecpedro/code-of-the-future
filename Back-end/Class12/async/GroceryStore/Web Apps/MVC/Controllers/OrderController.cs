using Models.ViewModels;
using Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Model.Redis;
using MVC.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    [Route("[controller]")]
    public class OrderController : BaseController
    {
        private readonly IIdentityParser<ApplicationUser> appUserParser;
        private readonly IOrderService orderService;

        public OrderController(
            IIdentityParser<ApplicationUser> appUserParser,
            IOrderService orderService,
            ILogger<OrderController> logger,
            IUserRedisRepository repository) 
            : base(logger, repository)
        {
            this.appUserParser = appUserParser;
            this.orderService = orderService;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult> History(string customerId)
        {
            await CheckUserCounterData();

            List<OrderDTO> model = await orderService.GetAsync(customerId);
            return base.View(model);
        }

        [Route("Notifications")]
        public async Task<ActionResult> Notifications()
        {
            string customerId = GetUserId();
            UserCounterData userCounterData = await userRedisRepository.GetUserCounterDataAsync(customerId);
            await userRedisRepository.MarkAllAsReadAsync(customerId);
            await CheckUserCounterData();
            return View(userCounterData);
        }
    }
}
