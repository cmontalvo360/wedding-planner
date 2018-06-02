using System;
using Microsoft.EntityFrameworkCore;

namespace Wedding_Planner.Models
{
    public class WeddingsContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingsContext(DbContextOptions<WeddingsContext> options) : base(options) { }
        public DbSet<Wedding> weddings { get; set; }
        public DbSet<User> users {get; set; }
        public DbSet<GuestList> users_weddings {get; set; }

        internal Wedding LastOrDefault(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}