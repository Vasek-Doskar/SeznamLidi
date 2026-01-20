using Microsoft.EntityFrameworkCore;
using SeznamLidi.Models;

namespace SeznamLidi.Database
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public PersonContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // soubor naší databáze (soubor sqlite)
        //    optionsBuilder.UseSqlite("Data Source=seznamlidi.db");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // data seed
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, FirstName = "Jan", LastName = "Novák", Age = 30, },
                new Person { Id = 2, FirstName = "Petr", LastName = "Svoboda", Age = 25, }
                );
        }
    }
}
