using Microsoft.EntityFrameworkCore;
using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.Models;
using CharacterSheetCreatorBack.DAL.Models;


namespace CharacterSheetCreatorBack.DbContexts
{
    public class RpgContext : DbContext
    {
        public DbSet<AttackModel> Attacks { get; set; }
        public DbSet<CharacterModel> Characters { get; set; }


        public RpgContext(DbContextOptions<RpgContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttackModel>().ToTable("Attacks");
            modelBuilder.Entity<CharacterModel>()
                .ToTable("Characters")
                .HasMany(c => c.Attacks)
                .WithOne(a => a.CharacterModel)
                .HasForeignKey(a => a.CharacterModelId)
                .IsRequired(false);
        }
    }
}
