using System.ComponentModel.DataAnnotations;

namespace CharacterSheetCreatorBack.Classes
{
    public class Character
    {
        [Key]
        public int ID { get; set; }
        public int IdGame { get; set; }
        public int IdPlayer { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string RaceName { get; set; }
        public int Level { get; set; }
        public int Ac {  get; set; }
        public int SpellSaveDC { get; set; }
        public int SpeelCastAbility { get; set; }
        public int Initiative { get; set; }
        public int Hp { get; set; }
        public int HpMax { get; set; }
        public int HitDiceNumber { get; set; }
        public int HitDiceValue { get; set; }
        public List<int> Stats { get; set; } // str, dex, cons, ...
        public List<int> Skills { get; set; } // saving throws + abilities
        public List<bool> Proefficiencies { get; set; }
        public List<Attack> Attacks { get; set; }
        public int ProefficiencyBonus { get; set; }
        public int PassivePerception { get; set; }

        /**********************************************************************/
        /* costructor                                                         */
        /**********************************************************************/

        public Character()
        {
            Name = string.Empty;
            Stats = new List<int>(6);
            Skills = new List<int>(24);
            Proefficiencies = new List<bool>(24);
            Attacks = new List<Attack>();
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
            return attack.DamageType;
        }

        /**********************************************************************/
        /* roll                                                               */
        /**********************************************************************/

        public int RollInitiative()
        {
            var rand = new System.Random();
            return rand.Next() % 20 + 1 + Initiative;
        }

        private int ComputeStatBonus(int stat, bool isProefficient)
        {
            double tmp = (stat - 10) / 2.0;
            int result = (int) Math.Ceiling(tmp);

            if (isProefficient)
            {
                result += ProefficiencyBonus;
            }
            return result;
        }

        public int RollAny(string abilityName)
        {
            var rand = new System.Random();
            // not working ?
            /* BUG: int index = (int) Ability.parse(abilityName); */
            int index = 0;
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
            var rand = new System.Random();
            Attack attack = Attacks[index];
            bool isProefficient = Proefficiencies[(int) attack.LinkedAbility];
            int stat = Stats[(int) attack.LinkedAbility];
            int bonus = ComputeStatBonus(stat, isProefficient);

            return rand.Next() % 20 + 1 + bonus;
        }

        public int RollDamage(int index)
        {
            if (index >= Attacks.Count)
            {
                throw new Exception("Error: invalid index");
            }
            var rand = new System.Random();

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
