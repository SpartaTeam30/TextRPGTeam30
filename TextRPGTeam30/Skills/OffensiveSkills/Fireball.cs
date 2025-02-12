namespace TextRPGTeam30
{
    public class Fireball : OffensiveSkill
    {
        public Fireball()
        {
            name = "화염구";
            description = "화염구를 발사한다.";
            damageModifier = 2.5f;
            cost = 25;
            count = 1;
        }

        public Fireball(string description, float damageModifier, int cost, int count) : base(description, damageModifier, cost, count)
        {

        }
    }
}
