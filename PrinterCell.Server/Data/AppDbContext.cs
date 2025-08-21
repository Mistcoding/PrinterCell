using Microsoft.EntityFrameworkCore;
using PrinterCell.Shared.Models;

namespace PrinterCell.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Articolo> Articolo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Articolo>().HasIndex(x => x.Codice).IsUnique();

        }
    }
}
