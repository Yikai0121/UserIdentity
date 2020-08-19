using Microsoft.EntityFrameworkCore;
using UserIdentityPr.Models;

namespace UserIdentityPr.Data
{
    public class AlbumDbcontext : DbContext
    {
        public AlbumDbcontext(DbContextOptions<AlbumDbcontext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        }
        public DbSet<Album> Albums { get; set; }
    }
}
