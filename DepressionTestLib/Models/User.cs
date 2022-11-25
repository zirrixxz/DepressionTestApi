using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Models
{
    [Table("User")]
    public class User

    {
        [Key]
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public int Age { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Phone { get; set; }   
        public string Email { get; set; }
        public int Year { get; set; }
        public string Faculty { get; set; }
    }
}
