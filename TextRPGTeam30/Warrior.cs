namespace TextRPGTeam30
{
    public class Warrior : Job
    {
        public Warrior()
        {
            name = "전사";
            skills.Add(new Slash());
            hp = 150;
            attack = 35;
            defense = 40;
        }

        public Warrior(Skill skill, int hp, float attack, float defense) : base(skill, hp, attack, defense)
        {

        }
    }
}
