using LotoApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LotoApp.DataAccess
{
    public class LotoAppDbContext :DbContext
    {
        public LotoAppDbContext(DbContextOptions<LotoAppDbContext> options) : base(options){}

        public DbSet<User> Users {get; set;}
        public DbSet<Session> Sessions {get; set;}
        public DbSet<Ticket> Tickets {get; set;}
        public DbSet<Draw> Draws {get; set;}
        public DbSet<Winner> Winners {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
