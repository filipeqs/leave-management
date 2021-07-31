using System;
using System.ComponentModel.DataAnnotations;

namespace leave_management.Models
{
    public class DetailsLeaveTypeVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class CreateLeaveTypeVM
    {
        [Required]
        public string Name { get; set; }
    }
}
