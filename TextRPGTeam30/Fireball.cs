namespace TextRPGTeam30
{
    internal class Fireball : Skill
    {
        public Fireball()
        {
            name = "화염구";
            damage = 20;
            cost = 25;
        }

        public Fireball(string name, int damage, int cost) : base("화염구", 20, 25)
        {

        }
    }
}
