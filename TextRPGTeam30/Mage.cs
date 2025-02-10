namespace TextRPGTeam30
{
    internal class Mage : Job
    {
        public string name = "마법사";
        public Mage()
        {
            skills.Add(new Fireball());
            hp = 75;
            attack = 15;
            defense = 5;
        }

        public Mage( Skill skill, int hp, float attack, float defense) : base(skill, hp, attack, defense)
        {

        }
    }
}
