using LiquorWebshop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LiquorWebshop.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Whisky> Whiskies { get; set; }
    }
}
