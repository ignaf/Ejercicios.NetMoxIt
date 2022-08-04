using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter customer's name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }

        [Required(ErrorMessage ="Please select membership type")]
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        [Min18YearsIfAMember]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

    }
}
