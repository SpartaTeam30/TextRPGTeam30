namespace TextRPGTeam30
{
    internal class Warrior : Job
    {
        public Warrior()
        {
            name = "전사";
            skills.Add(new Slash());
            hp = 100;
            attack = 10;
            defense = 10;
        }

        public Warrior(string name, Skill skill, int hp, float attack, float defense) : base(name, skill, hp, attack, defense)
        {

        }
    }
}
