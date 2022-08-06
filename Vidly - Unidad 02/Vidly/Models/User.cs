using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]        
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public int PhoneNumber { get; set; }

        [Required]
        public int DriverLicense { get; set; }


        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public Role Role { get; set; }

        [Required]
        public byte RoleId { get; set; }
       

    }
}
