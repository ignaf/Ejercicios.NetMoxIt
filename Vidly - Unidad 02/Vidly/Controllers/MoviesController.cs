using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private List<Movie> _movies = new List<Movie>();
        private Movie m1 = new Movie{ Name = "Shrek", Id = 1, Imdb= "https://www.imdb.com/title/tt0126029/" };
        private Movie m2 = new Movie { Name = "Wall-E", Id = 2, Imdb= "https://www.imdb.com/title/tt0910970/" };

        public MoviesController()
        {
            _movies.Add(m1);
            _movies.Add(m2);
        }


        public ActionResult Index()
        {
            return View();
        }               

        public ActionResult List()
        {
            return View(_movies);
        }
    }
}
