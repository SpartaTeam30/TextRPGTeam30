namespace TextRPGTeam30
{
    public class Fireball : OffensiveSkill
    {
        public Fireball()
        {
            name = "화염구";
            damageModifier = 2.5f;
            cost = 25;
        }

        public Fireball(float damageModifier, int cost) : base(damageModifier, cost)
        {

        }
    }
}
