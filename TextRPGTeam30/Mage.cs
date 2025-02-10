namespace TextRPGTeam30
{
    public class Mage : Job
    {
        public string name = "마법사";
        public Mage()
        {
            name = "마법사";
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
