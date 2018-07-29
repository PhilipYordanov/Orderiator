using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orderiator.Api.Infrastructure.Constants;
using Orderiator.Services;

namespace Orderiator.Api.Controllers
{
    [EnableCors(PolicyConstants.OrderiatorPolicy)]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private ILogger<CustomersController> _logger { get; set; }
        private readonly ICustomerRepository _customRepository;

        public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository)
        {
            this._customRepository = customerRepository;
            this._logger = logger;
        }

        // GET api/customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(_customRepository.GetAllCustomers());
        }

        // GET api/customers/{customerId}
        [HttpGet("{customerId}")]
        public IActionResult GetCustomer(string customerId)
        {
            return Ok(_customRepository.GetCustomer(customerId));
        }

        // GET api/customers/{customerId}/orders
        [HttpGet("{customerId}/orders")]
        public IActionResult GetOrders(string customerId)
        {
            return Ok(_customRepository.GetOrders(customerId));
        }
    }
}
