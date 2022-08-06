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

        [Authorize(Roles = RoleName.CanManageMovies + "," + RoleName.ReadOnlyUser)]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = RoleName.CanManageMovies + "," + RoleName.ReadOnlyUser)]
        public ActionResult List()
        {
            if (User.IsInRole(RoleName.ReadOnlyUser))
            {
                return View("ReadOnlyList");
            }
            return View("List");
        }

        [Route("Movies/Details/{id}")]
        [Authorize(Roles = RoleName.CanManageMovies + "," + RoleName.ReadOnlyUser)]

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
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _ctx.Genres.ToList();
            MovieFormViewModel movievm = new MovieFormViewModel();
            movievm.Genres = genres;

            return View("MovieForm", movievm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var vm = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _ctx.Genres.ToList()
                };
                return View("MovieForm", vm);
            }
            if (movie.Id == 0)
            {
                _ctx.Movies.Add(movie);

            }
            else
            {
                var movieInDb = _ctx.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;

            }
            _ctx.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _ctx.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                var vm = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _ctx.Genres.ToList()
                };
                return View("MovieForm", vm);
            }
        }
    }
}
