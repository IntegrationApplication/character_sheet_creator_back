using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.Models;
using System.ComponentModel.DataAnnotations;

namespace CharacterSheetCreatorBack.DAL.Models
{
    public class AttackModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Ability LinkedAbility { get; set; }
        public string DamageType { get; set; } = string.Empty;
        public int NbDices { get; set; } = default(int);
        public int DicesFaces { get; set; } = default(int);
        public int DamageBonus { get; set; } = default(int);
        // foreign key
        public int CharacterModelId { get; set; }
        public CharacterModel CharacterModel { get; set; } = null!;

        /**********************************************************************/
        /* costructors                                                        */
        /**********************************************************************/

        public AttackModel()
        {
        }

        /**********************************************************************/
        /* functions                                                          */
        /**********************************************************************/

        public void FromAttack(Attack other)
        {
            this.Name = other.Name;
            this.LinkedAbility = other.LinkedAbility;
            this.DamageType = other.DamageType;
            this.NbDices = other.NbDices;
            this.DicesFaces = other.DicesFaces;
            this.DamageBonus = other.DamageBonus;
        }
    }
}
