using Microsoft.EntityFrameworkCore;
using CharacterSheetCreatorBack.Classes;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace CharacterSheetCreatorBack.DbContexts
{
    public class RpgContext : DbContext
    {
        /* public DbSet<Spell> Spells { get; set; } */

        /* public DbSet<Ability> Abilities { get; set; } */

        /* public DbSet<Skill> Skills { get; set; } */

        /* public DbSet<Class> Classes { get; set; } */

        public DbSet<Attack> Attacks { get; set; }

        public DbSet<Character> Characters { get; set; }


        public RpgContext(DbContextOptions<RpgContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* modelBuilder.Entity<Spell>().ToTable("Spells"); */

            /* modelBuilder.Entity<Ability>().ToTable("Abilities"); */

            /* modelBuilder.Entity<Skill>().ToTable("Skills"); */

            /* modelBuilder.Entity<Class>().ToTable("Classes"); */

            modelBuilder.Entity<Attack>().ToTable("Attacks");

            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("Characters");
                /* entity.HasMany<Ability>(c => c.Abilities); */
                /* entity.HasMany<Spell>(c => c.Spells); */
                entity.HasMany<Attack>(c => c.Attacks);
                /* entity.HasMany<Skill>(c => c.Skills); */
                /* entity.HasOne<Class>(c => c.Classe); */
            });

        }

        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) */
        /* { */
        /*     base.OnConfiguring(optionsBuilder); */
        /*     optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=RPG;Trusted_Connection=True;TrustServerCertificate=true;"); */
        /* } */
    }
}
