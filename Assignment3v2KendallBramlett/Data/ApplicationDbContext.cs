using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment3v2KendallBramlett.Models;

namespace Assignment3v2KendallBramlett.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Assignment3v2KendallBramlett.Models.Actors> Actors { get; set; }
        public DbSet<Assignment3v2KendallBramlett.Models.Movies> Movies { get; set; }
        public DbSet<Assignment3v2KendallBramlett.Models.MoviesActors> MoviesActors { get; set; }
    }
}