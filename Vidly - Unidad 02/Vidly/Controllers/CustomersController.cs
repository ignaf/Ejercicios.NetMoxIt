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
        
        [Route("Customers")]
        public ActionResult List()
        {
            List<Customer> customers = new List<Customer>();
            Customer c1 = new Customer { Name = "John Smith" };
            Customer c2 = new Customer { Name = "Mary Williams" };
            customers.Add(c1);
            customers.Add(c2);

            return View(customers);
        }

      
    }
}
