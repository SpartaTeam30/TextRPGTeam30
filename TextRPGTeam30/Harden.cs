namespace TextRPGTeam30
{
    internal class Harden : UtilitySkill
    {
        public string name = "단단해지기";
        public Harden()
        {
            cost = 15;
            dDefense = 1;
        }

        public Harden(int cost, int dHealth, int dAttack, int dDefense) : base(cost, dHealth, dAttack, dDefense)
        {

        }
    }
}
