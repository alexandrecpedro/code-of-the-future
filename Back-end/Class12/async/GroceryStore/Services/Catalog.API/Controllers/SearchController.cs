using Catalog.API.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IProductQueries productQueries;

        public SearchController(ILogger<ProductController> logger,
            IProductQueries productQueries)
        {
            this.logger = logger;
            this.productQueries = productQueries;
        }

        /// <summary>
        /// Gets the complete product catalog list
        /// </summary>
        /// <returns>
        /// The complete product catalog list
        /// </returns>
        /// <param name="search"></param>
        /// <response code="401">Not authorized</response> 
        /// <returns></returns>
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string search)
        {
            IEnumerable<Product> products = await productQueries.GetProductsAsync(search);
            return Ok(products);
        }
    }
}