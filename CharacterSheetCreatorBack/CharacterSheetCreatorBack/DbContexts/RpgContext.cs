using Microsoft.EntityFrameworkCore;
using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.Models;


namespace CharacterSheetCreatorBack.DbContexts
{
    public class RpgContext : DbContext
    {
        public DbSet<Attack> Attacks { get; set; }
        public DbSet<CharacterModel> Characters { get; set; }


        public RpgContext(DbContextOptions<RpgContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attack>().ToTable("Attacks");
            modelBuilder.Entity<CharacterModel>()
                .ToTable("Characters")
                .HasMany(c => c.Attacks);
        }
    }
}
