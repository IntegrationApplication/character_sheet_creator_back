using System.ComponentModel.DataAnnotations;
using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DAL.Models;

namespace CharacterSheetCreatorBack.Models
{
    public class CharacterModel
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
        public string Stats { get; set; } = string.Empty; // str, dex, cons, ...
        public string Skills { get; set; } = string.Empty; // saving throws + abilities
        public string Proefficiencies { get; set; } = string.Empty;
        public List<AttackModel> Attacks { get; set; } = new List<AttackModel>();
        public int ProefficiencyBonus { get; set; }
        public int PassivePerception { get; set; }

        /**********************************************************************/
        /* costructor                                                         */
        /**********************************************************************/

        public CharacterModel()
        {
            // default constructor for EF
        }

        public CharacterModel(Character character)
        {
            FromCharacter(character);
        }

        /**********************************************************************/
        /* convertion to character                                            */
        /**********************************************************************/

        public Character ToCharacter() {
            Character character = new Character();

            character.ID = this.ID;
            character.IdGame = this.IdGame;
            character.IdPlayer = this.IdPlayer;
            character.Name = this.Name;
            character.ClassName = this.ClassName;
            character.RaceName = this.RaceName;
            character.Level = this.Level;
            character.Ac = this.Ac;
            character.SpellSaveDC = this.SpellSaveDC;
            character.SpellCastAbility = this.SpellCastAbility;
            character.Initiative = this.Initiative;
            character.Hp = this.Hp;
            character.HpMax = this.HpMax;
            character.HitDiceNumber = this.HitDiceNumber;
            character.HitDiceValue = this.HitDiceValue;
            character.Stats = StringToLstInt(this.Stats);
            character.Skills = StringToLstInt(this.Skills);
            character.Proefficiencies = StringToLstBool(this.Proefficiencies);
            // update attacks
            this.Attacks.ForEach(attack => character.Attacks.Add(new Attack(attack)));
            character.ProefficiencyBonus = this.ProefficiencyBonus;
            character.PassivePerception = this.PassivePerception;

            return character;
        }

        public void FromCharacter(Character character) {
            /* this.ID = character.ID; */
            this.IdGame = character.IdGame;
            this.IdPlayer = character.IdPlayer;
            this.Name = character.Name;
            this.ClassName = character.ClassName;
            this.RaceName = character.RaceName;
            this.Level = character.Level;
            this.Ac = character.Ac;
            this.SpellSaveDC = character.SpellSaveDC;
            this.SpellCastAbility = character.SpellCastAbility;
            this.Initiative = character.Initiative;
            this.Hp = character.Hp;
            this.HpMax = character.HpMax;
            this.HitDiceNumber = character.HitDiceNumber;
            this.HitDiceValue = character.HitDiceValue;
            this.Stats = LstIntToString(character.Stats);
            this.Skills = LstIntToString(character.Skills);
            this.Proefficiencies = LstBoolToString(character.Proefficiencies);
            // update attacks
            this.Attacks.Clear();
            // character.Attacks.ForEach(attack => this.Attacks.Add(attack));
            this.ProefficiencyBonus = character.ProefficiencyBonus;
            this.PassivePerception = character.PassivePerception;
        }

        /**********************************************************************/
        /* functions                                                          */
        /**********************************************************************/

        private string LstIntToString(List<int> list) {
            string output = "";

            foreach (int elt in list)
            {
                output += elt.ToString() + ";";
            }
            return output;
        }

        private List<int> StringToLstInt(string str) {
            List<int> output = new List<int>();

            foreach (string elt in str.Split(";").Where(s => s != ""))
            {
                output.Add(int.Parse(elt));
            }
            return output;
        }

        private string LstBoolToString(List<bool> list) {
            string output = "";

            foreach (bool elt in list)
            {
                output += elt.ToString() + ";";
            }
            return output;
        }

        private List<bool> StringToLstBool(string str) {
            List<bool> output = new List<bool>();

            foreach (string elt in str.Split(";").Where(s => s != ""))
            {
                output.Add(bool.Parse(elt));
            }
            return output;
        }
    }
}
