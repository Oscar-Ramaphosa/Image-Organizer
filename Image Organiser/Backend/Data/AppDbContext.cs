using ImageOrganizer.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageOrganizer.Api.Data
{
    // DbContext represents a session with the database
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // This maps to the Images table
        public DbSet<ImageEntity> Images { get; set; }
    }
}
