using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private List<Customer> _customers = new List<Customer>();
        private Customer c1 = new Customer { Name = "John Smith", Id = 1 };
        private Customer c2 = new Customer { Name = "Mary Williams", Id=2 };       

        public CustomersController()
        {
            _customers.Add(c1);
            _customers.Add(c2);

        }

        [Route("Customers")]
        public ActionResult List()
        {
            return View(_customers);
        }

        [Route("Customers/Details/{id}")]
        public ActionResult Detail(int id)
        {
            Customer customerDetail = null;
            foreach(var customer in _customers)
            {
                if(customer.Id == id)
                {
                    customerDetail = customer;
                }
            }

            if (customerDetail == null)
            {
                return NotFound();
            }
            else
            {
                return View(customerDetail);
            }

            
        }       

      
    }
}
