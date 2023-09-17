using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderWebApis.Models;

namespace OrderWebApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;

        private readonly IMongoCollection<Order> _ordersCollection;
        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
            //var dbHost = "localhost";
            //var dbName = "dms_order";

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);

            _ordersCollection = database.GetCollection<Order>("orders");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>>GetOrders()
        {
            return await _ordersCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }

        [HttpGet("orderId")]
        public async Task<ActionResult<Order>>GetById(string orderId)
        {
            var filterDefinition=Builders<Order>.Filter.Eq(s=>s.OrderId, orderId);
            return await _ordersCollection.Find(filterDefinition).SingleOrDefaultAsync();

        }

        [HttpPost]
        public async Task<ActionResult>Create(Order model)
        {
            await _ordersCollection.InsertOneAsync(model);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult>Update(Order model)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(s=>s.OrderId,model.OrderId);
            await _ordersCollection.ReplaceOneAsync(filterDefinition, model);
            return Ok();
        }


        [HttpDelete("orderId")]
        public async Task<ActionResult> Delete(string orderId)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(s => s.OrderId, orderId);
             await _ordersCollection.DeleteOneAsync(filterDefinition);
            return Ok();    
        }
    }
}
