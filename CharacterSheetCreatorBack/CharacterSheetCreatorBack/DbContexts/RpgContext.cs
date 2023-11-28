using Microsoft.EntityFrameworkCore;
using CharacterSheetCreatorBack.Classes;


namespace CharacterSheetCreatorBack.DbContexts
{
    public class RpgContext : DbContext
    {
        public DbSet<Character> Characters { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Spell> Spells { get; set; } = null!;

        public RpgContext(DbContextOptions<RpgContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().ToTable("Characters");
            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Spell>().ToTable("Spells");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=RPG;Trusted_Connection=True;TrustServerCertificate=true;");
        }

    }
}
