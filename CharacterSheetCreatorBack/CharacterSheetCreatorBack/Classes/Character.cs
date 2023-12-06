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
        /* accessors                                                          */
        /**********************************************************************/

        public string GetDamageType(int index)
        {
            if (index >= Attacks.Count)
            {
                throw new Exception("Error: invalid index");
            }
            Attack attack = Attacks[index];
            return attack.GetDamageType;
        }

        /**********************************************************************/
        /* roll                                                               */
        /**********************************************************************/

        public RollInitiative()
        {
            var rand = System.Random();
            return rand.Next() % 20 + 1 + Initiative;
        }

        private ComputeStatBonus(int stat, bool isProefficient)
        {
            int result = Math.Ceiling((stat - 10) / 2);

            if (isProefficient)
            {
                result += ProefficiencyBonus;
            }
            return result;
        }

        public RollAny(string abilityName)
        {
            var rand = System.Random();
            int index = (int) Ability.parse(abilityName);
            bool isProefficient = Proefficiencies[index];
            int stat = Stats[index];
            int bonus = ComputeStatBonus(stat, isProefficient);

            return rand.Next() % 20 + 1 + bonus;
        }

        public int RollAttack(int index)
        {
            if (index >= Attacks.Count)
            {
                throw new Exception("Error: invalid index");
            }
            var rand = System.Random();
            Attack attack = Attacks[index];
            bool isProefficient = Proefficiencies[(int) Attacks.LinkedAbility];
            int stat = Stats[(int) Attacks.LinkedAbility];
            int bonus = ComputeStatBonus(stat, isProefficient);

            return rand.Next() % 20 + 1 + bonus;
        }

        public int RollDamage(int index)
        {
            if (index >= Attacks.Count)
            {
                throw new Exception("Error: invalid index");
            }
            var rand = System.Random();

            Attack attack = Attacks[index];
            int result = attack.DamageBonus;
            // roll all dices
            for (int i = 0; i < attack.NbDices; ++i) {
                result += rand.Next() % attack.DicesFaces + 1;
            }

            return result;
        }
    }
}
