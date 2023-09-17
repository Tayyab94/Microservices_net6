using Microsoft.AspNetCore.Mvc;
using ProductWebApis.Models;

namespace ProductWebApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return _context.Products;
        }

        [HttpGet("{productid:int}")]
        public async Task<ActionResult> GetById(int productid)
        {
            var Product = await _context.Products.FindAsync(productid);
            return Ok(Product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product model)
        {
            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Product model)
        {
            _context.Products.Update(model);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> Delete(int productId)
        {
            var customer = await _context.Products.FindAsync(productId);
            _context.Products.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
