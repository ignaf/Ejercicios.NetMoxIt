using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.DAL;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private MyDbContext _ctx;

        public CustomersController(MyDbContext context)
        {
            _ctx = context;
        }

        //GET /api/customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return _ctx.Customers.ToList();
        }

        //GET /api/customers/1
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _ctx.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        //POST /api/customers
        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();
            return customer;
        }

        //PUT /api/customers/1
        [HttpPut]
        public IActionResult UpdateCostumer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customerInDb = _ctx.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }
            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customer.MembershipTypeId = customer.MembershipTypeId;

            _ctx.SaveChanges();
            return NoContent();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            var customerInDb = _ctx.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }
            _ctx.Customers.Remove(customerInDb);
            _ctx.SaveChanges();
            return NoContent();
        }

 
    }
}
