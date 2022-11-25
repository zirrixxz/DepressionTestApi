using DepressionTestLib.DBContext;
using DepressionTestLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Data
{
    public class UserManager

    {
        DepressionTestDBContext db;

        public UserManager(DepressionTestDBContext db)
        {
            this.db = db;
        }
        public List<User> GetUser()
        {
            List<User> list = new List<User>();
            return list;
        }
    }
}
