using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3-30 characters.")]
        public string Name { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
