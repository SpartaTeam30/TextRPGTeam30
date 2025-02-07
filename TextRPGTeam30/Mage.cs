namespace TextRPGTeam30
{
    internal class Mage : Job
    {
        public string name { get; }
        public Mage(Skill skill, int hp, float attack, float defense) : base(skill, hp, attack, defense)
        {
            name = "마법사";
        }
    }
}
