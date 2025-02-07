namespace TextRPGTeam30
{
    internal class Skill
    {
        public string name;
        public int cost;

        public Skill(string name, int cost)
        {
            this.name = name;
            this.cost = cost;
        }

        public void UseSkill()
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");
        }
    }
}
