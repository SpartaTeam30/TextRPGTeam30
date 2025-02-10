namespace TextRPGTeam30
{
    internal class Slash : OffensiveSkill
    {
        public string name = "베기";

        public Slash()
        {
            damageModifier = 1.5f;
            cost = 10;
        }

        public Slash(float damageModifier, int cost) : base(damageModifier, cost)
        {

        }
    }
}
