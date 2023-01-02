using AutoMapper;
using Ordering.Models;
using Ordering.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderItem, OrderItemDTO>();
        }
    }
}
