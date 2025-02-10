namespace TextRPGTeam30
{
    internal class Mage : Job
    {
        public Mage()
        {
            name = "마법사";
            skills.Add(new Fireball());
            hp = 75;
            attack = 15;
            defense = 5;
        }

        public Mage(string name, Skill skill, int hp, float attack, float defense) : base(name, skill, hp, attack, defense)
        {

        }
    }
}
