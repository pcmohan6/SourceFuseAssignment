using SourceFuseSampleAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SourceFuseSampleAPI.Controllers
{
    /// <summary>
    /// Default route of the controller
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]//Content type supported by the all methods
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        #region Constructor
        /// <summary>
        /// Constructor passes DB contect default
        /// </summary>
        /// <param name="dbContext"></param>
        public CustomerController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region GetCustomers
        /// <summary>
        /// Method to get all customers with applicable status codes
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<CustomerMaster>>> GetCustomers()
        {
            if (_dbContext.CustomerMaster == null)
            {
                return NotFound();
            }
            return await _dbContext.CustomerMaster.ToListAsync();
        }
        #endregion

        #region Get customer by Id
        /// <summary>
        /// Method to get one customer by passing customer Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerMaster>> GetCustomerById(int customerId)
        {
            if (_dbContext.CustomerMaster == null)
            {
                return NotFound();
            }
            var customer = await _dbContext.CustomerMaster.FindAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        #endregion

        #region Create customer
        /// <summary>
        /// Method to create new customer, it does asynchronous way of creation in backend
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerMaster>> PostCustomer(CustomerMaster customer)
        {
            _dbContext.CustomerMaster.Add(customer);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.CustomerId }, customer);
        }

        #endregion

        #region Overwrite a customer
        /// <summary>
        /// Method to overwrite customer info
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCustomer(int customerId, CustomerMaster customer)
        {
            if (customerId != customer.CustomerId)
            {
                return BadRequest();
            }

            _dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        #endregion

        #region Delete a customer
        /// <summary>
        /// Method to delete a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCustomer(int customerId)
        {
            if (_dbContext.CustomerMaster == null)
            {
                return NotFound();
            }

            var customer = await _dbContext.CustomerMaster.FindAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            _dbContext.CustomerMaster.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(long customerId)
        {
            return (_dbContext.CustomerMaster?.Any(e => e.CustomerId == customerId)).GetValueOrDefault();
        }
        #endregion
    }
}
