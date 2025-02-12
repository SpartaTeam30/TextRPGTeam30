namespace TextRPGTeam30
{
    public class Skill
    {
        public string name;
        public string description;
        public int cost;
        public int count;

        public Skill()
        {

        }

        public Skill(string desctiption, int cost, int count)
        {
            this.description = desctiption;
            this.cost = cost;
            this.count = count;
        }

        public void UseSkill()
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");
        }
    }
}
