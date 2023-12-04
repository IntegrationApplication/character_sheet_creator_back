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

        public Class Classe { get; set; }

        public int Level { get; set; }

        public int Ac {  get; set; }
        
        public int SpellSaveDC { get; set; }

        public int SpeelCastAbility { get; set; }

        public int Initiative { get; set; }

        public int Hp { get; set; }

        public int HpMax { get; set; }  

        public int HitDice { get; set; }

        public List<Ability> Abilities { get; set; }

        public List<Attack> Attacks { get; set; }

        public List<Spell> Spells { get; set; }

        public List<Skill> Skills { get; set; }

        public int ProefficiencyBonus { get; set; }


        public Character()
        {
            Name = string.Empty;
            Classe = new Class();
            Level = 0;
            Ac = 0;
            SpellSaveDC = 0;
            SpeelCastAbility = 0;
            Initiative = 0;
            Hp = 0;
            HpMax = 0;
            HitDice = 0;
            Abilities = new List<Ability>();
            Skills = new List<Skill>();
            Attacks = new List<Attack>();
            Spells = new List<Spell>();
            ProefficiencyBonus = 0;
        }
    }
}
