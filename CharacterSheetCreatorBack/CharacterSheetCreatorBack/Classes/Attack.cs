using System.ComponentModel.DataAnnotations;

namespace CharacterSheetCreatorBack.Classes
{
    public class Attack
    {
        [Key]
        public int Id { get; set; }
        public Ability LinkedAbility { get; set; }
        public string DamageType { get; set; }
        public int[] DamageDice { get; set; }
        public Attack() { 
        }
    }
}
