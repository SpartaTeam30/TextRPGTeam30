namespace TextRPGTeam30
{
    internal class Growl : UtilitySkill
    {
        public string name = "울부짖기";
        public Growl()
        {
            cost = 15;
            dAttack = -1;
        }

        public Growl(int cost, int dHealth, int dAttack, int dDefense) : base(cost, dHealth, dAttack, dDefense)
        {

        }
    }
}
