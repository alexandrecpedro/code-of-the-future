using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Ordering.Models;
using Ordering.Models.DTOs;
using Ordering.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Models;

namespace Ordering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public object JwtClaimTypes { get; private set; }

        public OrderingController(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        // POST api/ordering
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await orderRepository.CreateOrUpdate(order);
            return Ok(resultado);
        }

        [Authorize]
        [HttpGet("{customerId}")]
        public async Task<ActionResult> Get(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException();
            }

            IList<Order> orders = await orderRepository.GetOrders(customerId);

            if (orders == null)
            {
                return NotFound(customerId);
            }

            List<OrderDTO> dto = mapper.Map<List<OrderDTO>>(orders);
            return base.Ok(dto);
        }

        private string GetUserId()
        {
            var userIdClaim = 
                User
                .Claims
                .FirstOrDefault(x => new[] 
                    {
                        "sub", ClaimTypes.NameIdentifier
                    }.Contains(x.Type)
                && !string.IsNullOrWhiteSpace(x.Value));

            if (userIdClaim != null)
                return userIdClaim.Value;

            throw new InvalidUserDataException("Usuário desconhecido");
        }
    }
}
