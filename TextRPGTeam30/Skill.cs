namespace TextRPGTeam30
{
    public class Skill
    {
        public string name;
        public int cost;
        public int count;

        public Skill()
        {

        }

        public Skill(int cost, int count)
        {
            this.cost = cost;
            this.count = count;
        }

        public void UseSkill()
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");
        }
    }
}
