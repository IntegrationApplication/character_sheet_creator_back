using System.ComponentModel.DataAnnotations;

namespace CharacterSheetCreatorBack.Classes
{
    public class Spell
    {
        [Key]
        public string Description { get; set; }
        public Spell() 
        {
            Description = string.Empty;
        }
    }
}
