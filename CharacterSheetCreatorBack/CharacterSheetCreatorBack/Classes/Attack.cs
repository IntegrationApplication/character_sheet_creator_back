using CharacterSheetCreatorBack.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace CharacterSheetCreatorBack.Classes
{
    public class Attack
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Ability LinkedAbility { get; set; }
        public string DamageType { get; set; } = string.Empty;
        public int NbDices { get; set; } = default(int);
        public int DicesFaces { get; set; } = default(int);
        public int DamageBonus { get; set; } = default(int);

        /**********************************************************************/
        /* costructors                                                        */
        /**********************************************************************/

        public Attack() {
        }

        public Attack(AttackModel model)
        {
            FromModel(model);
        }

        public void FromModel(AttackModel model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.LinkedAbility = model.LinkedAbility;
            this.DamageType = model.DamageType;
            this.NbDices = model.NbDices;
            this.DicesFaces = model.DicesFaces;
            this.DamageBonus = model.DamageBonus;
        }

        /**********************************************************************/
        /* functions                                                          */
        /**********************************************************************/

        public void Print()
        {
            Console.WriteLine("attack: {");
            Console.WriteLine("name: " + Name);
            Console.WriteLine("linkedAbility: " + LinkedAbility.ToString());
            Console.WriteLine("damageType: " + DamageType);
            Console.WriteLine("dice: " + NbDices + "d" + DicesFaces + " + " + DamageBonus);
            Console.WriteLine("}");
        }

        public string Display() {
            return $"{Name} ({LinkedAbility}): {NbDices}d{DicesFaces} + {DamageBonus} ({DamageType})";
        }
    }
}
