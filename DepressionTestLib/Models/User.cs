using DepressionTestLib.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Models
{
    public class User : IdentityUser 
    {
        public string? UserId { get; set; }
       // public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }    
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
       
        public string? Year { get; set; }
        public string? Faculty { get; set; }

        public string? RoleName { get; set; }
    }
}
