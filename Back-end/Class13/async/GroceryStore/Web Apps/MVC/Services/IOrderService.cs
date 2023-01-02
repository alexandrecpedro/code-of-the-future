using Models.ViewModels;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IOrderService : IService
    {
        Task<List<OrderDTO>> GetAsync(string customerId);
    }
}
