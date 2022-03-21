using Microsoft.EntityFrameworkCore;
namespace Models
{
    public class BolnicaContext : DbContext
    {
        public DbSet<Bolnica> Bolnice {get;set;}
        public DbSet<Doktor> Doktori{get;set;}
        public DbSet<Odeljenje> Odeljenja{get;set;}
        public DbSet<Pacijent> Pacijenti {get;set;}
        public DbSet<Spoj> Spojevi {get;set;}
        public BolnicaContext(DbContextOptions options):base(options){
            
        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<Pacijent>()
                .HasMany<Spoj>(s => s.Spojevi)
                .WithOne(s=>s.Pacijent);
            mb.Entity<Doktor>()
                .HasMany<Spoj>(s => s.Spojevi)
                .WithOne(s=>s.Doktor);
        }
    }
}