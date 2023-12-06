using System.ComponentModel.DataAnnotations;

namespace CharacterSheetCreatorBack.Classes
{
    public class Character
    {
        [Key]
        public int ID { get; set; } = default(int);
        public int IdGame { get; set; }
        public int IdPlayer { get; set; }
        public string Name { get; set; }

        public string ClassName { get; set; }

        public int Level { get; set; }

        public int Ac {  get; set; }

        public int SpellSaveDC { get; set; }

        public int SpeelCastAbility { get; set; }

        public int Initiative { get; set; }

        public int Hp { get; set; }

        public int HpMax { get; set; }

        public int HitDice { get; set; }

        public List<int> Stats { get; set; }

        public List<bool> Proefficiencies { get; set; }

        public List<Attack> Attacks { get; set; }

        public int ProefficiencyBonus { get; set; }

        public int PassivePerception { get; set; }


        public Character()
        {
            Name = string.Empty;
            Level = 0;
            Ac = 0;
            SpellSaveDC = 0;
            SpeelCastAbility = 0;
            Initiative = 0;
            Hp = 0;
            HpMax = 0;
            HitDice = 0;
            Stats = new List<int>(24);
            Proefficiencies = new List<bool>(24);
            Attacks = new List<Attack>();
            ProefficiencyBonus = 0;
        }

        /**********************************************************************/
        /* roll                                                               */
        /**********************************************************************/

        /*public int RollAbility(string abilityName)
        {
            Ability? ability = Stats.Find(x => x.Name == abilityName);

            if (ability is null)
            {
                throw new Exception("Error: ability not found !");
            }
            return ability.Roll();
        }

        public int RollSkill(string skillName)
        {
            Skill? skill = Skills.Find(s => s.Name == skillName);

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

            Ability? ability = Stats.Find(x => x.Name == name);
            if (ability is not null)
            {
                return ability.Roll();
            }

            Skill? skill = Skills.Find(s => s.Name == name);
            if (skill is not null)
            {
                return skill.Roll();
            }

            throw new Exception(@"Error: {name} is not rollable.");
        }

        public int RollAttack(int index)
        {
            if (index >= Attacks.Count)
            {
                throw new Exception("Error: invalid index");
            }
            Attack attack = Attacks[index];
            return attack.Roll();
        }*/
    }
}
