using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class LoginInfo
    {
        [Key]
        public int LoginId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]        
        public string Role { get; set; }
        public Company Company { get; set; }
        public int? CompanyId { get; set; }
        public Customer Customer { get; set; }
        public int? CustomerId { get; set; }
    }
}
