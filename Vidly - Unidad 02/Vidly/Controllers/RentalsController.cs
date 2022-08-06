using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        
        public ActionResult New()
        {
            return View();
        }

       
    }
}
