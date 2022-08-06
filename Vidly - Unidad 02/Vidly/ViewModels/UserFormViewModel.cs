using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class UserFormViewModel
    {
        public IEnumerable<Role> Roles { get; set; }

        public User User { get; set; }
    }
}
