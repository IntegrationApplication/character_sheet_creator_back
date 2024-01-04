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

        public Attack() {
        }

        public Attack(Attack other) {
            Update(other);
        }

        public void Update(Attack other) {
            this.LinkedAbility = other.LinkedAbility;
            this.DamageType = other.DamageType;
            this.NbDices = other.NbDices;
            this.DicesFaces = other.DicesFaces;
            this.DamageBonus = other.DamageBonus;
        }
    }
}
