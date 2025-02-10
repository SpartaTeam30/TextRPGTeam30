namespace TextRPGTeam30
{
    public class Skill
    {
        public string name;
        public int cost;

        public Skill()
        {

        }

        public Skill(int cost)
        {
            this.cost = cost;
        }

        public void UseSkill()
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");
        }
    }
}
