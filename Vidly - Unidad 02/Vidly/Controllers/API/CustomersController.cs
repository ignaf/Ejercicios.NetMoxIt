﻿using AutoMapper;
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
    public class CustomersController : ControllerBase
    {
        private MyDbContext _ctx;
        private IMapper _mapper;

        public CustomersController(MyDbContext context, IMapper mapper)
        {
            _ctx = context;
            _mapper = mapper;
        }

        //GET /api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _ctx.Customers.ToList().Select(_mapper.Map<Customer, CustomerDto>);
        }

        //GET /api/customers/1
        [HttpGet("{id}")]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var customer = _ctx.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/customers
        [HttpPost]
        public ActionResult<CustomerDto> CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();
            customerDto.Id = customer.Id;


            return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customerDto);
        }

        //PUT /api/customers/1
        [HttpPut]
        public IActionResult UpdateCostumer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customerInDb = _ctx.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }
            _mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);            

            _ctx.SaveChanges();
            return Ok();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            var customerInDb = _ctx.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }
            _ctx.Customers.Remove(customerInDb);
            _ctx.SaveChanges();
            return Ok();
        }

 
    }
}
