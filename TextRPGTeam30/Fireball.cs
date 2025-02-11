namespace TextRPGTeam30
{
    public class Fireball : OffensiveSkill
    {
        public string name = "화염구";
        public Fireball()
        {
            damageModifier = 2.5f;
            cost = 25;
        }

        public Fireball(float damageModifier, int cost) : base(damageModifier, cost)
        {

        }
    }
}
