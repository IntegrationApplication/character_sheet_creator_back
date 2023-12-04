using System.ComponentModel.DataAnnotations;
using System;

namespace CharacterSheetCreatorBack.Classes
{
    public class Character
    {
        [Key]
        public int ID { get; set; }

        public int IdPlayer { get; set; }

        public string Name { get; set; }

        public Classe Classe { get; set; }

        public Skills Skills { get; set; }

        public List<Ability> Abilities { get; set; }

        public int Level { get; set; }

        public int Ac {  get; set; }

        public int SpellSaveDC { get; set; }

        public int SpeelCastAbility { get; set; }

        public int Initiative { get; set; }

        public int HP { get; set; }

        public int HPMax { get; set; }

        public int HitDice { get; set; }

        public List<Attack> Attacks { get; set; }

        public List<Spell> Spells { get; set; }

        public int ProefficiencyBonus { get; set; }

        public int PassivePerception { get; set; }


        public Character()
        {
            ID = 0;
            Name = string.Empty;
            Classe = new Classe();
            Skills = new Skills();
            Abilities = new List<Ability>();
            Level = 0;
            Ac = 0;
            SpellSaveDC = 0;
            SpeelCastAbility = 0;
            Initiative = 0;
            HP = 0;
            HPMax = 0;
            HitDice = 0;
            Attacks = new List<Attack>();
            Spells = new List<Spell>();
            ProefficiencyBonus = 0;
        }

        /**********************************************************************/
        /* roll                                                               */
        /**********************************************************************/

        public int RollAbility(string abilityName)
        {
            Ability? ability = Abilities.Find(x => x.Name == abilityName);

            if (ability is null)
            {
                throw new Exception("Error: ability not found !");
            }
            return ability.Roll();
        }

        public int RollSkill(string skillName)
        {
            Skill? skill = Skills.Find(skillName);

            if (skill is null)
            {
                throw new Exception("Error: skill not found !");
            }
            return skill.Roll();
        }

        public int RollInitiative() {
            var rand = new System.Random();
            return rand.Next() % 20 + 1 + Initiative;
        }

        public int RollAny(string name)
        {
            if (name == "init" || name == "initiative")
            {
                return RollInitiative();
            }

            Ability? ability = Abilities.Find(x => x.Name == name);
            if (ability is not null)
            {
                return ability.Roll();
            }

            Skill? skill = Skills.Find(name);
            if (skill is not null)
            {
                return skill.Roll();
            }

            throw new Exception(@"Error: {name} is not rollable.");
        }

        public int RollAttack(int index) {
            if (index >= Attacks.Count)
            {
                throw new Exception("Error: invalid index");
            }
            Attack attack = Attacks[index];
            return attack.Roll();
        }
    }
}
