namespace CharacterSheetCreatorBack.Classes
{
    public class Skills
    {
        public List<Skill> SkillList { get; set; }
        public int modifier {  get; set; }


        public Skills()
        {
            SkillList = new List<Skill>();
            modifier = 0;
        }

        public Skill? Find(string name) {
            return SkillList.Find(x => x.Name == name);
        }
    }
}
