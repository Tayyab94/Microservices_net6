using CustomerWebApis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;
        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>>GetCustomers()
        {
            return _context.Customers;
        }

        [HttpGet("{customerid:int}")]
        public async Task<ActionResult>GetById(int customerid)
        {
            var customer = await _context.Customers.FindAsync(customerid);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult>CreateCustomer(Customer model)
        {
            await _context.Customers.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(Customer model)
        {
            _context.Customers.Update(model);
            await _context.SaveChangesAsync();  
            return Ok();
        }


        [HttpDelete("{customerid:int}")]
        public async Task<ActionResult> Delete(int customerid)
        {
            var customer = await _context.Customers.FindAsync(customerid);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
