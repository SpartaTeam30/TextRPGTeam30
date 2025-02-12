namespace TextRPGTeam30
{
    public class Fireball : OffensiveSkill
    {
        public Fireball()
        {
            name = "화염구";
            damageModifier = 2.5f;
            cost = 25;
            count = 1;
        }

        public Fireball(float damageModifier, int cost, int count) : base(damageModifier, cost, count)
        {

        }
    }
}
