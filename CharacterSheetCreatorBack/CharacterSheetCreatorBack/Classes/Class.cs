using System.ComponentModel.DataAnnotations;


namespace CharacterSheetCreatorBack.Classes
{
    public class Class
    {
        [Key]
        public string Name { get; set; }
        public int HitDice { get; set; }

        public Class() {
            Name = "";
            HitDice = 0;
        }


    }
}
