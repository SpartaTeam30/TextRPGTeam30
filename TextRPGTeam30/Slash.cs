namespace TextRPGTeam30
{
    public class Slash : OffensiveSkill
    {
        public Slash()
        {
            name = "베기";
            damageModifier = 1.5f;
            cost = 10;
        }

        public Slash(float damageModifier, int cost) : base(damageModifier, cost)
        {

        }
    }
}
