using SourceFuseSampleAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SourceFuseSampleAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        #region Constructor
        public CustomerOrderController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region GetOrders
        [HttpGet("")]
        public async Task<ActionResult<IList<OrderHeader>>> GetOrders()
        {
            if (_dbContext.CustomerMaster == null)
            {
                return NotFound();
            }
            return await _dbContext.OrderHeader.ToListAsync();
        }
        #endregion

        #region Get a order
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderHeader>> GetOrder(int orderId)
        {
            if (_dbContext.OrderHeader == null)
            {
                return NotFound();
            }
            var order = await _dbContext.OrderHeader.FindAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
        #endregion

        #region Create a order
        [HttpPost("")]
        public async Task<ActionResult<OrderHeader>> PostOrder(OrderHeader order)
        {
            _dbContext.OrderHeader.Add(order);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { customerId = order.OrderID }, order);
        }
        #endregion


    }
}
