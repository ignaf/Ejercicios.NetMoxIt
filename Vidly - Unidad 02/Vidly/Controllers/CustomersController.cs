using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.DAL;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    // [Authorize(Roles = RoleName.CanManageMovies + "," + RoleName.ReadOnlyUser)]
    [Authorize(Roles =RoleName.CanManageMovies)]
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
            return View();
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
        public ActionResult New()
        {
            var membershipTypes = _ctx.MembershipTypes.ToList();
            var customervm = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = new Customer()
            };          
            

            return View("CustomerForm", customervm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var vm = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _ctx.MembershipTypes.ToList()
                };
            return View("CustomerForm", vm);
            }
            
            if (customer.Id == 0) { 
            _ctx.Customers.Add(customer);
           
            }
            else
            {
                var customerInDb = _ctx.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
            _ctx.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        public ActionResult Edit(int id)
        {
            var customer = _ctx.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                var vm = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _ctx.MembershipTypes.ToList()
                };
                return View("CustomerForm", vm);
            }
        }

    }
}
