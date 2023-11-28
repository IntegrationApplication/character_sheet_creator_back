using Microsoft.EntityFrameworkCore;
using CharacterSheetCreatorBack.Classes;


namespace CharacterSheetCreatorBack.DbContexts 
{
    public class RpgContext : DbContext
    {
        public DbSet<Spell> Spells { get; set; }

        public DbSet<Ability> Abilities { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Class> Classes { get; set; }


        public RpgContext(DbContextOptions<RpgContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spell>().ToTable("Spells");

            modelBuilder.Entity<Ability>().ToTable("Abilities");

            modelBuilder.Entity<Skill>().ToTable("Skills");

            modelBuilder.Entity<Class>().ToTable("Classes");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=RPG;Trusted_Connection=True;TrustServerCertificate=true;");
        }

    }
}
