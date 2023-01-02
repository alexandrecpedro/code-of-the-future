using Catalog.API.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IProductQueries productQueries;

        public ProductController(ILogger<ProductController> logger,
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
        /// <response code="401">Not authorized</response> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            IEnumerable<Product> products = await productQueries.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string code = null)
        {
            Product value = await productQueries.GetProductAsync(code);
            if (value == null)
                return new NotFoundResult();
            return base.Ok(value);
        }
    }
}