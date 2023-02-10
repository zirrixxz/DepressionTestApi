using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Helpers
{
    public class EditUserRequest
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Year { get; set; }
        public string? Faculty { get; set; }
        public int Telephone { get; set; }

    }
}
