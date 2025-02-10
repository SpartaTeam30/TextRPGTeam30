namespace TextRPGTeam30
{
    internal class Job
    {
        public string name;
        public List<Skill> skills = new List<Skill>();
        public int hp;
        public float attack;
        public float defense;

        public Job()
        {

        }

        public Job(string name, Skill skill, int hp, float attack, float defense)
        {
            this.name = name;
            skills.Add(skill);
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
        }

        public void ResetStat(Player player)
        {
            player.Hp = hp;
            player.Attack = attack;
            //player.Defense = defense;
        }
    }
}