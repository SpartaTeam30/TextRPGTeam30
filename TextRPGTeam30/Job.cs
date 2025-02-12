namespace TextRPGTeam30
{
    public class Job
    {
        public string name;
        public List<Skill> skills = new List<Skill>();
        public int hp;
        public int mp;
        public float attack;
        public int defense;

        public Job()
        {

        }

        public Job(Skill skill, int hp, int mp, float attack, int defense)
        {
            skills.Add(skill);
            this.hp = hp;
            this.mp = mp;
            this.attack = attack;
            this.defense = defense;
        }

        public void ResetStat(Player player)
        {
            player.Hp = hp;
            player.mp = mp;
            player.Attack = attack;
            player.Defense = defense;
        }
    }
}