namespace CharacterSheetCreatorBack.Classes
{
    public class Attack
    {
        public Ability LinkedAbility { get; set; }
        public string DamageType { get; set; }
        public int[] DamageDice { get; set; }
        public Attack() { 
            LinkedAbility = new Ability();
            DamageType = string.Empty;
            DamageDice = new int[3];
        }
    }
}
