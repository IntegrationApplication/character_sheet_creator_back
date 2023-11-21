namespace CharacterSheetCreatorBack.Classes
{
    public class Player
    {
        public int ID { get; set; }
        public int IDDiscord { get; set; }
        public List<Character> Characters { get; set; }

        public Player() {
            ID = 0;
            IDDiscord = 0;
            Characters = new List<Character>();
        }
    }
}
