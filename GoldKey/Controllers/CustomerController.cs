using GoldKey.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoldKey.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CustomersController : ApiController
    {
        // GET api/values
        public IEnumerable<Customer> Get()
        {
            return Customers.GetAll();
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        public Customer Get(int id)
        {
            return Customers.GetCustomer(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            var customer = JsonConvert.DeserializeObject<Customer>(value); // Convert JSON to Users
            Customers.CreateCustomer(customer); // Create a new User

        }

        // PUT api/values/5
        //[HttpPut("{id}")]

        public void Put(int id, [FromBody]string value)
        {
            var customer = JsonConvert.DeserializeObject<Customer>(value);
            Customers.UpdateCustomer(id, customer);

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Customers.DeleteCustomer(id);
        }
    }
}
