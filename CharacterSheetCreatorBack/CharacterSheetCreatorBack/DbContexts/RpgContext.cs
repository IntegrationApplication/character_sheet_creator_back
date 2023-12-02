using Microsoft.EntityFrameworkCore;
using CharacterSheetCreatorBack.Classes;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace CharacterSheetCreatorBack.DbContexts 
{
    public class RpgContext : DbContext
    {
        public DbSet<Spell> Spells { get; set; }

        public DbSet<Ability> Abilities { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Attack> Attacks { get; set; }

        public DbSet<Character> Characters { get; set; }


        public RpgContext(DbContextOptions<RpgContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spell>().ToTable("Spells");

            modelBuilder.Entity<Ability>().ToTable("Abilities");

            modelBuilder.Entity<Skill>().ToTable("Skills");

            modelBuilder.Entity<Class>().ToTable("Classes");

            modelBuilder.Entity<Attack>().ToTable("Attacks").Property(a => a.DamageDice)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.None).Select(int.Parse).ToArray()
            ).Metadata.SetValueComparer(new ValueComparer<int[]>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToArray()
        ));

            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("Characters");
                entity.HasMany<Ability>(c => c.Abilities);
                entity.HasOne<Class>(c => c.Classe);
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=RPG;Trusted_Connection=True;TrustServerCertificate=true;");
        }

    }
}
