using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class RentalsController : ControllerBase
    {
        private MyDbContext _ctx;
        private IMapper _mapper;

        public RentalsController(MyDbContext context, IMapper mapper)
        {
            _ctx = context;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<RentalDto> CreateRentals(RentalDto rentalDto)
        {

            var customer = _ctx.Customers.Single(m => m.Id == rentalDto.CustomerId);

            var movies = _ctx.Movies.Where(m => rentalDto.MovieIds.Contains(m.Id)).ToList();         
            

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable != 0)
                {
                    var rental = new Rental
                    {
                        DateRented = DateTime.Today,
                        Customer = customer,
                        Movie = movie
                    };
                    movie.NumberAvailable--;
                    _ctx.Rentals.Add(rental);
                }
                else
                {
                    return BadRequest("Movie is not available.");
                }


            }
            _ctx.SaveChanges();

            return Ok();
        }


    }
}
