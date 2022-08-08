﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class RentalDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public List<int> MovieIds { get; set; }
    }
}
