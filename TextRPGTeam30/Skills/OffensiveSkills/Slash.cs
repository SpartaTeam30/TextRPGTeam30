namespace TextRPGTeam30
{
    public class Slash : OffensiveSkill
    {
        public Slash()
        {
            name = "베기";
            description = "검을 휘두른다.";
            damageModifier = 1.5f;
            cost = 10;
            count = 1;
        }

        public Slash(string description, float damageModifier, int cost, int count) : base(description, damageModifier, cost, count)
        {

        }
    }
}
