namespace CharacterSheetCreatorBack.Classes
{
    public class Character
    {
        public string Name { get; set; }
        public string PlayerID { get; set; }
        public string PlayerName { get; set; }
        public string Background { get; set; }
        public string Alignement { get; set; }
        public string Race { get; set; }
        public string ClassAndLEvel { get; set; }


        public Character()
        {
            Name = "";
            PlayerID = "";
            PlayerName = "";
            Background = "";
            Alignement = "";
            Race = "";
            ClassAndLEvel = "";
        }

        public Character(string name, string playerID, string playerName, string background, string alignement, string race, string classAndLevel)
        {
            Name = name; PlayerID = playerID; PlayerName = playerName; Background = background; Alignement = alignement; Race = race; ClassAndLEvel = classAndLevel;
        }
    }
}
