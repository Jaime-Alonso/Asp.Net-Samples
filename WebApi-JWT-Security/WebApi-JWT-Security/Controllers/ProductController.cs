using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi_JWT_Security.Services;

namespace WebApi_JWT_Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet(Name = nameof(GetById))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetById(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
