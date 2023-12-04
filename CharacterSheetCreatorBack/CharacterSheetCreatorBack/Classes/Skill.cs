
namespace CharacterSheetCreatorBack.Classes
{
    public class Skill
    {
        public string Name { get; set; }
        public Ability Ability { get; set; }
        public int Modifier { get; set; }
        public bool Proefficiency { get; set; }

        public Skill()
        {
            Name = "";
            Ability = new Ability();
            Modifier = 0;
            Proefficiency = false;
        }

        public Skill(string name, Ability ability, int modifier, bool proefficiency)
        {
            Name = name;
            Ability = ability;
            Modifier = modifier;
            Proefficiency = proefficiency;
        }

        public Roll()
        {
            var rand = new System.Random();
            return rand.Next() % 20 + 1 + Modifier;
        }
    }
}
