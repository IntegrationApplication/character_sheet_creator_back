using System.ComponentModel.DataAnnotations;

namespace CharacterSheetCreatorBack.Classes
{
    public class Ability
    {
        [Key]
        public String Name { get; set; }
        public int Value { get; set; }
        public int Modifier { get; set; }

        public Ability()
        {
            Name = "";
            Value = 0;
            Modifier = 0;
        }

        public Ability(String name, int value, int modifier) 
        {  
            Name = name; 
            Value = value; 
            Modifier = modifier;
        }
    }
}
