using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Helpers
{
    public class LoginResult
    {
        public string UserId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public string RoleName { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
