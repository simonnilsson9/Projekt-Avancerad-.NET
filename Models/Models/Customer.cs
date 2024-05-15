using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Name must be between 2-40 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phonenumber is required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Must be a phonenumber.")]

        public string Phone { get; set; }
        [Required(ErrorMessage = "E-mail is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Must be a e-mail adress.")]
        public string Email { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
