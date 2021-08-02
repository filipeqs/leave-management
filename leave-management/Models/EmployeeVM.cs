using System;
using System.ComponentModel.DataAnnotations;

namespace leave_management.Models
{
    public class EmployeeVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Tax ID Number")]
        public string TaxId { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Join Date")]
        public DateTime DateJoined { get; set; }
    }
}
