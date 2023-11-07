namespace CharacterSheetCreatorBack
{
    public class Character
    {  
        public String Name {get;set;}
        public String PlayerID { get;set;}
        public String PlayerName { get;set;}    
        public String Background { get;set;}
        public String Alignement { get;set;}
        public String Race { get;set;}
        public String ClassAndLEvel { get;set;}


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
    }
}
