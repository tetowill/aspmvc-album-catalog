using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess
{
    public class MediaDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Person> Users { get; set; }
    }
}
