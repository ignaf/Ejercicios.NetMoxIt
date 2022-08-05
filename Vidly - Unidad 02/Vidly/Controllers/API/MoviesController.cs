using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.DAL;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private MyDbContext _ctx;
        private IMapper _mapper;

        public MoviesController(MyDbContext context, IMapper mapper)
        {
            _ctx = context;
            _mapper = mapper;
        }

        //GET /api/movies
        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
            return _ctx.Movies.ToList().Select(_mapper.Map<Movie, MovieDto>);
        }

        //GET /api/movies/1
        [HttpGet("{id}")]
        public ActionResult<MovieDto> GetMovie(int id)
        {
            var movie = _ctx.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/movies
        [HttpPost]
        public ActionResult<MovieDto> CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _ctx.Movies.Add(movie);
            _ctx.SaveChanges();
            movieDto.Id = movie.Id;


            return CreatedAtAction(nameof(GetMovies), new { id = movie.Id }, movieDto);
        }

        //PUT /api/movies/1
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movieInDb = _ctx.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }
            _mapper.Map<MovieDto, Movie>(movieDto, movieInDb);

            _ctx.SaveChanges();
            return Ok();
        }

        //DELETE /api/movies/1
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteMovie(int id)
        {
            var movieInDb = _ctx.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }
            _ctx.Movies.Remove(movieInDb);
            _ctx.SaveChanges();
            return Ok();
        }

 
    }
}
