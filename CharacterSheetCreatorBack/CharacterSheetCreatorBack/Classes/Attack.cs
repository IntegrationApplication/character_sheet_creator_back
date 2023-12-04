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

        public int Roll()
        {
            int total = 0;
            var rand = new System.Random();

            for (int i = 0; i < DamageDice[0]; ++i)
            {
                sum += rand.Next() % DamageDice[1] + 1;
            }
            return sum + DamageDice[2];
        }
    }
}
