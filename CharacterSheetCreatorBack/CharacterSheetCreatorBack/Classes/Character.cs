using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CharacterSheetCreatorBack.Classes
{
    public class Character
    {
        [Key]
        public int ID { get; set; }
        public ulong IdGame { get; set; }
        public ulong IdPlayer { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string RaceName { get; set; } = string.Empty;
        public int Level { get; set; }
        public int Ac {  get; set; }
        public int SpellSaveDC { get; set; }
        public int SpellCastAbility { get; set; }
        public int Initiative { get; set; }
        public int Hp { get; set; }
        public int HpMax { get; set; }
        public int HitDiceNumber { get; set; }
        public int HitDiceValue { get; set; }
        public List<int> Stats { get; set; } = new List<int>(); // str, dex, cons, ...
        public List<int> Skills { get; set; } = new List<int>(); // saving throws + abilities
        public List<bool> Proefficiencies { get; set; } = new List<bool>();
        public List<Attack> Attacks { get; set; } = new List<Attack>();
        public int ProefficiencyBonus { get; set; }
        public int PassivePerception { get; set; }

        /**********************************************************************/
        /* costructor                                                         */
        /**********************************************************************/

        public Character()
        {
        }

        public Character(ulong idPlayer, ulong idGame) {
            this.IdPlayer = idPlayer;
            this.IdGame = idGame;

            for (int i = 0; i < 6; ++i) {
                this.Stats.Add(10);
            }

            for (int i = 0; i < 24; ++i) {
                this.Skills.Add(0);
                this.Proefficiencies.Add(false);
            }
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

        public string GetStats()
        {
            string result = "";

            for (int i = 0; i < 6; ++i) {
                Ability ability = (Ability) i;
                bool isProefficient = Proefficiencies[i];
                int bonus = ComputeStatBonus(Stats[i]);
                string sign = bonus > 0 ? "+" : "";

                result += $"- {ability.ToString()}: {Stats[i]} [{sign}{bonus}]\n";
            }
            return result;
        }

        public string GetWeapons()
        {
            string result = "";

            foreach (Attack attack in Attacks) {
                result += $"- {attack.Display()}\n";
            }
            return result;
        }

        public string GetSavingThrows()
        {
            string result = "";

            for (int i = 0; i < 6; ++i) {
                Ability ability = (Ability) i;
                bool isProefficient = Proefficiencies[i];
                int bonus = ComputeStatBonus(Stats[i], isProefficient);
                string sign = bonus > 0 ? "+" : "";
                string proefficientSign = isProefficient ? "x" : " ";

                result += $"- [{proefficientSign}] {ability.ToString()}: {Stats[i]} [{sign}{bonus}]\n";
            }
            return result;
        }

        public string GetSkills()
        {
            string result = "";

            for (int i = 6; i < 24; ++i) {
                Ability ability = (Ability) i;
                bool isProefficient = Proefficiencies[i];
                int bonus = ComputeStatBonus(Skills[i], isProefficient);
                string sign = bonus > 0 ? "+" : "";
                string proefficientSign = isProefficient ? "x" : " ";

                result += $"- [{proefficientSign}] {ability.ToString()}: {Skills[i]} [{sign}{bonus}]\n";
            }
            return result;
        }

        public string GetInfos()
        {
            string result = "";

            result += $"- Name: {Name}\n";
            result += $"- Hp: {Hp}\n";
            result += $"- HpMax: {HpMax}\n";
            result += $"- Initiative: {Initiative}\n";
            result += $"- Spell save DC: {SpellSaveDC}\n";
            result += $"- Spell casting ability: {SpellCastAbility}\n";
            return result;
        }

        /**********************************************************************/
        /* roll                                                               */
        /**********************************************************************/

        public string RollInitiative()
        {
            var rand = new System.Random();
            int diceResult = rand.Next() % 20 + 1;
            int result = diceResult + Initiative;
            return $"{Name} rolled {result} for initiative ({diceResult} + {Initiative})";
        }

        private int ComputeStatBonus(int stat)
        {
            double tmp = (stat - 10) / 2.0;
            int result = (int) Math.Ceiling(tmp);
            return result;
        }

        private int ComputeStatBonus(int stat, bool isProefficient)
        {
            int result = ComputeStatBonus(stat);

            if (isProefficient)
            {
                result += ProefficiencyBonus;
            }
            return result;
        }

        public string RollAny(string abilityName)
        {
            Console.WriteLine($"begin roll any: {abilityName}");
            var rand = new System.Random();
            int index = (int) AbilityFromString(abilityName);
            Console.WriteLine($"index: {index}");
            Console.WriteLine($"Proefficiencies.Count: {Proefficiencies.Count}");
            Console.WriteLine($"Stats.Count: {Stats.Count}");
            bool isProefficient = Proefficiencies[index];
            int stat = Stats[index];
            int bonus = ComputeStatBonus(stat, isProefficient);
            int diceResult = rand.Next() % 20 + 1;
            int result = diceResult + bonus;

            return $"{Name} rolled {result} for {abilityName} ({diceResult} + {bonus})";
        }

        public string RollAttack(int index)
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
            int diceResult = rand.Next() % 20 + 1;
            int result = diceResult + bonus;

            return $"{Name} rolled {result} for attacking with {attack.Name} ({diceResult} + {bonus})";
        }

        public string RollDamage(int index)
        {
            if (index >= Attacks.Count)
            {
                throw new Exception("Error: invalid index");
            }

            string detailedResult = "";
            var rand = new System.Random();
            Attack attack = Attacks[index];
            int result = attack.DamageBonus;

            // roll all dices
            for (int i = 0; i < attack.NbDices; ++i) {
                int diceResult = rand.Next() % attack.DicesFaces + 1;
                result += diceResult;
                detailedResult += $"{diceResult} + ";
            }
            detailedResult += $"{attack.DamageBonus}";

            return $"{Name} did {result} damage points with {attack.Name} ({detailedResult})";
        }

        /**********************************************************************/
        /* Print for debugging                                                */
        /**********************************************************************/

        public void Print()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("ClassName: " + ClassName);
            Console.WriteLine("RaceName: " + RaceName);
            Console.WriteLine("Level: " + Level);
            Console.WriteLine("Ac: " + Ac);
            Console.WriteLine("SpellSaveDc: " + SpellSaveDC);
            Console.WriteLine("SpellCastAbility: " + SpellCastAbility);
            Console.WriteLine("Hp: " + Hp);
            Console.WriteLine("HpMax: " + HpMax);
            Console.WriteLine("attacs: [");
            Attacks.ForEach(attack => attack.Print());
            Console.WriteLine("]");
        }

        /**********************************************************************/
        /* convertion                                                         */
        /**********************************************************************/

        private Ability AbilityFromString(string str)
        {
            Hashtable abilitiesStr = new Hashtable()
            {
                { "str", Ability.Strength },
                { "strength", Ability.Strength },
                { "dex", Ability.Dexterity },
                { "dexterity", Ability.Dexterity },
                { "const", Ability.Constitution },
                { "constitution", Ability.Constitution },
                { "int", Ability.Intelligence },
                { "intelligence", Ability.Intelligence },
                { "wis", Ability.Wisdom },
                { "wisdom", Ability.Wisdom },
                { "char", Ability.Charisma },
                { "charisma", Ability.Charisma },
                { "acro", Ability.Acrobatics },
                { "acrobatics", Ability.Acrobatics },
                { "ah", Ability.Animal_handling },
                { "animal handling", Ability.Animal_handling },
                { "arc", Ability.Arcana },
                { "arcana", Ability.Arcana },
                { "ath", Ability.Athletics },
                { "athletics", Ability.Athletics },
                { "dec", Ability.Deception },
                { "deception", Ability.Deception },
                { "his", Ability.History },
                { "history", Ability.History },
                { "ins", Ability.Insight },
                { "insight", Ability.Insight },
                { "intim", Ability.Intimidation },
                { "intimidation", Ability.Intimidation },
                { "inv", Ability.Investigation },
                { "investigation", Ability.Investigation },
                { "med", Ability.Medicine },
                { "medicine", Ability.Medicine },
                { "nat", Ability.Nature },
                { "nature", Ability.Nature },
                { "perc", Ability.Perception },
                { "Perception", Ability.Perception },
                { "perf", Ability.Performance },
                { "performance", Ability.Performance },
                { "pers", Ability.Persuasion },
                { "persuasion", Ability.Persuasion },
                { "rel", Ability.Religion },
                { "religion", Ability.Religion },
                { "soh", Ability.Sleight_of_hand },
                { "sleight of hand", Ability.Sleight_of_hand },
                { "ste", Ability.Stealth },
                { "stealth", Ability.Stealth },
                { "sur", Ability.Survival },
                { "survival", Ability.Survival }
            };
            string strLower = str.ToLower();
            Console.WriteLine($"rolling: {strLower}");

            if (!abilitiesStr.ContainsKey(strLower))
            {
                Console.WriteLine("invalid argument");
                throw new InvalidOperationException();
            }

            return (Ability) abilitiesStr[strLower];
        }
    }
}
