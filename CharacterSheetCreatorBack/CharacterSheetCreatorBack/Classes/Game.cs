namespace CharacterSheetCreatorBack.Classes
{
    public class Game
    {
        public int ID {  get; set; }
        public Player MJ { get; set; }
        public string name { get; set; }
        public List<Tuple<Player,Character>> PlayerCharacters { get; set; }

        public Game() { 
            ID = 0;
            MJ = new Player();
            name = string.Empty;
            PlayerCharacters = new List<Tuple<Player,Character>>();
        }
    }
}
  