using DepressionTestLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.DBContext
{
    public partial class DepressionTestDBContext : DbContext
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
