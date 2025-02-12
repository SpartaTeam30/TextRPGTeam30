namespace TextRPGTeam30
{
    public class Mage : Job
    {
        public Mage()
        {
            name = "마법사";
            skills.Add(new Fireball());
            hp = 80;
            attack = 25;
            defense = 20;
        }

        public Mage( Skill skill, int hp, float attack, float defense) : base(skill, hp, attack, defense)
        {

        }
    }
}
