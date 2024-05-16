using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Error;
using ProductRepository;
using ProductData.Entites;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDpContext _context;

        public BuggyController(StoreDpContext context)
        {
            _context = context;
        }
        [HttpGet("notfound")]
        public ActionResult notFound()
        {
            var product=_context.Set<Product>().Find(100);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }
        [HttpGet("serverError")]
        public ActionResult serverError()
        {
            var product = _context.Set<Product>().Find(100);
            var finalProduct=product.ToString();
            return Ok(finalProduct);
        }
        [HttpGet("BadRequest/{id}")]
        public ActionResult BadRequest(int id)
        {
            return NotFound(new ApiResponse(400));
        }
    }
}
