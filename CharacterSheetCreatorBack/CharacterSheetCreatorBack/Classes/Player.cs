namespace CharacterSheetCreatorBack.Classes
{
    public class Player
    {
        [key]
        public int ID { get; set; }
       
        [key]
        public int IDDiscord { get; set; }
        public List<Character> Characters { get; set; }

        public Player() {
            ID = 0;
            IDDiscord = 0;
            Characters = new List<Character>();
        }
    }
}
