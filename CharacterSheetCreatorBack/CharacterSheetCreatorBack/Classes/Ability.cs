

namespace CharacterSheetCreatorBack.Classes
{
    public class Ability
    {
        public String Name { get; set; }
        public int Value { get; set; }
        public int Modifier { get; set; }
        public Skill LinkedSkill { get; set; }

        public Ability()
        {
            Name = "";
            Value = 0;
            Modifier = 0;
            LinkedSkill = new Skill();
        }

        public Ability(String name, int value, int modifier, Skill skill) 
        {  
            Name = name; 
            Value = value; 
            Modifier = modifier;
            LinkedSkill = skill;
        }
    }
}
