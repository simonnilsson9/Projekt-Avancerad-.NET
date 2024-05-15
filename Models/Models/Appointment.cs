using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        [Range(typeof(DateTime), "01-05-2024", "01-01-2026", ErrorMessage = "Date must be between 1/5-2024 until 1/1-2026.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "CustomerID is required.")]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        [Required(ErrorMessage = "CompanyID is required.")]
        public int CompanyId { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }
    }
}
