using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.DAL;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private MyDbContext _ctx;

        public CustomersController(MyDbContext context)
        {
            _ctx = context;
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }

        [Route("Customers")]
        public ActionResult List()
        {
            var customers = _ctx.Customers.Include(c=>c.MembershipType).ToList();

            return View(customers);
        }

        [Route("Customers/Details/{id}")]
        public ActionResult Detail(int id)
        {
            var customer = _ctx.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);


            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return View(customer);
            }


        }


    }
}
