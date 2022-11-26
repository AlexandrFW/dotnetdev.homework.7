using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomerController : Controller
    {
        private static List<Customer> _customers = new List<Customer>();

        [HttpGet("{id:long}")]
        public Task<Customer> GetCustomerAsync([FromRoute] long id)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);

            return Task.FromResult(customer);
        }

        [HttpPost("")]   
        public Task<long> CreateCustomerAsync([FromBody] Customer customer)
        {
            _customers.Add(customer);           

            return Task.FromResult(customer.Id); 
        }
    }
}