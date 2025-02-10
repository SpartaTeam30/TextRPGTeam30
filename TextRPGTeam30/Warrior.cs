namespace TextRPGTeam30
{
    internal class Warrior : Job
    {
        public string name = "전사";
        public Warrior()
        {
            skills.Add(new Slash());
            hp = 100;
            attack = 10;
            defense = 10;
        }

        public Warrior(Skill skill, int hp, float attack, float defense) : base(skill, hp, attack, defense)
        {

        }
    }
}
