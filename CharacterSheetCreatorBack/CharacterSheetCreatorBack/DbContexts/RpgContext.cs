using Microsoft.EntityFrameworkCore;
using CharacterSheetCreatorBack.Classes;


namespace CharacterSheetCreatorBack.DbContexts 
{
    public class RpgContext : DbContext
    {
        public DbSet<Spell> Spells { get; set; }

        public RpgContext(DbContextOptions<RpgContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spell>().ToTable("Spells"); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=RPG;Trusted_Connection=True;TrustServerCertificate=true;");
        }

    }
}
