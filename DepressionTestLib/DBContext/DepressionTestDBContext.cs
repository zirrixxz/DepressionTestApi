using DepressionTestLib.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DepressionTestLib.DBContext
{
    public partial class DepressionTestDBContext : IdentityDbContext<User>
    {
        public DepressionTestDBContext() : base()
        {

        }

        public DepressionTestDBContext(DbContextOptions<DepressionTestDBContext> options) : base(options)
        {

        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<DepressionTestHistory> DepressionTestHistory { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
     
    }
   
}
