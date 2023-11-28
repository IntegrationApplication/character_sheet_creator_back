namespace CharacterSheetCreatorBack.Classes
{
    public class Character
    {
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
    }
}
