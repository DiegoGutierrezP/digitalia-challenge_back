using BackendDigitaliaChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendDigitaliaChallenge.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recibo>()
                .HasOne<Usuario>(r => r.usuario)
                .WithMany(u => u.recibos)
                .HasForeignKey(r => r.usuarioId);
        }

            public AppDbContext() { }
        public DbSet<Recibo> recibo { get; set; }
        public DbSet<Usuario> usuario { get; set; }
    }
}
