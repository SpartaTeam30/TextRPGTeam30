namespace TextRPGTeam30
{
    internal class Slash : Skill
    {
        public Slash()
        {
            name = "베기";
            damage = 10;
            cost = 10;
        }

        public Slash(string name, int damage, int cost) : base("베기", 10, 10)
        {

        }
    }
}
