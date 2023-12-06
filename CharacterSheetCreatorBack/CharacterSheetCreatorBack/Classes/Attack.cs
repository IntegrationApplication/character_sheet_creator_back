using System.ComponentModel.DataAnnotations;

namespace CharacterSheetCreatorBack.Classes
{
    public class Attack
    {
        [Key]
        public int Id { get; set; }
        public Ability LinkedAbility { get; set; }
        public string DamageType { get; set; } = string.Empty;
        public int NbDices { get; set; } = default(int);
        public int DicesFaces { get; set; } = default(int);
        public int DamageBonus { get; set; } = default(int);
    }
}
