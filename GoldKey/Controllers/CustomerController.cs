using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoldKey.Models;
using Newtonsoft.Json;

namespace GoldKey.Controllers
{
    public class CustomerController : ApiController
    {
        // GET: api/Customer
        public IEnumerable<Customer> Get()
        {
            return Customers.GetAll();
        }

        // GET: api/Customer/5
        public Customer Get(int id)
        {
            return Customers.GetCustomer(id);
            ;
        }

        // POST: api/Customer
        public void Post([FromBody]string value)
        {
            var customer = JsonConvert.DeserializeObject<Customer>(value); // Convert JSON to Users
            Customers.CreateCustomer(customer); // Create a new User

        }

        // PUT: api/Customer/5
        
        public void Put(int id, [FromBody]string value)
        {
            var customer = JsonConvert.DeserializeObject<Customer>(value);
            Customers.UpdateCustomer(id, customer);

        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
            Customers.DeleteCustomer(id);

        }
    }
}
