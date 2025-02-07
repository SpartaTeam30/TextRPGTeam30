namespace TextRPGTeam30
{
    internal class Warrior : Job
    {
        public string name { get; }
        public Warrior(Skill skill, int hp, float attack, float defense) : base(skill, hp, attack, defense)
        {
            name = "전사";
        }
    }
}
