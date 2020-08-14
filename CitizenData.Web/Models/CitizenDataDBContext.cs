using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenData.Web.Models
{
    public class CitizenDataDBContext : DbContext
    {
        public CitizenDataDBContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }
}
