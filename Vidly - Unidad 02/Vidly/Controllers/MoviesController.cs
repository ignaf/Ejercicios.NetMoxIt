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
    public class MoviesController : Controller
    {

        private MyDbContext _ctx;

        public MoviesController(MyDbContext context)
        {
            _ctx = context;
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }               

        public ActionResult List()
        {
            List<Movie> movies = _ctx.Movies.Include(m => m.Genre).ToList();
            
            return View(movies);
        }

        [Route("Movies/Details/{id}")]
        public ActionResult Detail(int id)
        {
            var movie = _ctx.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);


            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return View(movie);
            }


        }
    }
}
